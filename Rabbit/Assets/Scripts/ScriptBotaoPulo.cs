using UnityEngine;
using System.Collections;

public class ScriptBotaoPulo : MonoBehaviour {

	void Start () {
	
		// dimensionando e posicionando botao.

		RectTransform rt =  GetComponent<RectTransform> ();	// instanciando RectTransforme para poder manipular o botao.

		// definindo dimensão, o tamanho de x é aplicado em left e ridth(olhar no unity), ou sejá (Screen.width/4) = ((Screen.width/4)*2), nao tenho certeza.
		rt.sizeDelta = new Vector2 (Screen.width / 4, Screen.height);

		// posicionando botao. não consegui enterder isto direito, fiz por tentativa e erro.
		rt.position = new Vector2 (Screen.width - rt.sizeDelta.x/1.1f, Screen.height/2);

	}

}
