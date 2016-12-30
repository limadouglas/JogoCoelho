using UnityEngine;
using System.Collections;

public class GerarNovaParticula : MonoBehaviour {

	public GameObject particula;

	void Update () {
		if (GameObject.Find ("Player").transform.position.y < -2.3) {
			Instantiate (particula).transform.position = GameObject.Find("Player").transform.position;
		}
	}
}
