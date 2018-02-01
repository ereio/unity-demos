/** Author: Taylor Ereio
 * File: SkeletonAnimController.cs
 * Date: 4/2/15
 * Description: Handles all off the interaction with the movement and appearance of the
 * skeleton.
 * */
using UnityEngine;
using System.Collections;

public class SkeletonAnimController : MonoBehaviour {
	static int startingState = Animator.StringToHash ("Base.LyingDown");
	static int prepareState = Animator.StringToHash ("Base.Prepare");
	static int walkingState = Animator.StringToHash ("Base.Walking");
	static int idleAttackState = Animator.StringToHash ("Base.IdleAttack");
	static int attackState = Animator.StringToHash ("Base.Attack");
	static int crawlingState = Animator.StringToHash ("Base.Crawling");
	static int deathState = Animator.StringToHash ("Base.Death");


	public bool damageHit;

	public float walkingSpeed;		// speeds of movement
	public float crawlingSpeed;
	public int initTime = 2;		// sets a pause before walking - adjustable
	public int damageCount = 0;		// amount of damage done to skeleton

	public Transform target;		// target for skeleton movement
	public Animator anim;			// access to several external/internal components
	public TextMesh meshWord;
	public AudioSource gunshot;

	private float constantY = -0.1663437f;	// constant skeleton y val
	private int ATTACK_TIME = 2;			// Attack time interval
	private float moveTime = 0;				// tracks before walking
	private bool initalized = false;		// check for init
	private float attackTime = 0;			// tracks before attacking
	private int attacks;					// number of attacks to player
	private AnimatorStateInfo info;			//    access to several private 
	private WordController wordController;	//    external/internal components
	private CameraShaker cameraShaker;
	private LevelController levelControl;

	// Use this for initialization
	void Start () {
		attacks = 0;
		wordController = GameObject.Find("WordTyped").GetComponent<WordController>();
		cameraShaker = Camera.main.GetComponent<CameraShaker>();
		levelControl = GameObject.Find("GlobalScripts").GetComponent<LevelController>();
		walkingSpeed = 1.75f;
		crawlingSpeed = 0.75f;
		// meshWord = GameObject.Find ("SkeletonText").GetComponent<TextMesh> ();
	}
	
	/// <summary>
	/// State machine for the Skeleton. Handles what checks and ops are done during
	/// each phase of animation. Damage to skeleton is only allowed when active, etc.
	/// </summary>
	void Update () {
		info = anim.GetCurrentAnimatorStateInfo (0);

		if(info.nameHash == startingState){			// Initalize and set movement delay
			if(!initalized){
				GenerateSkeleton();
				ViewTarget();
				moveTime = Time.time + initTime;
				initalized = true;
			}
		} else if(info.nameHash == prepareState){	// preparing word
			CheckPrepared();

		} else if(info.nameHash == walkingState){	// walking and accepting attacks
			WalkMovement();
			CheckDamage();
			CheckPosition();
			BeginAttack();

		} else if(info.nameHash == idleAttackState){// readying for attack / accepting attack
			CheckDamage();
			CheckAttack ();
			CheckNondead();

		} else if(info.nameHash == attackState){	// motion of attacking
			CheckDamage();

		} else if(info.nameHash == crawlingState){	// now crawling, no attack, accepting damage
			CrawlMovement();
			CheckPosition();
			CheckDamage();
			CheckWord();

		} else if(info.nameHash == deathState){		// dying and reseting of skeleton
			transform.position += new Vector3(0,-0.003f,0);
			meshWord.text = "";
			CheckRestart();

		}
	}


	//////////////////////////////////////////////

	// Re/Generates the Skeleton
	// Resets variables allowing for the reset of the FSM operations
	private void GenerateSkeleton(){
		int randomX = Random.Range(-4,4);
		float randomZ = Random.Range(0f,-3f);
		transform.position = new Vector3 (randomX, constantY, randomZ);
		meshWord.lineSpacing = -14;
		wordController.wordState = false;
		meshWord.text = "";
		damageCount = 0;
		moveTime = 0;
		attacks = 0;
	}

	// Checks if MeshText and Word are in ready position
	// and sets the next walking animation
	private void CheckPrepared(){
		if(Time.time > moveTime){
			if(!wordController.wordState)
			
				meshWord.text = wordController.SelectWord();
				anim.SetTrigger("walking_trigger");

				meshWord.transform.position = transform.position;
				meshWord.transform.rotation = transform.rotation;
				meshWord.transform.LookAt (-target.transform.position * 2);
				damageHit = false;
		}
	}

	// Sets the view of the skeleton - originally more complex
	// wasn't as hard as I made it originally...
	private void ViewTarget(){
		transform.LookAt(target.transform);
	}

	// Sets Crawl movement - necessary seperate function when adjusting speeds
	// and distance to restart 
	private void CrawlMovement(){
		transform.position += transform.forward * Time.deltaTime * crawlingSpeed;
		if(Vector3.Distance(new Vector3(0,0,0), transform.position) > 11f){
			initalized = false;
			anim.SetTrigger("restart_trigger");
		}
	}

	// Sets Walk movement
	private void WalkMovement(){
		transform.position += transform.forward * Time.deltaTime * walkingSpeed;
		if(Vector3.Distance(new Vector3(0,0,0), transform.position) > 12f){
			initalized = false;
			anim.SetTrigger("restart_trigger");
		}
	}

	// Checks position of Meshword and corrects it on each update if necessary
	private void CheckPosition(){
		meshWord.transform.position = transform.position;
		meshWord.transform.rotation = transform.rotation;
		meshWord.transform.LookAt (-target.transform.position * 2);


	}

	// Checks if attack can occur in idleattackstate and counts it as
	// well as init a camera shake
	private void CheckAttack (){
		if(Time.time > attackTime){
			attackTime = Time.time + ATTACK_TIME;
			anim.SetTrigger("attack_trigger");
			cameraShaker.ShakeCamera(0.5f);
			attacks++;
		}
	}

	// Begins attack
	private void BeginAttack(){
		if(Vector3.Distance(transform.position, target.transform.position) < 2f)
			if(attacks == 0)
				anim.SetTrigger("begin_attack_trigger");
	}

	// Checks if 3 attacks have occured and sets to non-dead skeleton
	private void CheckNondead(){
		if(attacks >= 3){
			anim.SetTrigger("nondead_trigger");
		}
	}

	// Checks if skeleton is hit with word completion
	// if so, the score, damage [to skeleton], and words are reset
	private void CheckDamage(){
		if(damageHit){
			damageHit = false;
			levelControl.score++;
			damageCount++;

			// Reset MeshText For Crawler
			meshWord.text = "";
			meshWord.lineSpacing = -3;

			// Selects new word - trips word bool in controller
			wordController.SelectWord();
			gunshot.Play();

			// Set Damage Trigger
			anim.SetTrigger("damage_trigger");
		}
	}

	// Checks restart position after death animation
	private void CheckRestart(){
		if(transform.position.y < -0.8f){
			anim.SetTrigger("restart_trigger");
			initalized = false;
		}
	}

	// Checks for word only before second damage count
	private void CheckWord(){
		if(damageCount < 2){
			meshWord.text = wordController.currentWord;
		}
	}
}
