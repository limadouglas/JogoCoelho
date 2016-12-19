using UnityEngine;
using System.Collections;

public class MataObstaculoChaoFixo : MonoBehaviour {

	public Camera cam;
	private Vector2 tela;


	// Update is called once per frame
	void Update () {
		if ( (cam.transform.position.x - (Screen.width/100) * 1.2f) > transform.position.x) { // verificando se o gameObject já saiu da camera.
			Destroy (gameObject);	// destruindo objeto
		}
	}
		
}
