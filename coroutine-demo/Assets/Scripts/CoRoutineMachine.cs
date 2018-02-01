/** Author: Taylor Ereio
 * File: CoRoutineMachine.cs
 * Date: 3/26/15
 * Description: Contains most of the logic for the FSM's and Coroutines. 
 * */
using UnityEngine;
using System.Collections;

public class CoRoutineMachine : MonoBehaviour {

	public GameObject blockOne;
	public GameObject blockTwo;
	public GameObject blockFour;
	public GameObject blockEight;

	public Transform cameraTarget;
	public GUIText guiStatus;

	public AudioSource dingSound;
	public AudioSource tickSound;

	private BinaryBlockMangager oneMgmt;
	private BinaryBlockMangager twoMgmt;
	private BinaryBlockMangager fourMgmt;
	private BinaryBlockMangager eightMgmt;

	BinaryBlockMangager[] testing;
	int displayNumber = 0;

	// Use this for initialization
	void Start () {
		oneMgmt = blockOne.GetComponent<BinaryBlockMangager> ();
		twoMgmt = blockTwo.GetComponent<BinaryBlockMangager> ();
		fourMgmt = blockFour.GetComponent<BinaryBlockMangager> ();
		eightMgmt = blockEight.GetComponent<BinaryBlockMangager> ();

		StartCoroutine (gameSequence());
	}

	IEnumerator gameSequence(){
		yield return StartCoroutine(InitalizeDisplay());
		yield return StartCoroutine(CountDisplay ());
		yield return StartCoroutine(WaveDisplay ());
		yield return StartCoroutine(Outro());
	}

	IEnumerator InitalizeDisplay(){
		guiStatus.text = "Initalizing...";

		float transitionTime = 2.0f;
		float startTime = Time.time;
		Vector3 startPosition = transform.position;

		// moves the camera closer
		while(Vector3.Distance(transform.position, cameraTarget.transform.position) > 0.01f){
			GetComponent<Camera>().transform.position = Vector3.Lerp(startPosition,cameraTarget.transform.position,
			                                         (Time.time-startTime)/transitionTime);
			yield return null;
		}

		startTime = Time.time;
		while((Time.time - startTime) < 2.0f){
			tickSound.Play();			// plays the ticks for a bit
			yield return new WaitForSeconds(0.5f);
		}

		oneMgmt.state = GameState.OFF;	// resets blocks
		twoMgmt.state = GameState.OFF;
		fourMgmt.state = GameState.OFF;
		eightMgmt.state = GameState.OFF;
		yield break;
	}

	IEnumerator CountDisplay(){
		while(displayNumber < 15){
			displayNumber++;		// increases number on first hit to 1
			dingSound.Play();
			tickSound.Play();		// plays both sounds
			CountDisplaySwitch();	// Runs the binary display
			guiStatus.text = "Counting: " + displayNumber;
			yield return new WaitForSeconds(0.5f);
			tickSound.Play();		// adds a tick every other half second
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}

	// The half a second wait allows each block to reach the top
	// before sending the previous down again.
	IEnumerator WaveDisplay(){
		guiStatus.text = "WAVE!!!";
		oneMgmt.state = GameState.OFF;
		twoMgmt.state = GameState.OFF;
		fourMgmt.state = GameState.OFF;
		eightMgmt.state = GameState.OFF;
		yield return new WaitForSeconds(1.5f);
		dingSound.Play();
		oneMgmt.state = GameState.ON;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		twoMgmt.state = GameState.ON;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		oneMgmt.state = GameState.OFF;
		fourMgmt.state = GameState.ON;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		twoMgmt.state = GameState.OFF;
		eightMgmt.state = GameState.ON;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		fourMgmt.state = GameState.OFF;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		fourMgmt.state = GameState.ON;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		eightMgmt.state = GameState.OFF;
		twoMgmt.state = GameState.ON;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		fourMgmt.state = GameState.OFF;
		oneMgmt.state = GameState.ON;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		twoMgmt.state = GameState.OFF;
		yield return new WaitForSeconds(0.5f);
		dingSound.Play();
		oneMgmt.state = GameState.OFF;
		yield return null;

	}

	IEnumerator Outro(){
		guiStatus.text = "GoodBye :)";
		yield break;
	}

	// Large switch/FSM representing the binary count from
	// 1 to 15
	private void CountDisplaySwitch(){
		switch(displayNumber){
		case 1:
			oneMgmt.state = GameState.ON;
			break;
		case 2:
			oneMgmt.state = GameState.OFF;
			twoMgmt.state = GameState.ON;
			break;
		case 3:
			oneMgmt.state = GameState.ON;
			break;
		case 4:
			oneMgmt.state = GameState.OFF;
			twoMgmt.state = GameState.OFF;
			fourMgmt.state = GameState.ON;
			break;
		case 5:
			oneMgmt.state = GameState.ON;
			break;
		case 6:
			oneMgmt.state = GameState.OFF;
			twoMgmt.state = GameState.ON;
			break;
		case 7:
			oneMgmt.state = GameState.ON;
			break;
		case 8:
			oneMgmt.state = GameState.OFF;
			twoMgmt.state = GameState.OFF;
			fourMgmt.state = GameState.OFF;
			eightMgmt.state = GameState.ON;
			break;
		case 9:
			oneMgmt.state = GameState.ON;
			break;
		case 10:
			oneMgmt.state = GameState.OFF;
			twoMgmt.state = GameState.ON;
			break;
		case 11:
			oneMgmt.state = GameState.ON;
			break;
		case 12:
			oneMgmt.state = GameState.OFF;
			twoMgmt.state = GameState.OFF;
			fourMgmt.state = GameState.ON;
			break;
		case 13:
			oneMgmt.state = GameState.ON;
			break;
		case 14:
			oneMgmt.state = GameState.OFF;
			twoMgmt.state = GameState.ON;
			break;
		case 15:
			oneMgmt.state = GameState.ON;
			break;
		default:
			break;
		}
	}

}
