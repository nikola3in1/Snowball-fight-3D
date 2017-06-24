using UnityEngine;
using System.Collections;

public class OstaciOdSnega : MonoBehaviour {
	public float ostaciLifespan = 10.0f;
	private ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps=GetComponent <ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		ostaciLifespan -= Time.deltaTime;
		if (ostaciLifespan <= 0) {
			Destroy (gameObject);
		}

	}
}
