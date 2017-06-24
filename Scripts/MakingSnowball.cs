using UnityEngine;
using System.Collections;

public class MakingSnowball : MonoBehaviour {

	// Use this for initialization
	void GuiON(){
		GameObject.Find ("MakingSnowballGUI").SetActive (true);

	}
	void GuiOFF(){
		gameObject.GetComponentInChildren<MakingSnowball>().enabled=false;
	}
}
