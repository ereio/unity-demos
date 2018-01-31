/** Author: Taylor Ereio
 * File: WallGenerator.cs
 * Date: 2/3/2015
 * Description: Generates a wall lasting 5 seconds, every 7 seconds
 * */
using UnityEngine;
using System.Collections;

public class WallGenerator : MonoBehaviour {
	public GameObject wallType;			// type of wall variable
	public const float GEN_TIME = 7.0f;	// time between wall generation
	public const float LIFE_TIME = 5.0f;// time of wall life span
	private int randomX,				// random x cord
				randomZ;				// random y cord
	private GameObject currentWall;		// handle for current rendered wall
	private float nextGen;				// calculated handler time to render

	// Use this for initialization
	void Start () {
		GenerateWall();					// generate wall
		nextGen = Time.time + GEN_TIME;	// calculate the time till next generation
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= nextGen){		// if past time for gen
			GenerateWall();				// generate wall and calc accurate next time
			nextGen = Time.time + GEN_TIME - (Time.time - nextGen);
		}

	}

	// If the wallType isn't null, it will generate in a area of 50x50
	// the floor is 35x35 so it will never fall off the map
	// after instantiating, it will set up the destruction of the wall at the const LIFE_TIME variable
	// based on seconds
	void GenerateWall(){
		if(wallType != null){
			randomX = Random.Range(-25,25);
			randomZ = Random.Range(-25,25);
			currentWall = (GameObject)Instantiate (wallType, new Vector3(randomX, 4.5f, randomZ), Quaternion.identity);
			Destroy (currentWall, LIFE_TIME);
		}
	}
}