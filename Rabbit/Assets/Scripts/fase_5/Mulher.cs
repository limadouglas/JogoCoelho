using UnityEngine;
using System.Collections;

public class Mulher : MonoBehaviour {

	private bool podeChamar;
	private bool pensando;
	private Vector3 posicaoInicial;
	private int direcao;
	public GameObject particula;
	public float tempoPensando;
	public float distacia;
	public float velocidade;
	public bool parar;


	void Start () {
		posicaoInicial = transform.position;
		pensando = false;
		direcao = -1;
		podeChamar = true;
		parar = false;
	}
	
	// Update is called once per frame
	void Update () {	
		if (!parar) {
			if (transform.position.x < posicaoInicial.x - distacia) {
				direcao = 1;
				transform.localScale = new Vector3 (-1, 1, 1);

				if (!pensando && podeChamar) {
					podeChamar = false;
					Invoke ("chamar", distacia / velocidade);
				}

			} else if (transform.position.x > posicaoInicial.x + distacia) {
				direcao = -1;
				transform.localScale = new Vector3 (1, 1, 1);

				if (!pensando && podeChamar) {
					podeChamar = false;
					Invoke ("chamar", distacia / velocidade);
				}
			}


			if (!pensando) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocidade * direcao, 0);
				GetComponent<Animator> ().SetBool ("andar", true);
			}
		}
		
	}

	void chamar() {
		StartCoroutine (pararPensar ());
	}

	IEnumerator pararPensar() {
		pensando = true;
		GetComponent<Animator> ().SetBool ("andar", false);
		GetComponent<Rigidbody2D>().velocity  = Vector2.zero;
		particula.transform.position = new Vector2 (transform.position.x, transform.position.y + 0.8f);
		StartCoroutine (destruirParticula(Instantiate(particula)));
		yield return new WaitForSeconds (tempoPensando);
		podeChamar = true;
		pensando = false;
	}

	IEnumerator destruirParticula(GameObject part) {
		yield return new WaitForSeconds (tempoPensando);
		Destroy (part);
	}

	void pararMulher() {
		GetComponent<Rigidbody2D>().velocity  = Vector2.zero;
		transform.localScale = new Vector3 (1, 1, 1);
		GetComponent<Animator> ().SetBool ("andar", false);
		parar = true;
	}

}
