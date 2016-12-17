using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	void Awake() {

		PlayerPrefs.SetFloat("checkpoint", 0);

		if (PlayerPrefs.GetInt ("vida") <= 0) {
			PlayerPrefs.SetInt ("vida", 3);
			PlayerPrefs.SetInt ("fase", 1);
		}
		
		switch (PlayerPrefs.GetInt ("fase")) {

			case 1:	SceneManager.LoadScene ("Cena_1");
					break;

			case 2: SceneManager.LoadScene ("Cena_2");
					break;

			case 3: SceneManager.LoadScene ("Cena_3");
					break;

			case 4: SceneManager.LoadScene ("Cena_4");
					break;

			case 5: SceneManager.LoadScene ("Cena_5"); 
					break;

			default:SceneManager.LoadScene ("Cena_1");
					break;
		}

	}

}
