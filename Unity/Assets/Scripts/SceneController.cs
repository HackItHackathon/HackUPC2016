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


    // Use this for initialization
    void Start () {
        float xSpace = Boundary.maxX - Boundary.minX;
        float xDelta = xSpace / 5;
        
		Instantiate(codeImage, new Vector3(Boundary.minX + xDelta, height, 0), Quaternion.identity);
        Instantiate(codeImage, new Vector3(Boundary.minX + 2*xDelta, height, 0), Quaternion.identity);
        Instantiate(codeImage, new Vector3(Boundary.minX + 3 * xDelta, height, 0), Quaternion.identity);
        Instantiate(codeImage, new Vector3(Boundary.minX + 4 * xDelta, height, 0), Quaternion.identity);
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
