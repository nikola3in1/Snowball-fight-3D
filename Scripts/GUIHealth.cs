using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GUIHealth : MonoBehaviour {
	float health;
	public Text guiHealth;
	Health h;
	void Start () {


	}

	

	void Update(){
		health= GameObject.FindGameObjectWithTag("MyPlayer").GetComponent<Health> ().HealthPoints;
		guiHealth.text = health.ToString ();

		if (guiHealth != null) {
			guiHealth.text = health.ToString ();
		}
		//Debug.Log (health);

	}

}

