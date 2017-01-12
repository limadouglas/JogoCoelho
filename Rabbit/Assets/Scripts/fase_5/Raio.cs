using UnityEngine;
using System.Collections;

public class Raio : MonoBehaviour {

	public GameObject particula;
	public float tempoDestruir;
	public bool parar;

	void Start () {
		parar = false;
		Invoke ("destruirRaio", tempoDestruir);
	}

	void destruirRaio() {
		if (!parar) {
			Destroy (gameObject);
			Instantiate (particula).transform.position = transform.position;
		}
	}

	public void alterarEstadoObstaculo() {
		GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		GetComponent<Rigidbody2D> ().isKinematic = true;
		parar = true;

	}

}
