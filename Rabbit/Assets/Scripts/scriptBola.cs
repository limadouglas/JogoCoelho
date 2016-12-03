using UnityEngine;
using System.Collections; 

public class scriptBola : MonoBehaviour {

	public float velocidade;
	public float saltoAltura;
	public float saltoDistancia;

	// 'Screen.width' retorna o tamanho da tela ex: em 824 estou convertendo para 8.24, já que, a posição se define em unidade menores,(olhe o componente Transform).
	// necessario converter para float pois o 'Vector3' não aceita 'double'
	private static float LARGURA_TELA = (float) (Screen.width/100.0);

	Vector2 objetoPosicao;	// posição do objeto em relação a tela.

	void Start () {}

	void FixedUpdate () {

		// convertendo posição do gameObjet de World para Screen, é necessario para dar suporte a diferentes tipo de telas.
		objetoPosicao =  Camera.main.WorldToScreenPoint (transform.position);

		// verificando toque na tela.

		if ( objetoPosicao.y >= (Screen.height - 5) ) {										// verificando se o objeto esta no chao;
			if ((Input.touches.Length <= 0))								// verificando se a tela não esta sendo pressionada.
			moveParar ();
			else if (Input.GetTouch (0).position.y >= (Screen.height / 2)) 	// verificando se o toque foi no na parte superior(PULO).
			movePula ();
			else if (Input.GetTouch (0).position.x >= (Screen.width / 2))	// verificando se o toque foi na parte da frente(DIREITA).
			moveFrente ();
			else if (Input.GetTouch (0).position.x < (Screen.width / 2))	// verificando se o toque foi na parte de trás(ESQUEDA).
			moveTras ();
		}

		// não deixa o gameObject ultrapassar a tela do lado direito.
		if (objetoPosicao.x > Screen.width) 
			transform.position = new Vector3( LARGURA_TELA, transform.position.y, 0);	// reposicionando gameObjet.
		
		// não deixa o gameObject ultrapassar a tela do lado esquerdo.
		else if (objetoPosicao.x < 0) {
			transform.position = new Vector3( -(LARGURA_TELA), transform.position.y, 0);	// reposicionando gameObjet.
		}

	}



	void moveFrente() {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocidade * Time.deltaTime , 0);
	}



	void moveTras() {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade * Time.deltaTime), 0);
	}



	void moveParar() {
		// ao reiniciar o 'isKeinematic', o gameObject perde velocidade, força e etc ... 
			GetComponent<Rigidbody2D> ().isKinematic = true;
			GetComponent<Rigidbody2D> ().isKinematic = false;
	}
		


	void movePula() {
			moveParar();									// zerando para o saldo não ter distancia maior que o desejado.
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (saltoDistancia, saltoAltura));	// aplicando força de salto.
	}


}