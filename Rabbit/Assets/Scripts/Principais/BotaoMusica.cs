using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BotaoMusica : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform rt =  GetComponent<RectTransform> ();
		rt.position = new Vector2 (Screen.width * 0.17f , Screen.height * 0.92f);
		rt.sizeDelta = new Vector2 (Screen.width/ 4, Screen.height/10);

	}





}
