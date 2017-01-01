using UnityEngine;
using System.Collections;

public class ParticulaRaio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("destruirParticula", 2);
	}
	
	void destruirParticula() {
		Destroy (gameObject);
	}
}
