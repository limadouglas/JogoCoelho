using UnityEngine;
using System.Collections;

public class ScriptFundo : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

		// convertendo um vetor de Screen para dimenções do mundo, 				
		Vector2 tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, 0));		//Camera.main.pixelWidth retorna a Largura da camera.
		transform.position = new Vector2(-(tela.x+3), transform.position.y);

	}
}
