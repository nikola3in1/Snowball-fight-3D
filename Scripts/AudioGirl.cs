using UnityEngine;
using System.Collections;

public class AudioGirl : MonoBehaviour {

	public AudioClip charming;
	public AudioClip laugh;

	AudioClip[] sounds;
	AudioClip sound;
	Health h;
	Vector3 currentPosition;
	public bool girl=true;
	// Use this for initialization
	void Start () {
		sounds = new AudioClip[] {
			(AudioClip)Resources.Load ("SALLY/Sally1"),
			(AudioClip)Resources.Load ("SALLY/Sally2")

		};


		h = gameObject.GetComponent<Health> ();
	}

	// Update is called once per frame
	void Update () {
		currentPosition = gameObject.transform.localPosition;

	}

	public void PlayRandomSound(){

		sound = sounds [Random.Range (0, sounds.Length)];
		AudioSource.PlayClipAtPoint(sound ,currentPosition);
		Debug.Log ("girl sound");
	}

	public void CharmingSFX(){
		AudioSource.PlayClipAtPoint (charming, currentPosition);
		AudioSource.PlayClipAtPoint (laugh, currentPosition);
	}

}