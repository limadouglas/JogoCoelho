using UnityEngine;
using System.Collections;

public class ScriptFundo : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

		// posicionando BoxCollider2d na base da tela.

		// convertendo um vetor de Screen para dimenções do mundo, 				
		Vector2 tela = Camera.main.ScreenToWorldPoint (new Vector2 (0, Camera.main.pixelHeight));		//Camera.main.pixelHeight retorna a Altura da camera.
		GetComponent<BoxCollider2D> ().offset = new Vector3 (0, -(tela.y-1));		// reposicionando boxCollider2D.
	}
}
