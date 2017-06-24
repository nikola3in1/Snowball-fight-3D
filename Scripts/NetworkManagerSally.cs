using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class NetworkManagerSally : Photon.MonoBehaviour {
	public GameObject standByCamera;
	public GameObject menuModel;
	public GameObject menuModel2;
	public GameObject menuModel3;
	public GameObject standByLight;
	public	GUIText nana;
	SpawnSpot[] spawnSpots ;
	bool connecting = false;
	public bool offlineMode = false;
	List<string> chatMessages;
	int maxChatMessages = 5;

	public void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		//Connect ();
		connecting = true;
		chatMessages = new List<string> ();
	}

	public void TextChanged	(string newString){
		PhotonNetwork.player.name = newString;
	}

	void OnDestroy(){
		PlayerPrefs.SetString ("Username",PhotonNetwork.player.name);
	}
	public void AddChatMessages(string m){
		GetComponent<PhotonView> ().RPC ("AddChatMessage_RPC", PhotonTargets.AllBuffered, m);
	}

	[PunRPC]
	void AddChatMessage_RPC(string m){
		while (chatMessages.Count >= maxChatMessages) {
			chatMessages.RemoveAt (0);
		}
		chatMessages.Add (m);
	}


	public void Connect (){
		//		if (offlineMode) {
		//		PhotonNetwork.offlineMode = true;
		//		OnJoinedLobby ();
		//	} 
		//		else {
		PhotonNetwork.ConnectUsingSettings ("0.1");
		PhotonNetwork.JoinLobby ();
		OnJoinedLobby ();

	}



	public void debug(){
		Debug.Log (PhotonNetwork.player.name);
	}

	/*public void singlePlayer(){
		PhotonNetwork.offlineMode = true;
		OnJoinedLobby();
	}*/

	public void OnGUI (){
		if (PhotonNetwork.connected == false && connecting== true) {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

		} 

		if (PhotonNetwork.connected ==true && connecting ==false){	
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();

			foreach (string msg in chatMessages){
				GUILayout.Label(msg);
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}

	}


	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();


	}


	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
		Debug.Log ("dadaa");
	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		connecting = false;
		SpawnMyPlayer ();
	}
	void SpawnMyPlayer() {
		AddChatMessages("Spawning player: " + PhotonNetwork.player.name);

		if (spawnSpots == null) {
			Debug.LogError ("WTF?!?!");
			return;
		}
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		GameObject myPlayerGO = (GameObject) PhotonNetwork.Instantiate ("CharacterGirl",mySpawnSpot.transform.position,mySpawnSpot.transform.rotation, 0);

		/*
		myPlayerGO.layer = 9;
		myPlayerGO.GetComponentInChildren<LayerChange> ().enabled = true;
		myPlayerGO.GetComponentInChildren<NewLayerChange> ().enabled = true;
		myPlayerGO.GetComponentInChildren<LayerChanger1> ().enabled = true;
		myPlayerGO.GetComponentInChildren<LayerChanger2> ().enabled = true;
		myPlayerGO.GetComponentInChildren<LayerChanger3> ().enabled = true;
		myPlayerGO.GetComponentInChildren<LayerChanger4> ().enabled = true;
		myPlayerGO.GetComponentInChildren<LayerChanger5> ().enabled = true;
		myPlayerGO.transform.FindChild ("Hitboxes").gameObject.SetActive (false);
*/
		myPlayerGO.GetComponentInChildren<Camera> ().enabled = true;



		//GameObject myGrudva = (GameObject)PhotonNetwork.Instantiate ("grudva", myPlayerGO.transform.position + myPlayerGO.transform.forward, myPlayerGO.transform.rotation, 0);
		standByCamera.SetActive(false);
		menuModel.SetActive (false);
		standByLight.SetActive (false);
		myPlayerGO.GetComponent<PlayerMovement> ().enabled = true;
		myPlayerGO.GetComponent<FirstPersonController> ().enabled = true;
		//myPlayerGO.GetComponent<CharacterController> ().enabled = true;
		myPlayerGO.GetComponent<BacanjeGrudve> ().enabled = true;
		myPlayerGO.GetComponent<Health> ().enabled = true;
		//myPlayerGO.GetComponent<Grudva> ().enabled = true;
		//myGrudva.GetComponent<GlassBreaker> ().enabled = true;
		myPlayerGO.GetComponentInChildren<AudioListener>().enabled=true;
		myPlayerGO.GetComponentInChildren<Chrosshair>().enabled=true;
		myPlayerGO.tag = "MyPlayer";



	}
}	
