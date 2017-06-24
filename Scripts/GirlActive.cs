using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GirlActive : Photon.MonoBehaviour {

	Vector3 currentPosition;
	Health h;
	Animator anim;
	float cooldown;
	public float cooldownSec=40f;
	public float activeTime=4f;
	Vector3 position;
	Quaternion rotation;
	NetworkManager netManager;
	AudioGirl aGirl;
	public bool activeUsed;
	CharmTimer timer;
	public GameObject hbx;

	void Start () {
		aGirl = gameObject.GetComponent<AudioGirl> ();
		h = gameObject.GetComponent<Health> ();
		anim = gameObject.GetComponent<Animator> ();
		netManager = GameObject.FindObjectOfType<NetworkManager> ();
		netManager.CharmGUI.SetActive (true);
		timer = GameObject.FindObjectOfType<CharmTimer> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = gameObject.transform.localPosition;
		cooldownSec -= Time.deltaTime;


		if (cooldownSec > 0f) {
			timer.text.text = cooldownSec.ToString ("#") + "sec";
			timer.text.enabled = true;
			timer.ready.enabled = false;
			timer.readyPic.SetActive (false);

		}







		if (cooldownSec <= 0f) {

			timer.text.enabled = false;
			timer.ready.enabled = true;
			timer.readyPic.SetActive (true);
			

			if (Input.GetKeyDown (KeyCode.F)) {
				anim.SetBool ("Charm", true);
				//////yyy
				aGirl.CharmingSFX();

				Srca ();
				cooldownSec = 40f;
				activeUsed = true;
				h.activeTime = activeTime;
			
			}
		} 


	}
	public void Srca(){

		position = gameObject.transform.position;
		rotation = gameObject.transform.rotation;
		if(photonView.isMine)
			h.GetComponent<PhotonView> ().RPC ("srcaRPC", PhotonTargets.All, position,rotation);

	}

	[PunRPC]
	void Hbx(bool state){
		hbx.SetActive(state);
	}

}
