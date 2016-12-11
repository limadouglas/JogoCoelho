using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptPrincipal : MonoBehaviour {

	public GameObject msg;
	public Text msgIniciarReiniciar;
	private bool  fim;
	private GameObject[] objetos;
	private GameObject obstaculoAlto_1;
	private GameObject obstaculoAlto_2;

	void Start () {
		fim = false;
		msg.SetActive (true);

		objetos = GameObject.FindGameObjectsWithTag("Inimigo");

		// instanciando obstaculos alto, (esta é outra maneira de instanciar, ou seja, pelo nome do gameObject).
		obstaculoAlto_1 = GameObject.Find ("ObstaculoAlto_1");
		obstaculoAlto_2 = GameObject.Find ("ObstaculoAlto_2");

		//desabilitando todos os obstaculos.
		foreach (GameObject obj in objetos) {
			obj.SetActive (false);
		}

	}
	


	void Update () {


		 // touch
		if( fim && Input.GetTouch(0).position.y > - 6 ) {
			
			//retomando animações.
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Animacao")) {
				obj.GetComponent<Animator> ().enabled = true;
			}

			// retomando animação do player.
			GameObject.Find ("Player").GetComponent<Animator> ().enabled = true;

			// atualizando a cena.
			SceneManager.LoadScene ("Scene_1");
		}


		/*
		if( fim && ( Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) )  ) {

			//retomando animações.
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Animacao")) {
				obj.GetComponent<Animator> ().enabled = true;
			}

			// retomando animação do player.
			GameObject.Find ("Player").GetComponent<Animator> ().enabled = true;
				
			// atualizando a cena.
			SceneManager.LoadScene ("Scene_1");
		}
		*/

	}



	void jogoInicio() {
		
		msg.SetActive (false);

		foreach (GameObject obj in objetos) 
			obj.SetActive (true);
		
		obstaculoAlto_1.SendMessage ("retomar");
		obstaculoAlto_2.SendMessage ("retomar");

	}



	void jogoFim() {
		
		Invoke ("msgFim", 0.7f);

		//parando animações de todos inimigos.
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Animacao")) {
			obj.GetComponent<Animator> ().enabled = false;
		}

		// parando animação do player.
		GameObject.Find ("Player").GetComponent<Animator> ().enabled = false;


		//parando movimentação dos obstaculos altos.
		obstaculoAlto_1.SendMessage ("parar");
		obstaculoAlto_2.SendMessage ("parar");
	}



	void msgFim() {
		msgIniciarReiniciar.text = "Toque Para Reiniciar";
		msg.SetActive (true);
		fim = true;
	}

}
