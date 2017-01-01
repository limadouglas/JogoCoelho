using UnityEngine;
using System.Collections;

public class GerarNovaParticula : MonoBehaviour {

	public GameObject particula;
	private bool parar;

	void Start() { 
		parar = false;
	}

	void Update () {
		if (GameObject.Find ("Player").transform.position.y < -2.3 && !parar) {
			Instantiate (particula).transform.position = GameObject.Find("Player").transform.position;
			parar = true;
		}
	}
}
