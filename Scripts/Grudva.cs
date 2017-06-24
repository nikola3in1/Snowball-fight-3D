	using UnityEngine;
using System.Collections;

public class Grudva : Photon.MonoBehaviour {
	
	public float lifespan = 10f;
	public GameObject snegOstaci;
	public float ChestDMG=26f;
	float StomachDMG=20f;
	float HeadDMG=22f;
	public float ArmDMG=11f;
	public float LegDMG=10f;
	float BobsBackDMG=2f;
	int kills;
	ScoreManager scoreManager;
	NetworkManager netManager;
	float enemysHp;
	string nick;
	string ubica;
	string ubijeni;
	// Use this for initialization


	void Start () {
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
		netManager = GameObject.FindObjectOfType<NetworkManager> ();
		nick = photonView.owner.name;
	}
	
	// Update is called once per frame
	void Update () {
		
		Debug.Log (PhotonNetwork.player.name);


		lifespan -= Time.deltaTime;

		if (lifespan <= 0) {
			Explode();
		}

	}
		

	void OnCollisionEnter (Collision col){
		 
		Debug.Log ("OnColEnter" + PhotonNetwork.player.name);

		if (col.gameObject.tag == "Untagged" || col.gameObject.tag == "MyPlayer" || col.gameObject.tag == "Head"  || col.gameObject.tag == "Legs"  || col.gameObject.tag == "Stomach" || col.gameObject.tag == "BOT") {
			Instantiate (snegOstaci, col.contacts [0].point, Quaternion.identity);
			Explode ();

		}



		/*if (col.gameObject.tag == "MyPlayer") {							
			Health h = col.gameObject.GetComponentInParent<Health> ();
			Debug.Log ("We hit us");
			if (h != null) {
				//h.TakeDamage (HeadDMG);
				h.GetComponent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.AllBuffered, HeadDMG);
				Debug.Log (h.HealthPoints);
			}
		}*/

		if (col.gameObject.tag == "MyPlayer") {
			Explode ();
		}


		if (col.gameObject.tag == "BobsBack") 
		{	
			
			Health h = col.gameObject.GetComponentInParent<Health> ();
			Debug.Log ("We hit enemys chest");
			if (h != null) {
				//h.TakeDamage(ChestDMG);
				h.GetComponent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.All, BobsBackDMG);



				/*if(h.HealthPoints < 0)  {
					
					scoreManager.GetComponent<PhotonView> ().RPC ("killsUp", PhotonTargets.AllBufferedViaServer, nick);
				}*/
			}
		}


		if (col.gameObject.tag == "Stomach") 
		{
			Sounds s = col.gameObject.GetComponentInParent<Sounds> ();
			Health h = col.gameObject.GetComponentInParent<Health> ();
			Debug.Log ("We hit enemys stomach");

			if (h != null) {
				h.GetComponent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.All, StomachDMG);
				if (h.HealthPoints < 0) {
					scoreManager.GetComponent<PhotonView> ().RPC ("test", PhotonTargets.AllBuffered , nick);
				}
			

				}
			if (netManager.Boy == true && netManager.Bob == false && netManager.Girl == false) {
				s.DamageBoy ();
			}
			if (netManager.Bob == true && netManager.Boy == false && netManager.Girl == false) {
				s.DamageBob ();
			}
			if (netManager.Girl == true && netManager.Boy == false && netManager.Bob == false ) {
				s.DamageGirl ();
			}

		}


		if (col.gameObject.tag == "Head") {
			Sounds s = col.gameObject.GetComponentInParent<Sounds> ();
			Health h = col.gameObject.GetComponentInParent<Health> ();
			Debug.Log ("Headshot!");


			if (h != null) {
				h.GetComponent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.All, HeadDMG);
				if (h.HealthPoints < 0) {
					scoreManager.GetComponent<PhotonView> ().RPC ("test", PhotonTargets.AllBuffered, nick);
				}
				h.HeadShotAnim ();

			}

			if (netManager.Boy == true && netManager.Bob == false && netManager.Girl == false) {
				s.DamageBoy ();
			}
			if (netManager.Bob == true && netManager.Boy == false && netManager.Girl == false) {
				s.DamageBob ();
			}
			if (netManager.Girl == true && netManager.Boy == false && netManager.Bob == false) {
				s.DamageGirl ();
			}
		}


		if (col.gameObject.tag == "Leg") {
			Sounds s = col.gameObject.GetComponentInParent<Sounds> ();
			Health h = col.gameObject.GetComponentInParent<Health> ();
			Debug.Log ("We hit enemys leg");

			if (h != null) {
				h.GetComponent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.All, LegDMG);
				if (h.HealthPoints < 0) {
					scoreManager.GetComponent<PhotonView> ().RPC ("test", PhotonTargets.AllBuffered , nick);
				}
					
					
				}

				if (netManager.Boy == true && netManager.Bob == false && netManager.Girl == false) {
					s.DamageBoy ();
				}
				if (netManager.Bob == true && netManager.Boy == false && netManager.Girl == false) {
					s.DamageBob ();
				}
				if (netManager.Girl == true && netManager.Boy == false && netManager.Bob == false ) {
					s.DamageGirl ();
				}
			}
		if (col.gameObject.tag == "BOT") {
			BotMovement bt = col.gameObject.GetComponentInParent<BotMovement> ();
			if(bt != null){
				bt.Damage ();
			
				if (bt.HealthPoints <= 0f) {
					scoreManager.GetComponent<PhotonView> ().RPC ("test", PhotonTargets.AllBuffered, nick);
				}
			}
		}



	}

	void Explode() {
		Destroy (gameObject);
	}


}
 

