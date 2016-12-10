using UnityEngine;
using System.Collections;

public class ScriptObstaculoDois : MonoBehaviour {

	public float velocidade;			// velocidade do gameObjeto.
	private Vector2 tela;				// dimensão da tela.
	public Camera cam;
	private float valorRandomico;


	void Start () {

		// convertendo screen width e height para world.
		tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );

		// definindo velocidade de movimentação do gameObject.
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);

		// gerando numero randomico;
		valorRandomico = (Random.value * 3);

		// posicionado gameObject baseado no tamanho da tela e numero randomico.
		transform.position = new Vector2 (cam.transform.position.x + tela.x*2 + valorRandomico, -2);

	}


	void FixedUpdate () {

		// verificando se o gameObject está passando do final da tela (ESQUERDA) e o reposiciondo.
		if (transform.position.x <= cam.transform.position.x - (tela.x + 2)) {		// verificando.
			valorRandomico = (Random.value * 3);
			transform.position = new Vector2 (cam.transform.position.x + tela.x*2 + valorRandomico, -2);
		}
	}

}
