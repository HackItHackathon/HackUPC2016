using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject codeImage;

	// Use this for initialization
	void Start () {
		Instantiate (codeImage);
	}
}
