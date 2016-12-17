using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Vida1 : MonoBehaviour {

		void Start () {
		
		RectTransform rt = GetComponent<RectTransform> ();

		if (PlayerPrefs.GetInt ("vida") >= 1) {
			rt.localScale = new Vector2 (ScriptUtil.tela.x / 9, ScriptUtil.tela.x / 9);
			rt.position = new Vector2 (Screen.width - (rt.sizeDelta.x / 1.5f), Screen.height - (rt.sizeDelta.y / 1.5f));
		} else
			Destroy (gameObject);

	}
}
