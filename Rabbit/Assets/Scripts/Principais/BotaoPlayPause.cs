using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BotaoPlayPause : MonoBehaviour {

	private bool play;
	public Sprite spritePlay;				// sprite da imagem de play.
	public Sprite spritePause;				// sprite da imagem de pause.


	// Use this for initialization
	void Start () {
		RectTransform rt =  GetComponent<RectTransform> ();
		rt.position = new Vector2 (Screen.width*0.08f, Screen.height * 0.92f);
		rt.sizeDelta = new Vector2 (Screen.width/ 4, Screen.height/10);

		play = true;
	}


	// altera entre play e pause.
	void pausarJogar() {

		if (play) {																		// se play = true, então o jogo será pausado.
			Time.timeScale = 0;															// parando tempo.
			GetComponent<Button> ().image.overrideSprite = spritePlay;					// alterando icone.
			if (PlayerPrefs.GetInt ("som") == 1)
				GameObject.Find ("gameEngine").GetComponent<AudioSource>().mute = true;
		} else {																		// se play = false, então o jogo sera será retomado. 
			Time.timeScale = 1;															// retomando tempo.
			GetComponent<Button> ().image.overrideSprite = spritePause;					// alterando icone.
			if (PlayerPrefs.GetInt ("som") == 1)
				GameObject.Find ("gameEngine").GetComponent<AudioSource>().mute = false;
		}

		play = !play;																	// mudando estado da varivel.									 
	}


}
