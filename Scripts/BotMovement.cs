using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BotMovement : Photon.MonoBehaviour {
	public TextMesh text;
	float hp;

	NetworkManager netManager;

	public float fpsTargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovementSpeed;
	public float damping;
	public float HealthDistance;
	public float HealthZone;

	public float HealthPoints = 100f;

	Animator anim;
	float impuls = 100f;
	BacanjeGrudve bacnjeGrudve;
	public static GameObject[] enemys;
	public static GameObject[] HPs;
	GameObject closest;
	GameObject closestHP;
	GameObject closestHealth;
	GameObject closestEnemy;
	bool isMoving=false;
	public bool ImamoGrudve;
	public float GrudveBroj = 5f;
	Health h;

	void Start(){
		netManager = GameObject.FindObjectOfType<NetworkManager> ();
		anim = gameObject.GetComponent<Animator> ();
		bacnjeGrudve = gameObject.GetComponent<BacanjeGrudve> ();
		bacnjeGrudve.isBot = true;
		GrudveBroj = 5f;
		h = gameObject.GetComponent<Health> ();
		h.isBot = true;
	}

	GameObject Closest(){
		GameObject[] enemys;
		enemys = GameObject.FindGameObjectsWithTag ("MyPlayer");
		GameObject closest = null;
		float distance= Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in enemys){
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distance){
				closest=go;
				distance=curDistance;
			}
		}
		closestEnemy = closest;
		return closest;
	}

	GameObject ClosestHP(){
		GameObject[] HPs;
		HPs = GameObject.FindGameObjectsWithTag ("HP");
		GameObject closestHealth = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject hp in HPs) {
			Vector3 diff = hp.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closestHealth = hp;
				distance = curDistance;
			}
		}
		closestHP = closestHealth;
		return closestHealth;
	}

	public void Damage(){
		HealthPoints = HealthPoints - 20f;
	}

	void Update(){
		text.text= HealthPoints.ToString();

		if (isMoving) {
			anim.SetFloat ("Speed", 1f);
		} else {
			anim.SetFloat ("Speed", 0f);
		}

		if (HealthPoints <= 0f) {
			Die ();
		}


	}


	void FixedUpdate(){
		
		ClosestHP ();
		Closest ();


		if (h.HealthPoints < 20)
		{
			HealthDistance = Vector3.Distance (closestHealth.transform.position, transform.position);
			Quaternion rotation = Quaternion.LookRotation (closestHealth.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
			Move();

		}



		fpsTargetDistance = Vector3.Distance (closestEnemy.transform.position, transform.position);

			if (fpsTargetDistance <= enemyLookDistance) {
				lookAtPlayer ();
			}
	
		if (GrudveBroj < 0f) {
			Sakupljanje ();
		}

		if (fpsTargetDistance <= 20f) {
			isMoving = false;

			Bacanje ();
			Debug.Log (GrudveBroj);
		}
			 else {
				if (fpsTargetDistance <= attackDistance) {
				Move ();
				} 
			}

	}
	void lookAtPlayer(){
	Quaternion rotation = Quaternion.LookRotation (closestEnemy.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
	}
	void Move(){
		transform.position += transform.forward*enemyMovementSpeed*Time.deltaTime;
		isMoving = true;
	}
	void Bacanje(){
		if (GrudveBroj < 0f) {
			anim.SetBool ("BacanjeSkok", false);		
		} else {
			if (closestEnemy.GetComponent<Health> ().HealthPoints >= 0f) {
				anim.SetBool ("BacanjeSkok", true);
				isMoving = false;
				
			}
		}
	}

	void Sakupljanje(){
		anim.SetBool ("Sakupljanje", true);
		GrudveBroj++;
	}

	public void Die(){
		
			netManager.botRespawnerTime = 0.1f;
			PhotonNetwork.Destroy (gameObject);
		
	}
}

