using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
    public Canvas quitmenu;

	// Use this for initialization
	void Start () {
        //quitmenu = quitmenu.GetComponent<Canvas> ();
        quitmenu.enabled = false;
        
	}

    public void ExitPress()
    {
        quitmenu.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }
}
