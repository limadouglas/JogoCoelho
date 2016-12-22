using UnityEngine;
using System.Collections;

public class BotaoTras : MonoBehaviour {

	void Start () {
		transform.position = new Vector2 (Screen.width/13, Screen.height/6);


		GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/ 70, Screen.width/ 70);
	}

}
