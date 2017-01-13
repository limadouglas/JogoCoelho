using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour {

	private bool touch;

	public GameObject coracao;
	private GameObject player;
	public GameObject final;
	// Update is called once per frame
	void Update () {
		if (touch) {
			if (Input.GetButtonDown ("Fire1")) {
				PlayerPrefs.SetInt ("PrimeiraVez", 0);
				PlayerPrefs.SetInt ("msgIniciar", 1);
				PlayerPrefs.SetInt ("checkpoint", 0);
				PlayerPrefs.SetInt ("vida", 3);
				PlayerPrefs.SetInt ("fase", 1);
				SceneManager.LoadScene ("Cena_1");
			}
		}
		
	}


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			GameObject.Find ("GraphicsMulher").SendMessage ("pararMulher");
			//player.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, 10));
			player = coll.gameObject;
			GameObject.Find ("gameEngine").SendMessage ("finalizarJogo");
			Instantiate (coracao).transform.position = new Vector2(player.transform.position.x+5.5f, -5f);
			Invoke ("msgFinal", 7);
		}
	}


	void msgFinal() {
		final.SetActive (true);
		Invoke ("ativarTouch", 5);
	}

	void ativarTouch() {
		touch = true;
	}
		
}
