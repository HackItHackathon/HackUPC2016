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
        string url = "http://interact.siliconpeople.net/hackathon/getuserid?nom=" + userName.text;
        WWW web = new WWW(url);
        /*IEnumerator Start(){
            WWW www = new WWW(url);
            yield return www;
          }*/

    }
	
}
