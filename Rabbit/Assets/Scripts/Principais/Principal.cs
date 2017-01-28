using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using AppodealAds.Unity.Android;

public class Principal : MonoBehaviour, IInterstitialAdListener {
	
											// mensagens.
	public GameObject msg;					// gameObject para esconder ou exibir mensagem.
	public Text msgIniciarReiniciar;		// Text para poder alterar texto, cor, tamanho e etc.

	private bool  fim;						// var de controle para saber quando o jogo acabou.

	private GameObject player;				// player.

	private GameObject[] objetos;			// array para todos os inimigos.
											
	public GameObject obstaculo_1;			// instanciando obstaculo.
							
	private GameObject controles;			// gameObject de controle, para esconder e exibir eles. 

	private GameObject[] solos;				// array de solos para controlar o chao.

	// audio player.
	public AudioClip morte;
	public AudioClip pulo;
	public AudioClip vitoria;
	public AudioClip roletaSom;
	public AudioClip roletaVida;
	public AudioClip roletaDerrota;
	public AudioClip encontroFim;


	private Button botaoMusica;			// ativa ou desativa a musica de fundo.
	public Sprite spriteSomAtivado;		// icone de som ativado.
	public Sprite spriteSomDesativado;	// iconde de som desativado.

	// verificador para saber se passou de fase.
	private bool ganhou;

	private bool estaNoInicio;

	public GameObject roleta;

	public bool uma = true;

	public GameObject primeiraVez;
	public bool primeiraVezVerificador;

	string appKey = "efb5c422363727a3a58ef4d0f8a48a2fc5ee91cb4c234f93";

	void Start () {
		primeiraVezVerificador = false;
		msg.transform.position = Vector3.zero;
		// PlayerPrefs.SetInt ("vida", 1);
		// PlayerPrefs.SetInt ("fase", 3);

		ganhou = false;											// ganhou inicia como false.
		fim = false;											// iniciando fim como false.
		estaNoInicio = false;

		player = GameObject.Find ("Player");					// instanciando Player.
		controles = GameObject.Find ("Controles");				// instanciando controle.
		objetos = GameObject.FindGameObjectsWithTag("Inimigo"); // instanciando todos os inimigos em um array.
		solos = GameObject.FindGameObjectsWithTag("Solo"); 		// instanciando todos os solos em um array. fase 2.
		botaoMusica = GameObject.Find("BotaoMusica").GetComponent<Button>();	// instanciando o botao de musica.

		controles.SetActive (false);							// iniciando controles como invisivel.		
		foreach (GameObject obj in objetos) {					// desabilitando todos os obstaculos(inimigos).
			obj.SetActive (false);
		}
																// desabilitando todos os solos, menos o que o jogador esta em cima.
		foreach(GameObject solo in solos) {
			if(solo.name != "Solo")
				solo.SetActive (false);
		}

		if (PlayerPrefs.GetInt ("msgIniciar") == 1 ) {
			if (PlayerPrefs.GetInt ("PrimeiraVez") != 1) {
				primeiraVez.SetActive (true);
				estaNoInicio = true;
				primeiraVezVerificador = true;
				msg.SetActive (false);
				PlayerPrefs.SetInt ("PrimeiraVez", 1);
			} else {
				msg.SetActive (true);	
				PlayerPrefs.SetInt ("msgIniciar", 0);
				estaNoInicio = true;
			}

			if (SceneManager.GetActiveScene ().name == "Cena_2") {
				player.transform.position = new Vector2 (player.transform.position.x, 0.3f);
				GameObject.Find ("Solo").transform.position = new Vector2 (player.transform.position.x, GameObject.Find ("Solo").transform.position.y);	
			}

		} else
			jogoInicio ();

		somAtivadoDesativado ();								// verificando se o jogo ira iniciar com som.

		// propaganda é carregado apenas quando for a ultima vida e estiver conectado na internet.
		if(PlayerPrefs.GetInt("vida") <= 1 && Application.internetReachability != NetworkReachability.NotReachable) {
			Appodeal.disableLocationPermissionCheck();
			Appodeal.cache (Appodeal.INTERSTITIAL);
			Appodeal.initialize(appKey, Appodeal.INTERSTITIAL);
			Appodeal.setInterstitialCallbacks(this);
		}
	}	

