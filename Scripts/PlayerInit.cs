using UnityEngine;
using System.Collections;
using System.Linq;
public class PlayerInit : Photon.MonoBehaviour {
	ScoreManager scoreManager;
	string nick;
	float death,kill;
	string str;
	public string send;
	GameObject scoreBoard;

	void Start () {
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
		GameObject.Find("ScoreBoard").SetActive(true);



		
	}

	public void sender(){
		nick = PhotonNetwork.player.name;
		scoreManager.GetComponent<PhotonView> ().RPC ("reciever", PhotonTargets.AllBufferedViaServer, nick);

	}
}
