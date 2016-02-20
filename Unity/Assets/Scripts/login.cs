using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

    public Text userName;

	// Use this for initialization
	void Awake () {

    }

    public void onClick()
    {
        Debug.Log(userName.text);

    }
	
}
