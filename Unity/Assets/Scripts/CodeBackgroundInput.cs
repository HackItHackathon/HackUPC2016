using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CodeBackgroundInput : MonoBehaviour { // this class is like a scene controller
    
    public GameObject codeImage;
    public const float TIME = 5; // time that the player has to answer

    private GameObject[] codeImages;
    private int count = 0;
    private MeshRenderer meshRenderer;
    private float height;
    private float [] x_values;

    // Use this for initialization
    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        Bounds bounds = meshRenderer.bounds;
        Vector3 limits = bounds.size;

        float xDelta = limits.x / 4;
        height = transform.position.y;
        x_values = new float[4];
        x_values[0] = -1.5f * xDelta;
        x_values[1] = -0.5f * xDelta;
        x_values[2] = 0.5f * xDelta;
        x_values[3] = 1.5f * xDelta;

        codeImages = new GameObject[4];
    }
	
	// Update is called once per frame
	void Update () {
        float timeToEnd = TIME - Time.timeSinceLevelLoad;
        //Debug.Log(timeToEnd);
        if (timeToEnd <= 0)
        {
            // finish
            GameController.instance.IncrementGame();
        }
    }

    public void AddElement(int id)
    {
        if (count < 4) {
            codeImages[count] = Instantiate(codeImage, new Vector3(x_values[count], height, 0), Quaternion.identity) as GameObject;
            codeImages[count].GetComponent<CodeImage>().AssignSprite(id);
            ++count;
        }
    }

    public void RemoveElement()
    {
        if(count > 0)
        {
            Destroy(codeImages[count-1]);
            --count;
        }
    }

    public bool CodeIsCorrect()
    {
        string solution = GameController.instance.GetSolution();
        string input = "";
        for(int i = 0; i < 4; ++i)
            input = input + codeImages[i].GetComponent<CodeImage>().GetSprite() + " ";
        Debug.Log("Solution: " + solution + " | Input: " + input);
        return solution.Equals(input);
    }

    public void Accept()
    {
        if(count == 4 && CodeIsCorrect())
        {
            Debug.Log("CONGRATULATIONS"); // TODO: message box
            GameController.instance.IncrementGame();
        }
        else
        {
            Debug.Log("NOPE, you lose");
        }
    }
}
