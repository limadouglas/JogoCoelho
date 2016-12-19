using UnityEngine;
using System.Collections;

public class Vida3 : MonoBehaviour {
	
	void Start () {

		RectTransform rt = GetComponent<RectTransform> ();

		if (PlayerPrefs.GetInt ("vida") >= 3) {
			rt.sizeDelta = new Vector2 (Screen.width / 15,Screen.width/ 15);
			rt.position = new Vector2 (Screen.width - (rt.sizeDelta.x * 2.34f), Screen.height * 0.92f);
		} else
			Destroy (gameObject);
	}
}
