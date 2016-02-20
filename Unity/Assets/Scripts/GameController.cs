using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [HideInInspector]
    public static GameController instance;
    public PositionController positionController;

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

        positionController = new PositionController();
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
