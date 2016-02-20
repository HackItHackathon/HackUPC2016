using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public GameObject codeImage;
    public Text timeText;
    public float height = 3;

    public const float TIME = 4; // time that the player has to memorize
    public const float MIN_TIME = 0.001f;

    private GameObject[] codeImages;
    private string solution;


    // Use this for initialization
    void Start () {
        float xSpace = Boundary.maxX - Boundary.minX;
        float xDelta = xSpace / 5;

        codeImages = new GameObject[4];
        codeImages[0] = Instantiate(codeImage, new Vector3(Boundary.minX + xDelta, height, 0), Quaternion.identity) as GameObject;
        codeImages[1] = Instantiate(codeImage, new Vector3(Boundary.minX + 2*xDelta, height, 0), Quaternion.identity) as GameObject;
        codeImages[2] = Instantiate(codeImage, new Vector3(Boundary.minX + 3 * xDelta, height, 0), Quaternion.identity) as GameObject;
        codeImages[3] = Instantiate(codeImage, new Vector3(Boundary.minX + 4 * xDelta, height, 0), Quaternion.identity) as GameObject;
        solution = "";
        for(int i = 0; i < 4; ++i)
        {
            solution = solution + codeImages[i].GetComponent<CodeImage>().GetSprite() + " ";
        }
        Debug.Log(solution);
    }

    void Update()
    {
        float timeToEnd = TIME - Time.timeSinceLevelLoad;
        if(timeToEnd <= MIN_TIME)
        {
            // finish
            //Debug.Log(GameController.getCodeElements());
            SceneManager.LoadScene("Minigame_1_input");            
        }
        else
        {
            int timeInt = (int)timeToEnd;
            timeText.text = timeInt.ToString();
        }
        
    }
}
