/** Author: Taylor Ereio
 * File: HandController.cs
 * Date: 3/4/2015
 * Description: Just controls the movement and pointing direction of the hand.
 * */

using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// This creates the appearance that the hand is following the mouse by
		// making the transform of the hand directed at the ray.
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		transform.LookAt (ray.direction * 150);

		// For Debugging purposes
		if(Input.GetKey(KeyCode.A))
			Debug.Log(Input.mousePosition + " Position");
	}
}
