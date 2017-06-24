using UnityEngine;
using System.Collections;

public class LayerChanger1 : MonoBehaviour {
	public GameObject head;
	public GameObject hair;
	public GameObject Nick;
	void Start(){
		head.layer = 9;
		hair.layer = 9;
		Nick.layer = 9;
	}


}
