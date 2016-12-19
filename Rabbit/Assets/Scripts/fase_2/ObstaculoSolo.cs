using UnityEngine;	
using System.Collections;

public class ObstaculoSolo : MonoBehaviour {

	private float velocidade;
	private Camera cam;
	private float valorRandomico;
	private bool comPlayer;
	private bool bateu;
	public Transform player;
	private int lado;


	void Start () {
		cam = GameObject.Find ("Main Camera").GetComponent<Camera>();

		velocidade = 40f;
		bateu = false;

		// definindo velocidade de movimentação do gameObject.
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);
		criaObstaculo ();
	}



	void FixedUpdate() {
		if (bateu)
			GetComponent<Rigidbody2D> ().AddForce( new Vector2 (100 * lado, 5 * lado));
		else if (comPlayer) {
			transform.position = new Vector2 (player.position.x, transform.position.y);
		} else {
			// verificando se o gameObject está passando do final da tela (ESQUERDA) e o reposiciondo.
			if (transform.position.x <= cam.transform.position.x - ((Screen.width / 100) * 1.2f) && cam.transform.position.x < 170) 		// verificando se utrapassou a tela e não chegou ao fim da fase..
			criaObstaculo ();
		}	
	}



	void criaObstaculo() {

		// gerando numero randomico;
		valorRandomico = Random.value;
		if (valorRandomico < 0.33f)
			valorRandomico = 0;
		else if (valorRandomico < 0.66f)
			valorRandomico = 3;
		else if (valorRandomico <= 1f)
			valorRandomico = 6;

		print (valorRandomico);

		// posicionado gameObject baseado no tamanho da tela e numero randomico.
		transform.position = new Vector2 (cam.transform.position.x + (Screen.width/100)/2 + (ValoresStaticos.getPosicaoUltimoObstaculo() + 2) + valorRandomico, -2);
		ValoresStaticos.setPosicaoUltimoObstaculo (transform.position.x);
	}


	public void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "SoloBateu") {
			if (coll.gameObject.transform.position.x > transform.position.x)
				lado = -1;
			else
				lado = 1;

			GetComponent<Rigidbody2D> ().isKinematic = false;
			bateu = true;
			print ("solo");

		}

		if (coll.gameObject.tag == "Player") {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			comPlayer = true;
		}
	}


	public void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);
			comPlayer = false;
		}
	}
		

	void OnTriggerEnter2D(Collider2D coll) {


	}
}
