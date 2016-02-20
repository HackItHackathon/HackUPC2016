using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [HideInInspector]
    public static GameController instance;
    [HideInInspector]
    public string solution;
    public Canvas canvas;
    public GameObject messageBox;
    public int ID;
    public Stack scenesStack = new Stack();

    public float puntuation = 0;

    //private static string codeElements = "";
    private int gamenumber = 0;

    // Use this for initialization
    void Awake()
    {
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
    }

    public void DisplayMessageBox(string text, UnityAction a)
    {
        GameObject message = Instantiate(messageBox);
        Text t = message.GetComponentInChildren<Text>();
        Button but = message.GetComponentInChildren<Button>();
        // Set text and listener
        t.text = text;
        but.onClick.AddListener(() => { Destroy(message); a(); });
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

    public void IncrementGame(GameObject obj)
    {
        ++gamenumber;
        Debug.Log(gamenumber);
        if (gamenumber >= 3)
        {
            // The end
            Debug.Log("This is the end. Puntuation: " + puntuation);
        }
        else
        {
            Destroy(obj);
            SceneManager.LoadScene("Minigame_1");
        }
    }

    public void LateUpdate()
    {
        
    }
}
