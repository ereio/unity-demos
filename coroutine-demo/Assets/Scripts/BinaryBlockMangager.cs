/** Author: Taylor Ereio
 * File: BinaryBlockManager.cs
 * Date: 3/26/15
 * Description: Contains the logic that operates the movement of the block. Has two states that actually do
 * anything, but could potentially have more for different movements.
 * When the state changes, I use a if else turnary condition to swap the values of the speed so it
 * moves downards
 * */
using UnityEngine;
using System.Collections;
public enum GameState {INIT, ON, OFF, WAVE, WAIT}

public class BinaryBlockMangager : MonoBehaviour {
	public GameState state = GameState.OFF;
	public int speed = 1;
	
	Vector3 targetPosition;

	private bool init;
	// Use this for initialization
	void Start () {
		state = GameState.ON;
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case GameState.ON:
			GetComponent<Renderer>().material.color = Color.green;
			targetPosition.y = 1.0f;
			speed = speed < 0 ? -speed : speed;
			MoveBlock();
			break;
		case GameState.OFF:
			GetComponent<Renderer>().material.color = Color.red;
			targetPosition.y = 0.5f;
			speed = speed > 0 ? -speed : speed;	// swaps speed for negative movement
			MoveBlock();
			break;
		default:
			break;
		}
	}

	private void MoveBlock(){
		if(Vector3.Distance(transform.position, targetPosition) > 0.01f){
			transform.position += new Vector3(0, speed * Time.deltaTime, 0);
		} else {
			state = GameState.WAIT;
		}
	}
}
