using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

 

public class Login_new : MonoBehaviour {

    public Text userName;

	// Use this for initialization
	void Awake () {

    }

    public void onClick()
    {
        Debug.Log(userName.text);
        string url = "http://interact.siliconpeople.net/hackathon/getuserid?nom=" + userName.text;
        WWW web = new WWW(url);
        while (!web.isDone) ;
        Debug.Log(web.text);
        Getuserid myObject = new Getuserid();
        JsonUtility.FromJsonOverwrite(web.text, myObject);
        Debug.Log("User ID: " + myObject.id);
        GameController.instance.ID = myObject.id;
        SceneManager.LoadScene("menu");
    }

}
