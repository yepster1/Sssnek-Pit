using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


//this script alows other scripts to call change music
public class audioMixerScript : MonoBehaviour {
	public static audioMixerScript INSTANCE;

	public AudioMixerSnapshot[] snapshots;
	// Use this for initialization


	void Awake(){
		
		DontDestroyOnLoad (gameObject);
		if (INSTANCE == null) {
			INSTANCE = this;
		} else {
			Destroy (gameObject);
		}

		//

	}

	public void ChangeSnapShot(int position){
		snapshots[position].TransitionTo(0.3f);
	}

    public void FadeInIntro(){
		snapshots[0].TransitionTo(1f);
	}

    public void Mute(){
        snapshots[3].TransitionTo(3f);
    }






}
