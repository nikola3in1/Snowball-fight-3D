using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NickAboveHead : Photon.MonoBehaviour {
	public TextMesh nick;
	string nickname;

	// Use this for initialization
	void Start () {

		nick.text=photonView.owner.name;

	}
	void Update()
	{
		if (this.nick != null)
			this.nick.transform.forward = Camera.main.transform.forward;
	}
}