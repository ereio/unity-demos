/** Author: Taylor Ereio
 * File: CollisionDectection.cs
 * Date: 2/3/2015
 * Description: Detects if bricks are hit by ball within the wall
 * */
using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	public GameObject detect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// pretty simple. When the ball hits an object titled Brick (if child) or Wall (the parent brick)
	// it will turn the corresponding object to a material with the color yellow.
	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "Brick" || col.gameObject.name == "Wall")
			col.gameObject.renderer.material.color = Color.yellow;
	}
}
