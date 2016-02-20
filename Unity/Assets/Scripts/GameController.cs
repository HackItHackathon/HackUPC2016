using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [HideInInspector]
    public static GameController instance;
    public PositionController positionController;

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
	
}
