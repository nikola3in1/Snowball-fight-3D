using UnityEngine;
using System.Collections;

public class UIAlign : MonoBehaviour {

	Vector3 panelPlacement;

	// Use this for initialization
	void Start () {
		if (Screen.width == 640 && Screen.height == 480) {
			panelPlacement = new Vector3 (-1, Screen.height / 6.2f, 0);

		} else {
			panelPlacement = new Vector3 (-1,Screen.height/6.6f,0);

		}
		gameObject.GetComponent<RectTransform> ().position = panelPlacement;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
