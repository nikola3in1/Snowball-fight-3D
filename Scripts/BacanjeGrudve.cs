using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class BacanjeGrudve : Photon.MonoBehaviour {
	public GameObject Grudva_objekt;
	public float impuls=100f;
	public float grudveBroj=0f;
	public float currentGrudve;
	bool animPlaying = false;
	Camera cam;
	GameObject thisGameObject;
	internal Animator anim;
	bool animW;
	bool bacanje=false;
	NetworkCharacter netChar;
	public bool isBot;
	Camera botCam;
	public GameObject BotAim;
	BotMovement botMovement;
	int oldcullinMask;
	bool skuplja=false;
	public void Start(){

		netChar = gameObject.GetComponent<NetworkCharacter> ();
		cam = gameObject.GetComponentInChildren<Camera> ();
		botCam = gameObject.GetComponentInChildren<Camera> ();
		botMovement = gameObject.GetComponent<BotMovement> ();
		

		grudveBroj = 5;
		anim = GetComponent<Animator> ();

		oldcullinMask = cam.cullingMask;



	}
	// Update is called once per frame
	void Update () {

		if (skuplja == false) {
			cam.cullingMask = oldcullinMask;
		}


		currentGrudve = grudveBroj;

		if (photonView.isMine) {


			if (Input.GetMouseButton (0) && grudveBroj > 0) {
				animW = true;
			
				anim.SetBool ("BacanjeSkok", true);
				gameObject.GetComponent<CharacterController> ().enabled = true;

			}
		

			if (Input.GetKey (KeyCode.R) && currentGrudve < 15f) {
				if (animPlaying == false) {
	
					anim.SetBool ("Sakupljanje", true);
					skuplja = true;

					//animPlaying = true;
				}
			}

		}


		if (isBot) {
			if (grudveBroj > 0f) {
				botMovement.ImamoGrudve = true;
			} else if (grudveBroj < 0f) {
				botMovement.ImamoGrudve = false;
			}
		}

	}


	void Sakupljanje(){
		grudveBroj+=0.5f;

	}
	public void PrekidanjeBacanja(){
		
		anim.SetBool ("BacanjeSkok", false);
	
	}

	void movementOff(){
		gameObject.GetComponent<CharacterController>().enabled = false;

	}



	void prekidanjeAnima(){
		
		//anim.SetLayerWeight (1,0.1f);

		anim.SetBool ("Sakupljanje", false);
	
		gameObject.GetComponent<CharacterController>().enabled = true;
		anim.SetBool ("BacanjeSkok", false);
		skuplja = false;
	

	}




	public void CameraCulling(){
		cam.cullingMask = -1 ;
	}

	void Bacanje(){
		Debug.Log (grudveBroj);

		if (!isBot) {
			if (photonView.isMine) {

				Camera cam = Camera.main;	
				GameObject instGrudva = (GameObject)PhotonNetwork.Instantiate ("grudva_objekt", cam.transform.position + cam.transform.forward, cam.transform.rotation, 0);
				Rigidbody rb = instGrudva.GetComponent<Rigidbody> ();
				rb.AddForce (cam.transform.forward * impuls, ForceMode.Impulse);
			
				grudveBroj--;
				animW = false;
				//anim.SetLayerWeight (1,0.1f);
				anim.SetBool ("BacanjeSkok", false);
				anim.SetBool ("Sakupljanje", false);
				animPlaying = false;
			}
		}

		else if (isBot) {


			GameObject instGrudva = (GameObject)PhotonNetwork.Instantiate ("grudva_objekt", BotAim.transform.position + BotAim.transform.forward, BotAim.transform.rotation, 0);
			Rigidbody rb = instGrudva.GetComponent<Rigidbody> ();
			rb.AddForce (BotAim.transform.forward * impuls, ForceMode.Impulse);
			grudveBroj--;
			botMovement.GrudveBroj--;
		}
}

}

