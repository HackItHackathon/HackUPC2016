using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PointController : MonoBehaviour {
    public Text txt;
    private int currentscore = 0;

	// Use this for initialization
	void Start () {
        string url = "http://interact.siliconpeople.net/hackathon/getuserinfo?id=" + GameController.instance.ID;
        WWW web = new WWW(url);
        while (!web.isDone) ;
        Getuserinfo user = new Getuserinfo();
        Debug.Log(web.text);

        JsonUtility.FromJsonOverwrite(web.text, user);
        currentscore = user.punt;
        txt.text = currentscore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if(txt!=null) txt.text = currentscore.ToString();

	}
    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
}
