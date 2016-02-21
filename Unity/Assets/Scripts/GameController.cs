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
    public int gameId = 0; // TODO: Joan passam-ho
    public bool isAttacker = true; // TODO: tambe aixo
    public float puntuation = 0;

    private PositionController positionController;
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
            string url = "http://interact.siliconpeople.net/hackathon/setpoints?gameid=" + gameId + "&punt";
            url += isAttacker ? "a=" : "d=" ;
            url += puntInt.ToString();
            WWW web = new WWW(url);
            Debug.Log(url);
            SceneManager.LoadScene("Menu");
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
    public void LoadScene(string level)
    {
        //GameController.instance.scenesStack.Push(level);
        SceneManager.LoadScene(level);
    }
}
