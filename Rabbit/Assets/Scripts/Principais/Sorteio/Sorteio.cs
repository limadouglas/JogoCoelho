using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorteio : MonoBehaviour {

	public GameObject seletorUm;
	public GameObject seletorDois;

	private GameObject gameEngine;
	private float velocidadeEfeito;

	int numero;

	// Use this for initialization
	void Start () {

		gameEngine = GameObject.Find ("gameEngine");

		seletorUm.SetActive (false);
		seletorDois.SetActive (false);

		velocidadeEfeito = 0.1f;

		StartCoroutine (efeito());
	}



	void Update () {

	}
		
	public void perdeuVida() {
		Destroy (gameObject);
		GameObject.Find ("gameEngine").SendMessage ("msgPerdeu", "Fim de Jogo");
	}



	public void ganhouVida() {
		Destroy (gameObject);
		PlayerPrefs.SetInt ("vida", 1);
		GameObject.Find ("gameEngine").SendMessage ("msgPerdeu", "Ganhou uma Vida");
	}


		
	IEnumerator efeito() {
		
		yield return new WaitForSeconds (0.75f);

		while (velocidadeEfeito < 0.35f) {
			print (velocidadeEfeito);
			if (seletorUm.activeSelf) {
				seletorUm.SetActive (false);
				seletorDois.SetActive (true);
			} else {
				seletorDois.SetActive (false);
				seletorUm.SetActive (true);
			}
			gameEngine.SendMessage ("sorteioSomBase");
			yield return new WaitForSeconds (velocidadeEfeito);
			velocidadeEfeito += 0.0095f;
		}

		numero = Random.Range (0, 10);

		if (numero <= 6) {

			print ("ganhou a vida: " + numero.ToString ());
			seletorDois.SetActive (false);
			seletorUm.SetActive (true);

			gameEngine.SendMessage ("sorteioSomVida");

			for (int i = 0; i < 4; i++) {
				yield return new WaitForSeconds (0.3f);
				seletorUm.SetActive (false);
				yield return new WaitForSeconds (0.3f);
				seletorUm.SetActive (true);
			}
				
			yield return new WaitForSeconds (0.8f);
			ganhouVida ();

		} else {

			print ("perdeu a vida: " + numero.ToString ());
			seletorUm.SetActive (false);
			seletorDois.SetActive (true);

			gameEngine.SendMessage ("sorteioSomDerrota");

			for (int i = 0; i < 4; i++) {
				yield return new WaitForSeconds (0.3f);
				seletorDois.SetActive (false);
				yield return new WaitForSeconds (0.3f);
				seletorDois.SetActive (true);
			}
				
			yield return new WaitForSeconds (0.8f);
			perdeuVida ();

		}
	}

}
