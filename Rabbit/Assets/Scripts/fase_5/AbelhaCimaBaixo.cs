using UnityEngine;
using System.Collections;

public class AbelhaCimaBaixo : MonoBehaviour {

	public float distanciaMover;
	public float velocidade;
	public float iniciarTempo;
	private Vector2 posicaoInicial;
	private float direcao;
	private bool iniciar;
	private bool parar;

	void Start () {
		direcao = -1;
		posicaoInicial = transform.position;
		iniciar = false;
		parar = false;
		Invoke ("iniciarObjeto", iniciarTempo);
	}

	void Update () {
		if (!parar) {
			if (iniciar) {
				if (transform.position.y <= (posicaoInicial.y - distanciaMover)) {
					direcao = 1;
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, velocidade * direcao);
				} else if (transform.position.y >= (posicaoInicial.y + distanciaMover)) {
					direcao = -1;
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, velocidade * direcao);
				} else
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, velocidade * direcao);
			}

		}
	}

	void iniciarObjeto(){
		iniciar = true;
	}

	public void alterarEstadoObstaculo() {
		parar = true;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	}
}

