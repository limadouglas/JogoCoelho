using UnityEngine;
using System.Collections;

public class ScriptCamera : MonoBehaviour {

	public Transform playerPosicao;
	public Vector2 velocidade;
	public float suavizacao;


	void Start() {
		// posição inicial igual a posicao do player.
		transform.position = new Vector3 (playerPosicao.position.x, transform.position.y, -10);
	}

	void FixedUpdate () {		
		
		// movendo camera junto com personagem.
		if( (playerPosicao.position.x + (Screen.width/100)/3f ) < 181)
			transform.position = new Vector3 (Mathf.SmoothDamp (transform.position.x, playerPosicao.position.x+2, ref velocidade.x, suavizacao), transform.position.y, -10); 
		
	}
		
}
