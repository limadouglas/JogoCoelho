using UnityEngine;
using System.Collections;

public static class ValoresStaticos {

	private static float posicaoUltimoObstaculo = 5;


	public static void setPosicaoUltimoObstaculo(float posicao) {
		posicaoUltimoObstaculo = posicao;
	}

	public static float getPosicaoUltimoObstaculo() {
		return posicaoUltimoObstaculo;
	}

}
