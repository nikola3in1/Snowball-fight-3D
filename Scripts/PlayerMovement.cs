using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	public float speed =2f;
	float moveVertical= 0f;
	float moveHorizontal =0f;
	float backwardSpeed=1f;
	float jumpSpeed= 50f;

	int jumpHash = Animator.StringToHash("Jump");

	CharacterController cc;
	Animator anim;
	NetworkCharacter netChar;

	void Start () {
		netChar = gameObject.GetComponent<NetworkCharacter> ();	
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		netChar.direction = transform.rotation * new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical") ).normalized;



		if (netChar.direction.magnitude > 1f) 
		
		{
			netChar.direction = netChar.direction.normalized;
		}
		anim.SetFloat ("Speed", Input.GetAxis ("Vertical") );
		anim.SetFloat("SideSpeed", Input.GetAxis("Horizontal"));



	
		if (cc.isGrounded) 
		{	
			anim.SetBool ("BacanjeSkok", false);
			anim.SetBool ("Jumping", false);

			if (Input.GetButton ("Jump")) 
			{
				netChar.verticalVelocity = jumpSpeed;
			} else 			
			{
				netChar.verticalVelocity = 0;			
			}

		}
		else 
		{	
			anim.SetBool ("Jumping", true);
		}	




		
	}




}

