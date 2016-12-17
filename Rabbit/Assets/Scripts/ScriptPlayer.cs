using UnityEngine;
using System.Collections; 
using UnityStandardAssets.CrossPlatformInput;

public class ScriptPlayer : MonoBehaviour {

	public float velocidade;
	public float saltoAltura;
	public float saltoDistancia;
	public Transform chaoVerificador;
	private float raioChao;
	public LayerMask layerColisao;
	private Animator anim;
	public float posicaoInicial;
	private GameObject gameEngine;
	private bool iniciarJogo;
	private static bool acabouJogo;
	public Camera cam;
	Vector2 tela;				// dimenções da tela.
	private bool estaNoChao;	// verifica se o gameObject está no chão.


	void Start () {

		// instanciando gameEngine.
		gameEngine = GameObject.FindGameObjectWithTag ("GameEngine");

		// variaveis de controle do jogo.
		iniciarJogo = true;
		acabouJogo = false;

		// instanciando animator.
		anim = GetComponent<Animator>();

		raioChao = 0.2f;

		// resetando controles.
		CrossPlatformInputManager.SetAxisZero ("Horizontal");
		CrossPlatformInputManager.SetButtonUp ("Jump");

		// defindo posicao inicial do Player.

		// convertendo screen width e height para world.
		tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );
		transform.position = new Vector2 (-(tela.x-4), -(tela.y/2));		// definindo posição da player.
		posicaoInicial = transform.position.x;	// salvando posicao inicial para que o personagem não saia da tela pela esquerda.

		if(PlayerPrefs.GetFloat("checkpoint") <= 19 )
			PlayerPrefs.SetFloat("posicaoinicial", posicaoInicial);				// gravando posicao inicial;
		else
			transform.position = new Vector2 (PlayerPrefs.GetFloat("checkpoint") + 3, -(tela.y/2));
	}



	void FixedUpdate () {

		if (iniciarJogo && Input.GetButtonDown("Fire1"))
			chamarJogoInicio ();

		// verificando se o objeto está colidindo com o chao em um raio de '0.2f'.
		estaNoChao = Physics2D.OverlapCircle (chaoVerificador.position, raioChao, layerColisao);

		// aplicando animação.
		anim.SetBool ("Chao", estaNoChao);
	

		if(!acabouJogo){													// verificando se o jogo acabou, para os controles pararem. 
			
			if (estaNoChao) {													// verificando se o objeto esta no chao;	

				// verificando por eixo horizontal e vertical.
				if (CrossPlatformInputManager.GetButtonDown ("Jump")) {			 	//Input.GetTouch (Input.touchCount-1).position.x >= (Screen.width / 2) 
					movePula ();
					gameEngine.SendMessage ("somPulo");
				}
				
				if (CrossPlatformInputManager.GetAxis ("Horizontal") != 0)	// verificando se alguma seta foi pressionada.
						mover ();
				else
					anim.SetFloat ("Velocidade", 0);							// desabilitando animação de andar.
			}


			// reposicionando player ao chegar nos extremos do jogo.
			if (transform.position.x > (185 + ScriptUtil.tela.x/2)) 					 // não deixa o gameObject ultrapassar a tela do lado direito.
					transform.position = new Vector2( (185 + ScriptUtil.tela.x/2) , transform.position.y);	// reposicionando gameObjet.
			else if (transform.position.x < posicaoInicial) 							 // não deixa o gameObject ultrapassar a tela do lado esquerdo.
				transform.position = new Vector2( posicaoInicial, transform.position.y); // reposicionando gameObjet.
		}

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
		
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (saltoDistancia * CrossPlatformInputManager.GetAxis("Horizontal"), saltoAltura));	
		//GetComponent<Rigidbody2D> ().AddForce (new Vector2 (saltoDistancia * 1, saltoAltura));	

		// ativando animação de pulo.
		anim.SetBool ("Chao", false);
	}
		

	// iniciando o jogo.
	void chamarJogoInicio() {
		gameEngine.SendMessage ("jogoInicio");
		iniciarJogo = false;
	}


	// detectando colisões e finalizando o jogo.
	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Inimigo" || coll.gameObject.tag == "Animacao") {

			gameEngine.SendMessage ("jogoFim");
			gameEngine.SendMessage ("somMorte");
			acabouJogo = true;
		}

			
		if (coll.gameObject.tag == "JogadorGanhou") {
			gameEngine.SendMessage ("msgGanhou");
			acabouJogo = true;
		}
	}


	void OnTriggerExit2D(Collider2D coll) {

		if (coll.gameObject.tag == "CheckPoint") {
			PlayerPrefs.SetFloat("checkpoint", transform.position.x);
		}

	}



}
