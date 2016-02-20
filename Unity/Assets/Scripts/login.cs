using UnityEngine;
using System.Collections;

public class login : MonoBehaviour {


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

    public static void onClick()
    {

    }
	
}
