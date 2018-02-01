/** Author: Taylor Ereio
 * File: MusicController.cs
 * Date: 2/26/2015 - 4/2/15
 * Description: Script uses the volume setting of the music to fade the music in and out. Once the volume
 * is so low it's practically inaudiable, it will stop the audio.
 * Practically the same script I used from a previous homework in this class.
 * */

using UnityEngine;
using System.Collections;

public class MusicController: MonoBehaviour {
	float fadeInAmt = 0.001f;
	float fadeOutAmt = 0.002f;
	float fadeMax = 4;
	float fadeMin = 0.005f;
	AudioSource audio;

	void Update(){
		if(audio != null){
			if(!audio.isPlaying){
					init();
			} else {
				FadeIn();
			}
		}
	}

	private void init(){
		audio = GetComponent<AudioSource>();
		audio.Play();
		audio.volume = 0;
	}

	void FadeIn(){
		// increment volume by fadeInAmt till it reaches
		// fade max
			if(audio.volume < fadeMax)
				audio.volume += fadeInAmt;
	}


	void FadeOut(){
		// if the audio volume is lower than min
		// just shut it off, otherwise decrement audio
			if(audio.volume > fadeMin)
				audio.volume -= fadeOutAmt;
			else
				audio.Stop();

	}
}
