using UnityEngine;
using System.Collections;

public class TagChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.tag =  PhotonNetwork.player.name+ "cam";
	}
}
