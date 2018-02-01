/** Author: Taylor Ereio
 * File: LevelController.cs
 * Date: 4/2/15
 * Description: Handles the potential for level management and keeping score
 * */
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public int level = 0;
	public int score = 0;
	public GUIText levelDisplay;
	public GUIText scoreDisplay;

	// Use this for initialization
	void Start () {
		//levelDisplay = GameObject.Find("LevelDisplay").GetComponent<GUIText>();
		scoreDisplay = GameObject.Find("ScoreDisplay").GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		//levelDisplay.text = "Level: " + level;
		scoreDisplay.text = "Score: " + score;

		// Logic here for adding skeletons when level increases
		// Also add combo stufffff maybe
	}
}
