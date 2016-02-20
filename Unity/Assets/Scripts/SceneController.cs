using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public GameObject codeImage;

	// Use this for initialization
	void Start () {
        float xSpace = Boundary.maxX - Boundary.minX;
        float xDelta = xSpace / 5;
		Instantiate(codeImage, new Vector3(Boundary.minX + xDelta, 0, 0), Quaternion.identity);
        Instantiate(codeImage, new Vector3(Boundary.minX + 2*xDelta, 0, 0), Quaternion.identity);
        Instantiate(codeImage, new Vector3(Boundary.minX + 3 * xDelta, 0, 0), Quaternion.identity);
        Instantiate(codeImage, new Vector3(Boundary.minX + 4 * xDelta, 0, 0), Quaternion.identity);       
    }
}
