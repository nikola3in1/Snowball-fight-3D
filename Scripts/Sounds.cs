using UnityEngine;
using System.Collections;


public class Sounds : MonoBehaviour {

	public AudioClip throwSound;

	GameObject respawncamlistener;
	public AudioListener hod;

	Vector3 currentPosition;
	AudioBoy audioBoy;
	AudioBob audioBob;
	AudioGirl audioGirl;

	public bool soundsOn=false;
	public bool musicOn;
	public float volume = 0.0f;
	public bool girl;
	public bool boy;
	public bool bob;

	void Start(){
		respawncamlistener = GameObject.FindGameObjectWithTag ("RespawnCamera");
		audioBoy = gameObject.GetComponent<AudioBoy> ();
		audioBob = gameObject.GetComponent<AudioBob> ();
		audioGirl = gameObject.GetComponent<AudioGirl> ();
		hod = gameObject.GetComponentInChildren<AudioListener> ();
	}


	void Update(){
		currentPosition = gameObject.transform.localPosition;
			/*

		if (AudioListener.volume == 1.0f) {
			if (soundsOn == true) {
				return;
			} else {
				AudioListener.volume =	 0.0f;
			}
		} 

		if (AudioListener.volume == 0.0f) {
			if (soundsOn == false) {
				return;
			} else {
				AudioListener.volume = 1.0f;
			}
		}

*/

		/*
		if (soundsOn == true && AudioListener.volume == 1.0f){
			//hod.enabled = true;
			AudioListener.volume = 1.0f;
//			respawncamlistener.SetActive(true);
		}
		if (soundsOn == false && AudioListener.volume == 1.0f) {
			//hod.enabled = false;
			AudioListener.volume = 0.0f;
///			respawncamlistener.SetActive(false);
		} 
		if (soundsOn == false && AudioListener.volume == 0.0f) {
			AudioListener.volume = 0.0f;
		}
		if(soundsOn == true && AudioListener.volume == 0.0f);
			AudioListener.volume = 1.0f;
		/*if (soundsOn == true && AudioListener.volume == 0.0f) {
			AudioListener.volume = 1.0f;
		}*/

	}

	public void Throw()
	{
		if (soundsOn == false) {
			AudioSource.PlayClipAtPoint (throwSound, currentPosition, volume);
		}

		if (soundsOn == true) {
			AudioSource.PlayClipAtPoint (throwSound, currentPosition);
		}



	}

	public void DamageBoy(){
		if (audioBoy.boy == true) {
			audioBoy.PlayRandomSound ();
		}
	}

	public void DamageBob(){
		if (audioBob.bob == true) {
			audioBob.PlayRandomSound ();
		}
	}

	public void DamageGirl(){
		if (audioGirl.girl == true) {
			audioGirl.PlayRandomSound ();
		}
	}




}
