using UnityEngine;
using System.Collections;

public class ScriptMataObstaculoChaoFixo : MonoBehaviour {

	public Camera cam;
	private Vector2 tela;
	
	// Update is called once per frame
	void Update () {
		if ( (cam.transform.position.x - ScriptUtil.tela.x*1.2) > transform.position.x) { // verificando se o gameObject já saiu da camera.
			Destroy (gameObject);	// destruindo objeto
		}
	}
}
