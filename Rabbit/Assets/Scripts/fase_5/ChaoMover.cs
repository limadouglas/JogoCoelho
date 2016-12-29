using UnityEngine;
using System.Collections;

public class ChaoMover : MonoBehaviour {

	public float distanciaMover;
	public float velocidade;
	public float iniciarTempo;
	private Vector2 posicaoInicial;
	private float direcao;
	private bool iniciar;

	void Start () {
		direcao = -1;
		posicaoInicial = transform.position;
		iniciar = false;
		Invoke ("iniciarObjeto", iniciarTempo);
	}

	void Update () {
		
		if (iniciar) {
			if (transform.position.x <= (posicaoInicial.x - distanciaMover)) {
				direcao = 1;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocidade * direcao, 0);
			} else if (transform.position.x >= (posicaoInicial.x + distanciaMover)) {
				direcao = -1;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocidade * direcao, 0);
			} else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocidade * direcao, 0);
		}

	}

	void iniciarObjeto(){
		iniciar = true;
	}
}
