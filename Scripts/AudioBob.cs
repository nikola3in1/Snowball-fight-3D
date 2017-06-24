using UnityEngine;
using System.Collections;

public class AudioBob : MonoBehaviour {

	AudioClip[] sounds;
	AudioClip sound;
	Health h;
	Vector3 currentPosition;
	public bool bob=true;
	// Use this for initialization
	void Start () {
		
		sounds = new AudioClip[] {
			(AudioClip)Resources.Load ("BOB/Bob1"),
			(AudioClip)Resources.Load ("BOB/Bob2"),
			(AudioClip)Resources.Load ("BOB/Bob3"),
			(AudioClip)Resources.Load ("BOB/Bob4")
		};


		h= gameObject.GetComponent<Health>();
	}

	// Update is called once per frame
	void Update () {
		currentPosition = gameObject.transform.localPosition;

	}

	public void PlayRandomSound(){

		sound = sounds [Random.Range (0, sounds.Length)];
		AudioSource.PlayClipAtPoint(sound ,currentPosition);
		Debug.Log ("bob sound");
	}

}