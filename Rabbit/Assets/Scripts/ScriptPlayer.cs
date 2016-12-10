using UnityEngine;
using System.Collections; 

public class ScriptPlayer : MonoBehaviour {

	public float velocidade;
	public float saltoAltura;
	public float saltoDistancia;
	public Transform chaoVerificador;
	private float raioChao;
	public LayerMask layerColisao;
	private int direcaoPulo;
	private bool chamarCoroutine;
	private Animator anim;
	public float posicaoInicial;


	Vector2 tela;				// dimenções da tela.

	private bool estaNoChao;	// verifica se o gameObject está no chão.

	void Start () {

		anim = GetComponent<Animator>();

		raioChao = 0.2f;
		direcaoPulo = 0;

		chamarCoroutine = true;

		// defindo posicao inicial do Player.

		// convertendo screen width e height para world.
		tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );
		transform.position = new Vector2 (-(tela.x-2), -(tela.y-1.2f));		// definindo posição da player.
		posicaoInicial = transform.position.x;	// salvando posicao inicial para que o personagem não saia da tela pela esquerda.


	}

	void FixedUpdate ()
	{

		// verificando se o objeto está colidindo com o chao em um raio de '0.2f'.
		estaNoChao = Physics2D.OverlapCircle (chaoVerificador.position, raioChao, layerColisao);

		// aplicando animação.
		anim.SetBool ("Chao", estaNoChao);
	


		if (estaNoChao) {												// verificando se o objeto esta no chao;	

			// verificando toque na tela.
			/*
			if (Input.GetTouch (Input.touchCount-1).position.y >= (Screen.height / 2)) 	// verificando se o toque foi no na parte superior(PULO).	
				movePula ();
			else if (Input.GetTouch (Input.touchCount-1).position.x >= (Screen.width / 2))	// verificando se o toque foi na parte da frente(DIREITA).
				mover (1);
			else if (Input.GetTouch (Input.touchCount-1).position.x < (Screen.width / 2))	// verificando se o toque foi na parte de trás(ESQUEDA).
					mover (-1);
			else
				anim.SetFloat("Velocidade", 0);
			*/

			// verificando pelas setas do teclado

			if(Input.GetKey(KeyCode.UpArrow))
				movePula ();
			else if (Input.GetKey(KeyCode.RightArrow))	// verificando se a seta foi na parte da frente(DIREITA).
				mover (1);
			else if (Input.GetKey(KeyCode.LeftArrow))	// verificando se a seta foi na parte de trás(ESQUEDA).
				mover (-1);
			else
				anim.SetFloat("Velocidade", 0);

		}



		if (transform.position.x > 182) 							// não deixa o gameObject ultrapassar a tela do lado direito.
			transform.position = new Vector2( 182, transform.position.y);	// reposicionando gameObjet.
		else if (transform.position.x < posicaoInicial) 					// não deixa o gameObject ultrapassar a tela do lado esquerdo.
			transform.position = new Vector2( posicaoInicial, transform.position.y);	// reposicionando gameObjet.

	}


	// metodo mover na horizontal onde quando recebe 1 se move para frente, ao receber -1 se move para trás.
	void mover(int lado) {
		
		anim.SetFloat("Velocidade", 1);

		// alterando lado do player.
		if( lado != transform.localScale.x )	// apenas quando lado e scale tiverem sinais diferentes.
			transform.localScale =  new Vector2(-transform.localScale.x, transform.localScale.y) ;	// espelhando imagem.

		transform.Translate ( (Vector2.right * velocidade * lado) * Time.deltaTime);	// movimentando gameObjet.
		if (chamarCoroutine)
			StartCoroutine (definirDirecaoPulo(lado));
	}
				

	// metodo responsalvel pelo pulo do objeto.
	void movePula() {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (saltoDistancia*direcaoPulo, saltoAltura));
		anim.SetBool ("Chao", false);
	}


	private IEnumerator definirDirecaoPulo(int lado) {
		direcaoPulo = lado;
		chamarCoroutine = false;
		yield return new WaitForSeconds (0.3f);
		direcaoPulo = 0;
		chamarCoroutine = true;
	}



}
