using UnityEngine;
using System.Collections;

public class ScriptCamera : MonoBehaviour {

	public Transform playerPosicao;
	public Vector2 velocidade;
	public float suavizacao = 0.0f;
	private float posicaoInicialPlayer;
	public GameObject obstaculoAlto;
	public GameObject obstaculoAltoDois;
	private Vector2 tela;
	private float valorRandomico;


	void Start() {
		posicaoInicialPlayer = playerPosicao.position.x;
	}


	void FixedUpdate () {
		
		// movendo camera junto com personagem.
		if(playerPosicao.position.x < 170)
			transform.position = new Vector3 (Mathf.SmoothDamp (transform.position.x, playerPosicao.position.x+4, ref velocidade.x, suavizacao), transform.position.y, -10); 

	}
		
}
