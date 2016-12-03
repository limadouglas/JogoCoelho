using UnityEngine;
using System.Collections;

public class scriptObstaculoAlto : MonoBehaviour {

	public float velocidade;
	private static float LARGURA_TELA = (float) (Screen.width/100.0);
	private static float ALTURA_TELA = (float) (Screen.height/100.0);

	void Start () {
		transform.position = new Vector2 (LARGURA_TELA+10, -(ALTURA_TELA));
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade), 0);
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.x <= -(LARGURA_TELA+10)) 
			transform.position = new Vector2 (LARGURA_TELA+10, transform.position.y);

	}
}
