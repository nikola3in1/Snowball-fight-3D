using UnityEngine;
using System.Collections;

public class AudioBoy : MonoBehaviour {
	NetworkManager netManager;
	AudioClip[] sounds;
	AudioClip sound;
	Health h;
	Vector3 currentPosition;
	public bool boy=true;
	// Use this for initialization
	void Start () {
		


		sounds = new AudioClip[] {
			(AudioClip)Resources.Load ("HARRY/Harry1"),
			(AudioClip)Resources.Load ("HARRY/Harry2"),
			(AudioClip)Resources.Load ("HARRY/Harry3"),
			(AudioClip)Resources.Load ("HARRY/Harry4")
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

	}

}
