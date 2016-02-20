using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {

    public GameObject codeImage;

    private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        Bounds bounds = meshRenderer.bounds;
        Vector3 limits = bounds.size;

        float xDelta = limits.x / 4;
        float yDelta = limits.y / 4;

        float yOffset = transform.position.y;

        float height1 = 1.5f * yDelta + yOffset;
        float height2 = 0.5f * yDelta + yOffset;
        float height3 = -0.5f * yDelta + yOffset;
        float height4 = -1.5f * yDelta + yOffset;
        float x1 = -1.5f * xDelta;
        float x2 = -0.5f * xDelta;
        float x3 = 0.5f * xDelta;
        float x4 = 1.5f * xDelta;

        GameObject [] codeImages = new GameObject[16];

        codeImages[0] = Instantiate(codeImage, new Vector3(x1, height1, 0), Quaternion.identity) as GameObject;
        codeImages[1] = Instantiate(codeImage, new Vector3(x2, height1, 0), Quaternion.identity) as GameObject;
        codeImages[2] = Instantiate(codeImage, new Vector3(x3, height1, 0), Quaternion.identity) as GameObject;
        codeImages[3] = Instantiate(codeImage, new Vector3(x4, height1, 0), Quaternion.identity) as GameObject;
        codeImages[4] = Instantiate(codeImage, new Vector3(x1, height2, 0), Quaternion.identity) as GameObject;
        codeImages[5] = Instantiate(codeImage, new Vector3(x2, height2, 0), Quaternion.identity) as GameObject;
        codeImages[6] = Instantiate(codeImage, new Vector3(x3, height2, 0), Quaternion.identity) as GameObject;
        codeImages[7] = Instantiate(codeImage, new Vector3(x4, height2, 0), Quaternion.identity) as GameObject;
        codeImages[8] = Instantiate(codeImage, new Vector3(x1, height3, 0), Quaternion.identity) as GameObject;
        codeImages[9] = Instantiate(codeImage, new Vector3(x2, height3, 0), Quaternion.identity) as GameObject;
        codeImages[10] = Instantiate(codeImage, new Vector3(x3, height3, 0), Quaternion.identity) as GameObject;
        codeImages[11] = Instantiate(codeImage, new Vector3(x4, height3, 0), Quaternion.identity) as GameObject;
        codeImages[12] = Instantiate(codeImage, new Vector3(x1, height4, 0), Quaternion.identity) as GameObject;
        codeImages[13] = Instantiate(codeImage, new Vector3(x2, height4, 0), Quaternion.identity) as GameObject;
        codeImages[14] = Instantiate(codeImage, new Vector3(x3, height4, 0), Quaternion.identity) as GameObject;
        codeImages[15] = Instantiate(codeImage, new Vector3(x4, height4, 0), Quaternion.identity) as GameObject;
        
        for (int i = 0; i < 16; ++i) codeImages[i].GetComponent<CodeImage>().AssignSprite(i);

    }
	
	// Update is called once per frame
	void Update () {	
	}
}
