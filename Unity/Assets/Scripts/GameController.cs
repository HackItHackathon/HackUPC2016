using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;

    private static string codeElements = "";

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

    public static void setCodeElement(string id)
    {
        codeElements += id;
        //Debug.Log(codeElements);
    }

    public static string getCodeElements()
    {
        return codeElements;
    }
	
}
