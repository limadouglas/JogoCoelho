using UnityEngine;
using System.Collections;

public class Vida1 : MonoBehaviour {

	void Start () {
		
		RectTransform rt = GetComponent<RectTransform> ();

		if (PlayerPrefs.GetInt ("vida") >= 1) {
			rt.sizeDelta = new Vector2 (Screen.width / 15, Screen.width/ 15);
			rt.position = new Vector2 (Screen.width - (rt.sizeDelta.x / 1.5f), Screen.height * 0.92f);
		} else
			Destroy (gameObject);

	}
}
