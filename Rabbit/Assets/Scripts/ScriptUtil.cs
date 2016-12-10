using UnityEngine;
using System.Collections;

public class ScriptUtil : MonoBehaviour {

	// convertendo screen width e height para world.
	public static  Vector2 tela = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight) );

}
