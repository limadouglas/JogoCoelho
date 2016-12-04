using UnityEngine;
using System.Collections; 

public class scriptBola : MonoBehaviour {

	public float velocidade;
	public float saltoAltura;
	public float saltoDistancia;
	public Transform chaoVerificador;


	Vector2 tela;				// dimenções da tela.

	private bool estaNoChao;	// verifica se o gameObject está no chão.

	void Start () {

		// defindo posicao inicial da bola.

		// convertendo screen width e height para world.
		tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );
		transform.position = new Vector2 (-(tela.x-2), -(tela.y-1.2f));		// definindo posiçã da bola.

	}

	void FixedUpdate () {

		if (   Physics2D.Linecast (  transform.position, chaoVerificador.position, 1 << LayerMask.NameToLayer("LayerChao")  )  )
			estaNoChao = true;
		else
			estaNoChao = false;

		// verificando toque na tela.

		if ( estaNoChao ) {												// verificando se o objeto esta no chao;	


			if ((Input.touches.Length <= 0))								// verificando se a tela não esta sendo pressionada.
				moveParar ();

			if (Input.GetTouch (0).position.y >= (Screen.height / 2)) 		// verificando se o toque foi no na parte superior(PULO).
				movePula ();
			else if (Input.GetTouch (0).position.x >= (Screen.width / 2))	// verificando se o toque foi na parte da frente(DIREITA).
				moveFrente ();
			else if (Input.GetTouch (0).position.x < (Screen.width / 2))	// verificando se o toque foi na parte de trás(ESQUEDA).
				moveTras ();

			if ((Input.touches.Length <= 0))								// verificando se a tela não esta sendo pressionada.
				moveParar ();
		}


		if (transform.position.x > tela.x -1) 							// não deixa o gameObject ultrapassar a tela do lado direito.
			transform.position = new Vector2( tela.x-1, transform.position.y);	// reposicionando gameObjet.
		else if (transform.position.x < -(tela.x-1)) 					// não deixa o gameObject ultrapassar a tela do lado esquerdo.
			transform.position = new Vector2( -(tela.x-1), transform.position.y);	// reposicionando gameObjet.
		

	}



	void moveFrente() {
			//transform.Translate (Vector2.right * velocidade * Time.deltaTime);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocidade * Time.deltaTime , 0);
	}



	void moveTras() {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade * Time.deltaTime), 0);
		//transform.Translate (Vector2.left * velocidade * Time.deltaTime);
	}



	void moveParar() {
		
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		// ao reiniciar o 'isKeinematic', o gameObject perde velocidade, força e etc ... 
		GetComponent<Rigidbody2D> ().isKinematic = true;
		GetComponent<Rigidbody2D> ().isKinematic = false;
	}
		


	void movePula() {
		  moveParar();									// zerando para o saldo não ter distancia maior que o desejado.
		  GetComponent<Rigidbody2D> ().AddForce (new Vector2 (saltoDistancia, saltoAltura));	// aplicando força de salto.
		//GetComponent<Rigidbody2D> ().AddForce(transform.up * saltoAltura);
	}


}