using UnityEngine;
using System.Collections;

public class InimigoBaixo : MonoBehaviour {

	public GameObject particula;
	public float delay;
	private bool delayAcabou;
	private int direcao;
	private bool parar;


	void Start () {													// definindo inicio como true para executar apenas uma vez no update.
		delayAcabou = false;													// criando uma delay para conseguir configurar melhor os obstaculos.
		direcao = 1;
		Invoke("delaySegundos", delay);											// chamando delaySegundos apos o tempo definido.
		parar = false;
	}



	void Update () {

		if (delayAcabou) {
			if (!parar) {
				if (transform.position.y >= -4.5f) 									// aumenta a velocidade quando sair do teto.
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 6 * direcao);			// diminuindo velociade.
			else if (transform.position.y < -4.5f)
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 1 * direcao);			// diminuindo velociade.

				if (transform.position.y >= 0.4) {
					direcao = -1;
					transform.localScale = new Vector3 (-1, -1, -1); 
				} else if (transform.position.y <= -5.5) {
					direcao = 1;
					transform.localScale = new Vector3 (1, 1, 1);
				}

			}
		}

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "LagoPeixe" && direcao == -1) {
			particula.transform.position = new Vector2 (transform.position.x, transform.position.y-1);
			StartCoroutine(destruirParticula(Instantiate (particula)));
		}
	}

	IEnumerator destruirParticula(GameObject part) {
		yield return new WaitForSeconds (5);
		Destroy (part);
	}

	void delaySegundos() {
		delayAcabou = true;															// delayAcabou = true, para iniciar os codigo do gameobject.
	}


	public void pararObstaculo() {
		parar = true;
	}
}
