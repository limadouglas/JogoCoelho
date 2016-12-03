using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Teste : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0);
	
	}
}
