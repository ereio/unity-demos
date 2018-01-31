/** Author: Taylor Ereio
 * File: Shooter.cs
 * Date: 2/3/2015
 * Description: Moves the Main Camera Gameobject and allows a ball projectile to
 * dispense from the main camera on the press of the space bar.
 * */
using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Rigidbody ballType;
	public const float SPEED = 0.2f;		// constant speed of main camera
	public const int BALL_SPEED = 1000;		// constant ball speed
	public const float BALL_LIFE = 2.0f;	// ball second life
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	// moves character by speed in direction of arrow keys pressed
	// If space is hit (not held) character will shoot a ball
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.Translate(SPEED * Vector3.forward);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate(SPEED * Vector3.left);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.Translate(SPEED * Vector3.back);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Translate(SPEED * Vector3.right);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			ShootBall();
		}
	}

	// First gets a handle of rigidbody on the instanciated ball
	// force of 1000 is added to the ball in the forward position
	// then it's set to destroy in 2 seconds
	void ShootBall(){
		Rigidbody curBall = (Rigidbody) Instantiate (ballType, transform.position, Quaternion.identity);
		curBall.AddForce (BALL_SPEED * Vector3.forward);
		Destroy (curBall.gameObject, BALL_LIFE);
	}

}
