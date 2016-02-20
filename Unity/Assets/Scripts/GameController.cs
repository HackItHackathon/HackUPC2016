using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class GameController : MonoBehaviour {

    [HideInInspector]
    public static GameController instance;
    public Canvas canvas;
    public GameObject messageBox;
    public int ID;

    private PositionController positionController;
    private static string codeElements = "";
    private string solution;
    private int gamenumber = 0;

	// Use this for initialization
	void Awake () {
	    if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    void Start()
    {
        Debug.Log("Game Started");
        positionController = GetComponent<PositionController>();
    }

    /*public static void setCodeElement(string id)
    {
        codeElements += id;
    }*/

    /*public static string getCodeElements()
    {
        return codeElements;
    }*/
	
    public void DisplayMessageBox(string text, UnityAction a)
    {
        GameObject message = Instantiate(messageBox);
        Text t = message.GetComponentInChildren<Text>();
        Button but = message.GetComponentInChildren<Button>();
        // Set text and listener
        t.text = text;
        but.onClick.AddListener(a);
        // Insert into the canvas
        message.transform.SetParent(canvas.transform, false);
    }

    public void SetSolution(string sol)
    {
        solution = sol;
    }

    public string GetSolution()
    {
        return solution;
    }
}
