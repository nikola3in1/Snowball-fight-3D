using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class killsDebug : MonoBehaviour {
	public Text kills;
	int killovi;
	// Use this for initialization
	void Start () {
		//PhotonNetwork.player.customProperties.Add ("Kills", 0);
		
	}

	
	// Update is called once per frame
	void Update () {
		killovi = PlayerPrefs.GetInt ("kills");
		kills.text = killovi.ToString();
	}
}
