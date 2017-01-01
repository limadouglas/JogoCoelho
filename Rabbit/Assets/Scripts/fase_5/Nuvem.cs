using UnityEngine;
using System.Collections;

public class Nuvem : MonoBehaviour {

	public float tempo;
	public float tempoDelay;
	public GameObject raio;
	private bool novoRaio;
	private bool parar;

	// Use this for initialization
	void Start () {
		novoRaio = false;
		parar = false;

		Invoke ("delay", tempoDelay);
	}
	
	// Update is called once per frame
	void Update () {

		if (!parar) {
			if (novoRaio) {
				soltarRaio ();
				novoRaio = false;
				StartCoroutine (tempoNovoRaio ());
			}
		}
	
	}

	IEnumerator tempoNovoRaio() {
		yield return new WaitForSeconds (tempo);
		novoRaio = true;
	
	}

	void soltarRaio() {
		Instantiate (raio).transform.position = new Vector2 (transform.position.x, transform.position.y-0.2f);	
	}

	void delay() {
		novoRaio = true;
	}

	public void alterarEstadoObstaculo() {
		parar = true;
	}
}
