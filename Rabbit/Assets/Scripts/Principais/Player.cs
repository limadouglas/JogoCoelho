using UnityEngine;
using System.Collections; 
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	public float velocidade;
	public float saltoAltura;
	public float saltoDistancia;
	public Transform chaoVerificador;
	private float raioChao;
	public LayerMask layerColisao;
	private Animator anim;
	public float posicaoInicial;
	private GameObject gameEngine;
	public Camera cam;
	Vector2 tela;				// dimenções da tela.
	private bool estaNoChao;	// verifica se o gameObject está no chão.
	private bool podePular;


	void Start () {

		// instanciando gameEngine.
		gameEngine = GameObject.FindGameObjectWithTag ("GameEngine");

		// instanciando animator.
		anim = GetComponent<Animator>();

		raioChao = 0.001f;

		// resetando controles.
		CrossPlatformInputManager.SetAxisZero ("Horizontal");
		CrossPlatformInputManager.SetButtonUp ("Jump");

		// defindo posicao inicial do Player.

		// convertendo screen width e height para world.
		tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );

		if (PlayerPrefs.GetFloat("checkpoint") <= 19 ) {
			transform.position = new Vector2 (-(tela.x-4), 0);		// definindo posição da player.
			posicaoInicial = transform.position.x;	// salvando posicao inicial para que o personagem não saia da tela pela esquerda.
			PlayerPrefs.SetFloat("posicaoinicial", -(tela.x-4));				// gravando posicao inicial;
			PlayerPrefs.SetFloat("checkpoint", -(tela.x-4));
		} else
			transform.position = new Vector2 (PlayerPrefs.GetFloat("checkpoint"), 0);
	}



	void FixedUpdate () {

		// verificando se o objeto está colidindo com o chao em um raio de '0.2f'.
		estaNoChao = Physics2D.OverlapCircle (chaoVerificador.position, raioChao, layerColisao);

		// aplicando animação.
		anim.SetBool ("Chao", estaNoChao);

		if (CrossPlatformInputManager.GetAxis ("Horizontal") != 0)		// verificando se alguma seta foi pressionada.
				mover ();
		else
			anim.SetFloat ("Velocidade", 0);							// desabilitando animação de andar.

		if (estaNoChao) {													// verificando se o objeto esta no chao;	

			// verificando por eixo horizontal e vertical.
			if (CrossPlatformInputManager.GetButton ("Jump")) 		 	//Input.GetTouch (Input.touchCount-1).position.x >= (Screen.width / 2) 
					movePula ();				
				
		}


		// reposicionando player ao chegar nos extremos do jogo.
		if (transform.position.x > (185 + (Screen.width / 100) / 2)) 					 // não deixa o gameObject ultrapassar a tela do lado direito.
				transform.position = new Vector2 ((185 + (Screen.width / 100) / 2), transform.position.y);	// reposicionando gameObjet.
		else if (transform.position.x < posicaoInicial) 							 // não deixa o gameObject ultrapassar a tela do lado esquerdo.
				transform.position = new Vector2 (posicaoInicial, transform.position.y); // reposicionando gameObjet.
		

	}


	// metodo mover na horizontal onde quando recebe 1 se move para frente, ao receber -1 se move para trás.
	void mover() {

		// ativando animação de andar.
		anim.SetFloat("Velocidade", 1);

		// alterando lado do player.
		if( CrossPlatformInputManager.GetAxis("Horizontal") != transform.localScale.x )	// apenas quando lado e scale tiverem sinais diferentes.
			transform.localScale =  new Vector2(-transform.localScale.x, transform.localScale.y) ;	// espelhando imagem.

		transform.Translate ( (Vector2.right * velocidade * CrossPlatformInputManager.GetAxis("Horizontal")) * Time.deltaTime);	// movimentando gameObjet.

	}
				

	// metodo responsalvel pelo pulo do objeto.
	void movePula() {

		// zerando velocidade para o pulo sempre ser da mesma velocidade e altura.
		GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		
		//GetComponent<Rigidbody2D> ().AddForce (new Vector2 (saltoDistancia * CrossPlatformInputManager.GetAxis("Horizontal"), saltoAltura));	
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((saltoDistancia * 1) * Time.deltaTime, saltoAltura * Time.deltaTime));	

		// ativando animação de pulo.
		anim.SetBool ("Chao", false);

		gameEngine.SendMessage ("somPulo");

	}


	// detectando colisões e finalizando o jogo.
	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Inimigo" || coll.gameObject.tag == "Animacao") {

			gameEngine.SendMessage ("jogoFim");
		}

	}


	void OnTriggerExit2D(Collider2D coll) {

		if (coll.gameObject.tag == "JogadorGanhou") {
			gameEngine.SendMessage ("jogadorGanhou");
		}

		if (coll.gameObject.tag == "CheckPoint") {
			if(PlayerPrefs.GetFloat("checkpoint") < transform.position.x)		// verificação para não gravar checkpoint anteriores, já que é possivel voltar na fase(ir para trás.).
				PlayerPrefs.SetFloat("checkpoint", transform.position.x);
		}

	}



}
