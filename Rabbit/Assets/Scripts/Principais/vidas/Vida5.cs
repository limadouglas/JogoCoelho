using UnityEngine;
using System.Collections;

public class Vida5 : MonoBehaviour {

	void Start () {

		RectTransform rt = GetComponent<RectTransform> ();

		if (PlayerPrefs.GetInt ("vida") >= 5) {
			rt.sizeDelta = new Vector2 (Screen.width / 15,Screen.width/ 15);
			rt.position = new Vector2 (Screen.width - (rt.sizeDelta.x * 4f), Screen.height * 0.92f);
		} else
			Destroy (gameObject);
	}
}
