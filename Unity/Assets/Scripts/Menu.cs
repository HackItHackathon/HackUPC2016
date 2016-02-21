using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {
    public Canvas quitmenu;
    public Button spy;
    public Button exit;
    public Button stats;
    public Button defend;
    public Button influence;
    public Button shop;
    public Button ranking;
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
        stats.enabled = false;
        defend.enabled = false;
        influence.enabled = false;
        shop.enabled = false;
        ranking.enabled = false;

    }

    public void NoPress()
    {
        quitmenu.enabled = false;
        spy.enabled = true;
        exit.enabled = true;
        stats.enabled = true;
        defend.enabled = true;
        influence.enabled = true;
        shop.enabled = true;
        ranking.enabled = true;
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
        //GameController.instance.scenesStack.Push(level);
        Debug.Log("Before entering: " + GameController.instance.ID);
        SceneManager.LoadScene(level);
    }
}
