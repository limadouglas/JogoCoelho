﻿using UnityEngine;
using System.Collections;

public class BotaoMusica : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform rt =  GetComponent<RectTransform> ();
		rt.position = new Vector2 (130, Screen.height-50);
		rt.sizeDelta = new Vector2 (Screen.width/ 4, Screen.height/10);
	}

}
