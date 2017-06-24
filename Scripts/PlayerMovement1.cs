using UnityEngine;
using System.Collections;

public class PlayerMovement1 : MonoBehaviour {
	Animator anim;
	float moveHorizontal=0f;
	float moveVertical=0f;
	CharacterController cc;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		moveVertical = Input.GetAxis ("Vertical");
		moveHorizontal = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Input.GetAxis ("Vertical") );
		anim.SetFloat("SideSpeed", Input.GetAxis ("Horizontal"));
				
	}
}
