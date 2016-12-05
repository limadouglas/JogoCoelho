using UnityEngine;
using System.Collections; 

public class scriptBola : MonoBehaviour {

	public float velocidade;
	public float saltoAltura;
	public float saltoDistancia;
	public Transform chaoVerificador;
	private float raioChao;
	public LayerMask layerColisao;
	private int direcaoPulo;
	private bool chamarCoroutine;


	Vector2 tela;				// dimenções da tela.

	private bool estaNoChao;	// verifica se o gameObject está no chão.

	void Start () {

		// defindo posicao inicial da bola.

		// convertendo screen width e height para world.
		tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );
		transform.position = new Vector2 (-(tela.x-2), -(tela.y-1.2f));		// definindo posiçã da bola.
		
		raioChao = 0.2f;
		direcaoPulo = 0;
		chamarCoroutine = true;

	}

	void FixedUpdate ()
	{

		// verificando se o objeto está colidindo com o chao em um raio de '0.2f'.
		estaNoChao = Physics2D.OverlapCircle (chaoVerificador.position, raioChao, layerColisao);
	
		// verificando toque na tela.

		if (estaNoChao) {												// verificando se o objeto esta no chao;	

			if (Input.GetTouch (Input.touchCount-1).position.y >= (Screen.height / 2) ) 	// verificando se o toque foi no na parte superior(PULO).
					movePula ();
			else if (Input.GetTouch (Input.touchCount-1).position.x >= (Screen.width / 2))	// verificando se o toque foi na parte da frente(DIREITA).
					mover (1);
			else if (Input.GetTouch (Input.touchCount-1).position.x < (Screen.width / 2))	// verificando se o toque foi na parte de trás(ESQUEDA).
					mover (-1);

		}


		if (transform.position.x > tela.x -1) 							// não deixa o gameObject ultrapassar a tela do lado direito.
			transform.position = new Vector2( tela.x-1, transform.position.y);	// reposicionando gameObjet.
		else if (transform.position.x < -(tela.x-1)) 					// não deixa o gameObject ultrapassar a tela do lado esquerdo.
			transform.position = new Vector2( -(tela.x-1), transform.position.y);	// reposicionando gameObjet.
		

	}


	// metodo mover na horizontal onde quando recebe 1 se move para frente, ao receber -1 se move para trás.
	void mover(int lado) {
		transform.Translate ( (Vector2.right * velocidade * lado) * Time.deltaTime);

		if (chamarCoroutine)
			StartCoroutine (definirDirecaoPulo(lado));
	}
				

	// metodo responsalvel pelo pulo do objeto.
	void movePula() {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (saltoDistancia*direcaoPulo, saltoAltura));
	}


	private IEnumerator definirDirecaoPulo(int lado) {
		direcaoPulo = lado;
		chamarCoroutine = false;
		yield return new WaitForSeconds (0.3f);
		direcaoPulo = 0;
		chamarCoroutine = true;
	}


}
