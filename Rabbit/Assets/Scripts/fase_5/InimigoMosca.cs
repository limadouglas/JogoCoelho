using UnityEngine;
using System.Collections;

public class InimigoMosca : MonoBehaviour {

	public float velocidade;
	private bool parar;

	void Start () {
		parar = false;	
	}
	
	void Update () {

		if(!parar) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-velocidade, 0);
			if(transform.position.x < -12) {
				transform.position = new Vector2 (200, transform.position.y);
			}
		}
	
	}

	public void pararObstaculo() {
		parar = true;
	}
}
