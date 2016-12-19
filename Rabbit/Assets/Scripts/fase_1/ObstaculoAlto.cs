using UnityEngine;
using System.Collections;

public class ObstaculoAlto : MonoBehaviour {

	private float velocidade;			// velocidade do gameObjeto.
	private float valorRandomico;
	private Camera cam;
	private float posicaoInicial;



	void Start () {
		cam = GameObject.Find ("Main Camera").GetComponent<Camera>();

		velocidade = 70;

		// definindo velocidade de movimentação do gameObject.
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);
		criaObstaculo (posicaoInicial);
	}



	void FixedUpdate() {

		// verificando se o gameObject está passando do final da tela (ESQUERDA) e o reposiciondo.
		if (transform.position.x <= cam.transform.position.x - ((Screen.width/100) * 1.2f) && cam.transform.position.x < 170) 		// verificando se utrapassou a tela e não chegou ao fim da fase..
			criaObstaculo (5);
	}



	void criaObstaculo(float distancia) {

		// posicionado gameObject baseado no tamanho da tela e numero randomico.
		transform.position = new Vector2 (cam.transform.position.x + (Screen.width/100) + distancia, -2);

	}

	void setPosicaoInicial(int posicaoInicial) {
		this.posicaoInicial = posicaoInicial;
	}
}
