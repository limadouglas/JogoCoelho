using UnityEngine;
using System.Collections;

public class ScriptMataObstaculoChaoFixo : MonoBehaviour {

	public Camera cam;
	private Vector2 tela;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( (cam.transform.position.x - ScriptUtil.tela.x*1.2) > transform.position.x) { // verificando se o gameObject já saiu da camera.
			Destroy (gameObject);	// destruindo objeto
		}
	}

	void parar(){
		anim.SetBool ("pararAnimacao", true);
	}

	void retomar(){
		anim.SetBool ("pararAnimacao", false);
	}

}
