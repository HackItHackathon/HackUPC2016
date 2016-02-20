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

<<<<<<< HEAD
    private PositionController positionController;
=======
    //private static string codeElements = "";
>>>>>>> 4be80dfc61230563a746c28e2f896c6f93bfc32e
    private int gamenumber = 0;
    private int gameId = 0; // TODO: Joan passam-ho
    private bool isAttacker = true; // TODO: tambe aixo

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
            // send it to the DB
            // first convert the puntuation to an integer
            int puntInt = (int) (puntuation * 1000f);
            string url = "http://interact.siliconpeople.net/hackathon/setpoint?gameid=" + gameId + "&punt";
            url += isAttacker ? "a=" : "d=" ;
            url += puntInt.ToString();
            WWW web = new WWW(url);
            Debug.Log(url);
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
