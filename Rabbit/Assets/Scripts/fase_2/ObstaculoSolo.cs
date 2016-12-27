using UnityEngine;	
using System.Collections;

public class ObstaculoSolo : MonoBehaviour {

	public Camera cam;
	private float velocidade;
	private bool comPlayer;
	public Transform player;
	private bool colidiu;


	void Start () {

		velocidade = 40f;
		comPlayer = false;
		colidiu = false;

		// definindo velocidade de movimentação do gameObject.
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);
	}



	void FixedUpdate() {
		if (!colidiu) {
			
			if (comPlayer)
				transform.position = new Vector2 (player.position.x, transform.position.y);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);
		

			if (transform.position.x <= cam.transform.position.x - ((Screen.width / 100) * 1.2f) && cam.transform.position.x < 170)
				transform.position = new Vector2 (transform.position.x + 205, transform.position.y);
		}
	}



	void OnCollisionEnter2D(Collision2D coll)  {

		if (coll.gameObject.tag == "Player") {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			comPlayer = true;
		} 
	
	}



	public void OnCollisionExit2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Player")
			comPlayer = false;
	}



	public void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.tag == "Solo") {
			GetComponent<Animator> ().SetBool ("colidiu", true);
			GetComponent<Rigidbody2D> ().isKinematic = false;

			coll.GetComponent<Rigidbody2D> ().isKinematic = false;
			coll.GetComponent<Animator>().SetBool("colidiu", true);
			coll.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			colidiu = true;

			Invoke ("destruir", 0.6f);
		}

	}


	void destruir() {
		Destroy (gameObject);
	}

}