	void Update () {

		if (estaNoInicio && Input.GetButtonDown ("Fire1")) {
			jogoInicio ();
			estaNoInicio = false;
			Time.timeScale = 1;	

			if (primeiraVezVerificador)
				primeiraVez.SetActive (false);
		}
		
		if (fim && Input.GetButtonDown ("Fire1")) {								// verificnado se fim = true e teve um toque na tela.
					
			Time.timeScale = 1;													// retornando tempo ao normal.

			if (ganhou) {
				PlayerPrefs.SetInt ("fase", PlayerPrefs.GetInt ("fase") + 1);	// salvando proxima fase.
				PlayerPrefs.SetInt ("vida", PlayerPrefs.GetInt ("vida") + 1);	// dando mais uma vida ao player.
				PlayerPrefs.SetInt ("checkpoint", 0);							// zerando checkPoint.

				switch (SceneManager.GetActiveScene ().name) {

				case "Cena_1":
					SceneController.getInstance().LoadScene("Cena_2");
					//SceneManager.LoadScene ("Cena_2");
					break;
				case "Cena_2":
					SceneController.getInstance().LoadScene("Cena_3");
					//SceneManager.LoadScene ("Cena_3");
					break;
				case "Cena_3":
					SceneController.getInstance().LoadScene("Cena_4");
					//SceneManager.LoadScene ("Cena_4");
					break;
				case "Cena_4":
					SceneController.getInstance().LoadScene("Cena_5");
					//SceneManager.LoadScene ("Cena_5");
					break;
				default		 :
					SceneController.getInstance().LoadScene("Cena_FIM");
					//SceneManager.LoadScene ("Cena_FIM");
					break;
				}
				
			} else if (PlayerPrefs.GetInt ("vida") > 0)
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);	// atualizando a cena.
			else {
				PlayerPrefs.SetFloat("checkpoint", PlayerPrefs.GetFloat("posicaoinicial"));	 // retornando o checkpoint inicial quando todas as vidas acabarem.
				PlayerPrefs.SetInt("fase", 1);									// retornando para fase 1.
				PlayerPrefs.SetInt("vida", 3);									// retornando com as três vidas padrão.

				SceneManager.LoadScene ("Cena_1");								// atualizando a cena.
			}

		}

	}

	void jogoInicio() {

		msg.SetActive (false);												// escondendo msg.
		controles.SetActive (true);											// mostrndo controles.

																			//instanciando obstaculos dependendo da fase.
		switch(SceneManager.GetActiveScene().name) {
		case "Cena_1":
			criarObstaculosAltofase1 ();
						break;
		case "Cena_2":
			criarObstaculosAltofase2 ();
						break;
		case "Cena_3":
			criarObstaculosAltofase3 ();
			break;
		case "Cena_4":
			criarObstaculosAltofase4 ();
			break;
		case "Cena_5":
			criarObstaculosAltofase5 ();
			break;
		}

		//mostrando obstaculos.
		foreach (GameObject obj in objetos)
			obj.SetActive (true);

	}



	void jogoFim() {

		player.GetComponent<Rigidbody2D> ().isKinematic = true; // desabilitando isKinematic do player.

		// parando objetos e animações.

		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Inimigo")) {
			if (go.GetComponent<Rigidbody2D> ()) {
				go.SendMessage("alterarEstadoObstaculo");
			}
		}

		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Animacao")) {
			go.GetComponent<Animator> ().enabled = false;
		}

		/*
		// parando inimigos da fase 3.
		if(SceneManager.GetActiveScene().name == "Cena_3")
			foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Inimigo")) {
				if(go.GetComponent<Rigidbody2D>())
					go.SendMessage ("pararObstaculo");
			}
		*/

		GameObject.Find ("Player").GetComponent<Animator> ().enabled = false;
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		GameObject.Find ("Player").transform.Translate (Vector3.zero);
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().AddForce(Vector3.zero);


		controles.SetActive (false);										// escondendo controles.
																			
		GetComponent<AudioSource> ().clip = morte;							// som de morte. 
		GetComponent<AudioSource> ().loop = false;							// desativando loop.
		GetComponent<AudioSource> ().Play();								// dando play.

		PlayerPrefs.SetInt ("vida", PlayerPrefs.GetInt ("vida") - 1);		// tirando uma vida do player.
			
		if (PlayerPrefs.GetInt ("vida") == 0)								// verificando se as vidas acabaram.
			Invoke ("chamarPropaganda", 1);									// chamando roleta.
		else
			msgPerdeu ("Toque Para Reiniciar");								// chamando metodo com msg de fim.

	}
		

	void msgPerdeu(string textoMsg) {
		fim = true;
		msgIniciarReiniciar.text = textoMsg;
		msg.SetActive (true);
		Time.timeScale = 0;
	}


	void chamarPropaganda () {
		print ("propaganda metodo chamado");
		if(Application.internetReachability == NetworkReachability.NotReachable)
			msgPerdeu ("Sem Internet!");	
		else if(Appodeal.isLoaded(Appodeal.INTERSTITIAL))
			Appodeal.show (Appodeal.INTERSTITIAL);
		else
			roleta = Instantiate (roleta);
	}


	public void onInterstitialLoaded() { print("Interstitial loaded"); }
	public void onInterstitialFailedToLoad() { print("Interstitial failed"); }
	public void onInterstitialShown() { print("Interstitial opened"); }
	public void onInterstitialClosed() { print("Interstitial closed"); roleta = Instantiate (roleta); }
	public void onInterstitialClicked() { print("Interstitial clicked"); roleta = Instantiate (roleta); }


	void roletaSemSom() {
		GetComponent<AudioSource> ().clip = null;
		GetComponent<AudioSource> ().Play();								// dando play.	
	}

	void roletaSomRodar(){
		GetComponent<AudioSource> ().clip = roletaSom;
		GetComponent<AudioSource> ().Play();								// dando play.	
	}

	void roletaSomVida() {
		GetComponent<AudioSource> ().clip = roletaVida;
		GetComponent<AudioSource> ().Play();								// dando play.
	}
	void roletaSomDerrota() {
		GetComponent<AudioSource> ().clip = roletaDerrota;
		GetComponent<AudioSource> ().Play();								// dando play.
	}



	void jogadorGanhou() {
		controles.SetActive (false);										// escondendo controles.
		GetComponent<AudioSource> ().clip = vitoria;						// som de vitoria. 
		GetComponent<AudioSource> ().loop = false;							// desativando loop.
		GetComponent<AudioSource> ().Play();								// dando play.	
		// audio de vitoria.
		//Invoke("desativarSom", 3f);

		ganhou = true;
		fim = true;
		Invoke("msgGanhou", 0.3f);															// invocando metodo mensagem ganhou.
	}
		

	void msgGanhou() {
		msgIniciarReiniciar.text = "Ir para fase " + (PlayerPrefs.GetInt("fase") + 1).ToString();	// alterando msg.
		msg.SetActive (true);											// exibindo msg.
		Time.timeScale = 0;												// parando tempo.			
	}



	// PARTE RELACIONADA AO SOM.

	void somAtivadoDesativado() {

		if (PlayerPrefs.GetInt ("som") == 2) {
			botaoMusica.image.overrideSprite = spriteSomDesativado;
			GetComponent<AudioSource> ().mute = true;
		} else {
			botaoMusica.image.overrideSprite = spriteSomAtivado;
			GetComponent<AudioSource> ().mute = false;
		}
	}


	void alterarEstadoSom() {
		if (PlayerPrefs.GetInt ("som") == 1)
			PlayerPrefs.SetInt ("som", 2);
		else
			PlayerPrefs.SetInt ("som", 1);

		somAtivadoDesativado ();
	}


	void somPulo() {
		GetComponent<AudioSource> ().PlayOneShot (pulo);
	}


	// OBSTACULOS DE DIVERSAS FASES.

	// metodo so será chamado quando estiver na fase 1.
	void criarObstaculosAltofase1() {	

	}


	// metodo so será chamado quando estiver na fase 2.
	void criarObstaculosAltofase2 () {

		// mostando solos.
		foreach(GameObject solo in solos) {
			if(solo.name != "Solo_1")
				solo.SetActive (true);
		}

		player.transform.position = new Vector2 (player.transform.position.x, 1f);
		GameObject.Find("Solo").transform.position = new Vector2(PlayerPrefs.GetFloat("checkpoint"), GameObject.Find("Solo").transform.position.y);
		//GameObject.Find ("Solo").GetComponent<Rigidbody2D> ().isKinematic = true;
	}

	// metodo so será chamado quando estiver na fase 3.
	void criarObstaculosAltofase3 () {
		//TODO
	}

	// metodo so será chamado quando estiver na fase 4.
	void criarObstaculosAltofase4 () {
		//TODO
	}

	// metodo so será chamado quando estiver na fase 5.
	void criarObstaculosAltofase5 () {
		//TODO
	}

	void finalizarJogo() {
		controles.SetActive (false);
		GetComponent<AudioSource> ().clip = encontroFim;
		GetComponent<AudioSource> ().Play();								// dando play.
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Inimigo")) {
			go.SetActive (false);
		}

	}

}
