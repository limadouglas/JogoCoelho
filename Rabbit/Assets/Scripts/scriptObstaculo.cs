﻿using UnityEngine;
using System.Collections;


public class ScriptObstaculo : MonoBehaviour {

	public float velocidade;			// velocidade do gameObjeto.
	private float valorRandomico;
	public Camera cam;
	public float posicaoInicial;



	void Start () {
		
		// definindo velocidade de movimentação do gameObject.
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-(velocidade) * Time.deltaTime, 0);
		criaObstaculo (posicaoInicial);
	}



	void FixedUpdate() {

		// verificando se o gameObject está passando do final da tela (ESQUERDA) e o reposiciondo.
		if (transform.position.x <= cam.transform.position.x - (ScriptUtil.tela.x * 1.2f)) 		// verificando.
			criaObstaculo (5);
	}



	void criaObstaculo(float distancia) {

		// gerando numero randomico;
		valorRandomico = (Random.value * 2);
		if (valorRandomico > -0.5f)
			valorRandomico = 2;
		else
			valorRandomico = 0;

		// posicionado gameObject baseado no tamanho da tela e numero randomico.
		transform.position = new Vector2 (cam.transform.position.x + ScriptUtil.tela.x + valorRandomico + distancia , -2);
	}


}



