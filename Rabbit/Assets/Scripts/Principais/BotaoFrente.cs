﻿using UnityEngine;
using System.Collections;

public class BotaoFrente : MonoBehaviour {

	void Start () {
		transform.position = new Vector2 (Screen.width/4, Screen.height/6);

		GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/ 60, Screen.width/ 60);
	}

}