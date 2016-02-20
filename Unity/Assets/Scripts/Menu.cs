using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {
    public Canvas quitmenu;
    public Button spy;
    public Button exit;
	// Use this for initialization
	void Start () {
        //quitmenu = quitmenu.GetComponent<Canvas> ();
        quitmenu.enabled = false;
        
	}

    public void ExitPress()
    {
        quitmenu.enabled = true;
        spy.enabled = false;
        exit.enabled = false;
    }

    public void NoPress()
    {
        quitmenu.enabled = false;
        spy.enabled = true;
        exit.enabled = true;
    }
	
    public void ExitGame()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(string level)
    {
        GameController.instance.scenesStack.Push(level);
        SceneManager.LoadScene(level);
    }
}
