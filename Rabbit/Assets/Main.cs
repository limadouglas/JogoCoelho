using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	void Awake() {

		//PlayerPrefs.DeleteAll ();		// limpar preferencias.

		if (PlayerPrefs.GetInt ("som") == 0) {		// 0 = não definido ainda, o padrão é iniciar com som.
			PlayerPrefs.SetInt ("som", 1);			// 1 = com som; 2 = sem som.
		}
			
		if (PlayerPrefs.GetInt ("vida") <= 0) {
			PlayerPrefs.SetInt ("vida", 3);
			PlayerPrefs.SetInt ("fase", 1);
		}

		//PlayerPrefs.SetInt ("fase", 4);
		PlayerPrefs.SetInt ("msgIniciar", 1);

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
