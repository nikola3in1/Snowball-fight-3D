using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PauseMenu : Photon.MonoBehaviour {
	NetworkManager netManager;
	CharacterController charCtlr;
	Health h;
	FirstPersonController fps;
	Chrosshair aim;
	BacanjeGrudve bacnaje;
	GameObject pauseCam;
	public GameObject main;
	bool activated;




	void Start () {
		netManager = GameObject.FindObjectOfType<NetworkManager> ();
		charCtlr = gameObject.GetComponent<CharacterController> ();
		h = gameObject.GetComponent<Health> ();
		fps = gameObject.GetComponent<FirstPersonController> ();
		aim = gameObject.GetComponentInChildren<Chrosshair> ();
		bacnaje = gameObject.GetComponent<BacanjeGrudve> ();

	}
	
	void Update () {

		if (netManager.PauseMenuGUI.activeSelf == false) {
			netManager.PlayerGUI.SetActive (true);
			aim.enabled = true;
			bacnaje.enabled = true;
			charCtlr.enabled = true;
			Cursor.visible = (false);
			fps.enabled = true;
		}

	
		if (netManager.PauseMenuGUI.activeSelf == false) {
			if (Input.GetKeyDown (KeyCode.Escape) ) {
				netManager.PauseMenuGUI.SetActive (true);
				netManager.PauseCam.SetActive (true);
				netManager.PlayerGUI.SetActive (false);
				main.SetActive (true);
				charCtlr.enabled = false;
				aim.enabled = false;
				bacnaje.enabled = false;
				Cursor.visible = (true);
				}
		
		}

		else if (netManager.PauseMenuGUI.activeSelf == true ){
			if (Input.GetKeyDown (KeyCode.Escape)) {
				main.SetActive (true);
				netManager.PauseMenuGUI.SetActive (false);
				netManager.PauseCam.SetActive (false);
				netManager.PlayerGUI.SetActive (true);
				charCtlr.enabled = true;
				aim.enabled = true;
				bacnaje.enabled = true;
				Cursor.visible = (false);
				}
		}
			
		if (netManager.PauseMenuGUI.activeSelf == true) {
			Cursor.visible = (true);
			fps.enabled = false;
		}

	}


	public void QuitGame(){
		h.disconnnect ();
		Application.Quit();	
	}

	public void BackToMainMenu(){
		netManager.chatMessages.Clear ();
		netManager.PlayerGUI.SetActive (false);
		h.disconnnect ();
		PhotonNetwork.LeaveRoom ();
		Debug.Log ("LeaveRoom");
		PhotonNetwork.LeaveLobby ();
		Debug.Log ("LeaveLobby");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
