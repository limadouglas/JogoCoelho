using UnityEngine;
using System.Collections;

public class InimigoAlto : MonoBehaviour {

	public GameObject particula;
	private Vector2 posicaoInicial;
	public float delay;
	private bool delayAcabou;
	private bool parar;



	void Start () {
		parar = false;
		posicaoInicial = transform.position;									// salvando posição inicial do gameObject.
		delayAcabou = false;													// criando uma delay para conseguir configurar melhor os obstaculos.
		Invoke("delaySegundos", delay);											// chamando delaySegundos apos o tempo definido.
	}



	void Update () {
		
		if (delayAcabou) {
			if (!parar) {
				if (transform.position.y <= -3.7f) {										// aumenta a velocidade quando sair do teto.
					particula.transform.position = new Vector2 (transform.position.x, transform.position.y + 1);	// posição da particula baseado na posição deste gameObject.
										
					StartCoroutine (destruirParticula (Instantiate (particula)));			// instanciando particula, e ja mandando ser destruida.
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -1);			// diminuindo velociade.
					transform.position = posicaoInicial;									// reposicionando gameObject.
				} 


				if (transform.position.y < 1.3f && !parar) {										// diminuir velocidade quando estiver antes do teto.
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -7);		// para dar um efeito legal.
				}

			}
		}

	}

	IEnumerator destruirParticula( GameObject part){		// destruindo particula após 5 segundos do instanciamento.
		yield return new WaitForSeconds (5);
		Destroy (part);
	}


	void delaySegundos() {
		delayAcabou = true;															// delayAcabou = true, para iniciar os codigo do gameobject.
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -1);
	}

	public void alterarEstadoObstaculo() {
		parar = true;
		GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
	}
		
}
