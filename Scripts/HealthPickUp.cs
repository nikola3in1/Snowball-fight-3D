using UnityEngine;
using System.Collections;

public class HealthPickUp : Photon.MonoBehaviour {

	public int BigHealthPickUpSpot = 2;
	float hpUp = -30f;
	Vector3 currentPosition;
	public AudioClip health;

	void Start(){
		currentPosition = gameObject.transform.localPosition;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag != "Grudva"){
		Health h = col.gameObject.GetComponent<Health> ();
		AudioSource.PlayClipAtPoint (health, currentPosition);
		h.GetComponent<PhotonView>().RPC ("TakeDamage" , PhotonTargets.AllBuffered ,hpUp);
			Explode();
		}
	}

	void Explode(){
		PhotonNetwork.Destroy (gameObject);
	}
}	
