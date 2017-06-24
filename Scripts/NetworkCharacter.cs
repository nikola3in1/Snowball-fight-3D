using UnityEngine;		
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
	
	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	Animator anim;
	[System.NonSerialized]
	public Vector3 direction=Vector3.zero;
	
	public float verticalVelocity =10f;
	PlayerMovement playerMovement;
	RoundManager roundManager;
	float minuti;
	float trajanjeRunde;
	bool gotFirstUpdate = false;
	public string nick;
	// Use this for initialization
	void Start () {

		playerMovement = gameObject.GetComponent<PlayerMovement> ();

		roundManager = GameObject.FindObjectOfType<RoundManager> ();


		if (PhotonNetwork.isMasterClient && roundManager.rundaTraje == false) {
			roundManager.minuti = 4f;	
			roundManager.trajanjeRunde = 59f;
			roundManager.hpPickupTimer = 20f;
			roundManager.rpcPoslat = false;
			roundManager.rundaTraje = true;

		} 




		anim = GetComponent<Animator> ();
		if (anim == null) {
			Debug.LogError ("No animator component od prefab");
		}

	}



	void Update () {

		if (photonView.isMine) {
			LocalMovement ();
		}
		else {

			transform.position=Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
			
		}

		
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext (anim.GetBool ("BacanjeSkok"));
			stream.SendNext (anim.GetFloat ("Speed"));
			stream.SendNext (anim.GetFloat ("SideSpeed"));
			stream.SendNext (anim.GetBool ("Jumping"));
			stream.SendNext (anim.GetBool ("Dead"));
			stream.SendNext (anim.GetBool ("Damage"));
			stream.SendNext (anim.GetBool ("Sakupljanje"));
			stream.SendNext (anim.GetBool ("Charm"));




		}
		else {
			realPosition = (Vector3)stream.ReceiveNext ();
			realRotation= (Quaternion)stream.ReceiveNext ();
			anim.SetBool ("BacanjeSkok",(bool)stream.ReceiveNext ());
			anim.SetFloat ("Speed", (float)stream.ReceiveNext ());
			anim.SetFloat ("SideSpeed", (float)stream.ReceiveNext ());
			anim.SetBool ("Jumping", (bool)stream.ReceiveNext ());
			anim.SetBool ("Dead", (bool)stream.ReceiveNext ());
			anim.SetBool ("Damage",(bool)stream.ReceiveNext ());
			anim.SetBool ("Sakupljanje", (bool)stream.ReceiveNext ());
			anim.SetBool ("Charm",(bool)stream.ReceiveNext ());
			



		
			if (gotFirstUpdate == false) {
				transform.position = realPosition;
				transform.rotation = realRotation;
				gotFirstUpdate = true;
			}
		}

	}



	void LocalMovement() {
		Vector3 dist = direction * playerMovement.speed * Time.deltaTime;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		dist.y = verticalVelocity * Time.deltaTime;  

	}
}
