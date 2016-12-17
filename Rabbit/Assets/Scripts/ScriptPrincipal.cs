using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ScriptPrincipal : MonoBehaviour {

											// mensagens.
	public GameObject msg;					// gameObject para esconder ou exibir mensagem.
	public Text msgIniciarReiniciar;		// Text para poder alterar texto, cor, tamanho e etc.

	private bool  fim;						// var de controle para saber quando o jogo acabou.


	private GameObject[] objetos;			// array para todos os inimigos.

											// instanciando inimigos alto  1 e 2.
	public GameObject obstaculoAlto_1;
	public GameObject obstaculoAlto_2;
							
	private GameObject controles;			// gameObject de controle, para esconder e exibir eles. 

	// variaveis do sistema de play pause.
	private bool play;						// var de controle para saber se está no play ou pause.
	private GameObject botaoPlayPause;		// gameObject para esconder e exibir.
	public Sprite spritePlay;				// sprite da imagem de play.
	public Sprite spritePause;				// sprite da imagem de pause.



	void Start () {

		play = true;											// iniciando play como verdadeiro.
		botaoPlayPause = GameObject.Find ("BotaoPlayPause");	// intanciando gameObject Play Pause.
		botaoPlayPause.SetActive (false);						// iniciando ele como desativado.

		fim = false;											// iniciando fim como false.
		msg.SetActive (true);									// mostrando gameObject de mensagem. 

		objetos = GameObject.FindGameObjectsWithTag("Inimigo"); // instanciando todos os inimigos em um array.

		controles = GameObject.Find ("Controles");				// instanciando controle.
		controles.SetActive (false);							// iniciando controles como invisivel.

		foreach (GameObject obj in objetos) {					//desabilitando todos os obstaculos(inimigos).
			obj.SetActive (false);
		}

	}
	


	void Update () {

		if( fim && Input.GetButtonDown("Fire1")  ) {										// verificnado se fim = true e teve um toque na tela.
												
			Time.timeScale = 1;																// retornando tempo ao normal.
			SceneManager.LoadScene ("Cena_1");												// atualizando a cena.

			foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Animacao")) {	//retomando animações.
				obj.GetComponent<Animator> ().enabled = true;
			}
				
			GameObject.Find ("Player").GetComponent<Animator> ().enabled = true;			// retomando animação do player.
		}


	}



	void jogoInicio() {
		
		botaoPlayPause.SetActive (true);									// mostrando botao de play pause.
		msg.SetActive (false);												// escondendo msg.
		controles.SetActive (true);											// mostrndo controles.

																			// Instanciando Obstaculos alto 1 e 2.
		Instantiate (obstaculoAlto_1).SendMessage("setPosicaoInicial", 5);  
		Instantiate (obstaculoAlto_2).SendMessage("setPosicaoInicial", 18);

																			//mostrando obstaculos alto.
		foreach (GameObject obj in objetos) 
			obj.SetActive (true);

	}



	void jogoFim() {

		PlayerPrefs.SetInt ("vida", PlayerPrefs.GetInt ("vida") - 1);		// tirando uma vida do player.
																			
		if(PlayerPrefs.GetInt("vida") == 0) {								// verificando se as vidas acabaram.
			PlayerPrefs.SetFloat("checkpoint", PlayerPrefs.GetFloat("posicaoinicial"));	 // retornando o checkpoint inicial quando todas as vidas acabarem.
			PlayerPrefs.SetInt("fase", 1);									// retornando para fase 1.
			PlayerPrefs.SetInt("vida", 3);									// retornando com as três vidas padrão.
		}

		botaoPlayPause.SetActive (false);									// escondendo botao play pause.
		controles.SetActive (false);										// escondendo controles.		
		Invoke ("msgFim", 0);												// chamando metodo com msg de fim.

																			//parando animações de todos inimigos.
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Animacao")) {
			obj.GetComponent<Animator> ().enabled = false;
		}

																			// parando animação do player.
		GameObject.Find ("Player").GetComponent<Animator> ().enabled = false;

	}



	void msgFim() {
		msgIniciarReiniciar.text = "Toque Para Reiniciar";
		msg.SetActive (true);
		fim = true;
		Time.timeScale = 0;
	}

	void msgGanhou() {
		msgIniciarReiniciar.text = "Você Venceu!";						// alterando msg.
		msg.SetActive (true);											// exibindo msg.
		Invoke ("jogadorGanhou", 1.5f);									// invocando metodo ganhou.
	}



	void jogadorGanhou() {
		PlayerPrefs.SetInt ( "vida", PlayerPrefs.GetInt("vida") + 1 );	// dando mais uma vida ao player.
		PlayerPrefs.SetInt ("fase", 2);									// salvando fase 2.
		SceneManager.LoadScene ("Cena_2");								// abrindo fase 2.
	}




	void pausarJogar() {

		if (play) {																		// se play = true, então o jogo será pausado.
			Time.timeScale = 0;															// parando tempo.
			botaoPlayPause.GetComponent<Button> ().image.overrideSprite = spritePlay;	// alterando icone.
		} else {																		// se play = false, então o jogo sera será retomado. 
			Time.timeScale = 1;															// retomando tempo.
			botaoPlayPause.GetComponent<Button> ().image.overrideSprite = spritePause;	// alterando icone.
		}

		play = !play;																	// mudando estado da varivel.									 
	}


}
