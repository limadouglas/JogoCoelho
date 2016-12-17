using UnityEngine;
using System.Collections;

public class ScriptJoystick : MonoBehaviour {

	RectTransform rt;

	void Start () {
		
		// dimensionando e posicionando joystick.

		rt =  GetComponent<RectTransform> ();	// instanciando RectTransforme para poder manipular o joystick.

		// dimensionando, com largura da metade da tela e altura igual a da tela.
		rt.sizeDelta = new Vector2 (Screen.width/8, Screen.width/8) ;

		//posicionando, x = metade da metade da tela e y igual ao centro da tela.
		rt.position = new Vector2 (Screen.width/6, Screen.height/6);
	}

	void Update(){
		//if(rt.position.x != Screen.width/4)
			//posicionando, x = metade da metade da tela e y igual ao centro da tela.
			//rt.position = new Vector2 (Screen.width/4, Screen.height/2);
			//rt.position = new Vector2 (Screen.width/6, Screen.height/6);
	}

}
