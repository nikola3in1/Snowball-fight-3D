using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GrudveLeft : MonoBehaviour {

	public Text grudve; 
	
	// Update is called once per frame
	void Update () {
		grudve.text ="15/" +GameObject.FindGameObjectWithTag ("MyPlayer").GetComponent<BacanjeGrudve> ().currentGrudve.ToString();
	}
}
