/** Author: Taylor Ereio
 * File: ColorShifter.cs
 * Date: 3/4/2015
 * Description: Changes the color of the blocks based on the position of the mouse. If the mouse is over the
 * blocks they will change to black, upon exit, they will change back to their original color.
 * */

using UnityEngine;
using System.Collections;

public class ColorShifter : MonoBehaviour {

	private Color preset;

	void Start(){
		// copy preset color
		preset = GetComponent<Renderer>().material.color;
	}
	void OnMouseEnter(){
		// change to black
		 GetComponent<Renderer>().material.color = Color.black;
	}

	void OnMouseExit(){
		// change back
		 GetComponent<Renderer>().material.color = preset;
	}
}
