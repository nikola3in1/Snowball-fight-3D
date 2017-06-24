using UnityEngine;
using System.Collections;

public class PauseMenuCamPosition : MonoBehaviour {
	GameObject pauseCamPosition;
	NetworkManager netManager;


	void Start () {
		pauseCamPosition = GameObject.FindGameObjectWithTag ("MyPlayer").transform.FindChild("PauseCamPosition").gameObject;
		netManager = GameObject.FindObjectOfType<NetworkManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (netManager.PauseMenuGUI.activeSelf == true) {
			gameObject.transform.position = pauseCamPosition.transform.position;
			gameObject.transform.rotation = pauseCamPosition.transform.rotation;
		}
	}
}
