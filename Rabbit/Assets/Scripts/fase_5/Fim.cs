using UnityEngine;
using System.Collections;

public class Fim : MonoBehaviour {

	public GameObject coracao;
	private GameObject player;
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			GameObject.Find ("GraphicsMulher").SendMessage ("pararMulher");
			//player.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, 10));
			player = coll.gameObject;
			GameObject.Find ("gameEngine").SendMessage ("finalizarJogo");
			Instantiate (coracao).transform.position = new Vector2(player.transform.position.x+5.5f, -0.5f);
		}
	}
		
}
