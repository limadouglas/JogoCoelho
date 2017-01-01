using UnityEngine;
using System.Collections;

public class RoletaCamera : MonoBehaviour {

	void Start () {
		GetComponent<Canvas> ().worldCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}

}
