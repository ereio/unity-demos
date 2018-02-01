/** Author: Taylor Ereio
 * File: WordController.cs
 * Date: 4/2/15
 * Description: Handles the word selection and user input checking.
 * IMPORTANT NOTE: most of foreach loop in update was found on the Unity Documentation
 * site under Input.inputString.
 * */
using UnityEngine;
using System.Collections;

public class WordController : MonoBehaviour {
	public GUIText guiText;


	private string[] dictionaryEasy = {
		"easy", "crazy", "duck", 
		"agree","apple","country","family","jelly",
		"slimy","cheap","saturn","walrus","quack",
		"knock","inside","animals","panda",
		"machine","catacomb","blubber","coffee","devil",
		"cricket","market","sand","farm","devil",
		"yell","water","pikachu","village",
		"partner",};

	private string[] dictionaryMedium = {
		"abrogate", "blasphemy", "refridgerator", 
		"laceration","obdurate","epitome","hyperbole","hedonism",
		"blighted","perfection","plethora","enunciation","blunderbuss",
		"earthquake","hurricane","labyrinth","resignation",
		"haughtiness","repudiate","objective","crepuscular","hypertension",
		"crickets","turtleneck","casablanca","tuberculosis",
		"superstitious","lamentation","tentative","acquiesce",
		"supercalifragilisticexpialidocious",};

	public string currentWord;
	public bool wordState;


	private SkeletonAnimController animController;
	
	// Use this for initialization
	void Start () {
			wordState = false;
			animController = GameObject.Find("skeleton").GetComponent<SkeletonAnimController>();
	}
	
	// Update is called once per frame
	void Update () {
		// most of this loop was provided by the unity documentation site
		// Receives a character from Input.inputString and parses it based on
		// characters receieved. If enter/return or space it will accept the
		// word in the stream to check if it's the current word. Otherwise,
		// it's added to the guiText and considered a part of an later attempt
			foreach (char c in Input.inputString) {
				if (c == "\b"[0]){
					if (guiText.text.Length != 0)	// deletes values if there are any to delete
						guiText.text = guiText.text.Substring(0, guiText.text.Length - 1);
				} else if(c == "\n"[0] || c == "\r"[0] || c == " "[0]){
					checkWordInput();
					guiText.text = "";
				} else {
					guiText.text += c;
				}
			}
	}

	// selects words based on skeleton state, could allow for different levels
	// of difficulty
	public string SelectWord(){
		// if the skeleton hasn't been damaged, fetch an easy word
		if(animController.damageCount < 1){
			int word_num = Random.Range (0, dictionaryEasy.Length-1);
			currentWord = dictionaryEasy [word_num];
		// if the skeleton has been damaged, fetch a medium word
		} else if(animController.damageCount >= 1){
			int word_num = Random.Range (0, dictionaryMedium.Length-1);
			currentWord = dictionaryMedium [word_num];
		}
		wordState = true;
		return currentWord;
	}

	// selects a word and records damage hit
	private void checkWordInput(){
		if(guiText.text == currentWord){
			SelectWord();
			animController.damageHit = true;
		}
	}
}
