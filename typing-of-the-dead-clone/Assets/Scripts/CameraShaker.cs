/** Author: Taylor Ereio
 * File: CameraShaker.cs
 * Date: 4/2/15
 * Description: Handles the camera movement when the player is attacked
 * */
using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

	private float shake;
	private float disseminate = 0.01f;
	private Vector3 originalPosition;

	void Start () {
		originalPosition = Camera.main.transform.position;
	}
	

	void Update () {
		if(shake > 0.05f){
			float x = Random.Range (-shake, shake);	// creates a x shake
			float y = Random.Range (-shake, shake);	// creates a y shake
			x += originalPosition.x;				// equates the shake with the camera position
			y += originalPosition.y;				// \/ shake the camera!
			transform.position = new Vector3(x,y,originalPosition.z);
			shake -= disseminate;					// make it shake less next time
		} else {									// otherwise, we aren't shakin'
			Camera.main.transform.position = originalPosition;
		}
	}

	// enter a full float for amount of movement
	// from another class
	public void ShakeCamera(float amount){
		StartCoroutine(MainCamShake (amount)); 
	}

	// waits for the hit animation from the skeleton
	// and enters the amount to shake
	IEnumerator MainCamShake(float amount){
		yield return new WaitForSeconds(0.75f);
		shake = amount;
	}
	
}
