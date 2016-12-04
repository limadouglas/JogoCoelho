using UnityEngine;
using System.Collections;

public class scriptObstaculo : MonoBehaviour {

	public float velocidade;			// velocidade do gameObjeto.
	private Vector2 tela;				// dimensão da tela.
	private Vector2 dimensaoObjeto;		// dimensão do gameObjeto.
	public float posicaoInicial;

	void Start () {

		// convertendo screen width e height para world.
		tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );

		// pegando dimensão do objeto.
		dimensaoObjeto = GetComponent<BoxCollider2D> ().size;

		// posicionado gameObject baseado no tamanho da tela e dimensão do objeto.
		transform.position = new Vector2 (tela.x+posicaoInicial, -(tela.y-dimensaoObjeto.y/2.2f));

		// definindo velocidade de movimentação do gameObject.
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);
	}

	void FixedUpdate () {

		// verificando se o gameObject está passando do final da tela (ESQUERDA) e o reposiciondo.
		if (transform.position.x <= -(tela.x+5))									// verificando.
			transform.position = new Vector2 (tela.x+posicaoInicial, transform.position.y);		// reposicionando.
		
	}
}
