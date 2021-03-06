﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Defenses : MonoBehaviour {
    public Text txtpartidesleft;
    public Text txtplayer;
    //public Text txttime;
    private string player;
    //private int time;
    //private int cur_time;
    //private int delta_time;
    private string partides = "0";
    // Use this for initialization
    void Awake ()
    {
        //System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        //cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
    }


    void Start()
    {
        Debug.Log("Defenses. ID: " + GameController.instance.ID + "GameController: " + GameController.instance);
        string url = "http://interact.siliconpeople.net/hackathon/getshield?id=" + GameController.instance.ID;
        Debug.Log("Getting defenses: " + url);
        WWW web = new WWW(url);
        while (!web.isDone) ;
        Getshield info = new Getshield();
        if (web.text.Length > 2)
        {
            JsonUtility.FromJsonOverwrite(web.text, info);
            player = info.nom;
            partides = info.partides.ToString();
            Debug.Log("Time: " + info.time);
            GameController.instance.gameId = info.gameid;
            GameController.instance.isAttacker = false;
            txtplayer.text = "hiol";
            txtpartidesleft.text = "0";
            //Debug.Log(System.DateTime.Parse(info.time));
            //cur_time = (int)(System.DateTime.Parse(info.time) - epochStart).TotalSeconds;
            /*
            time = (Int32)info.time;
            delta_time = time - cur_time;*/
        }
        else
        {
            txtplayer.enabled = false;
            //txttime.enabled = false;
        }
        

    }

    // Update is called once per frame
    void Update () {
        if (txtplayer != null) txtplayer.text = player;
        if (txtpartidesleft != null) txtpartidesleft.text = partides;
        //if (txttime != null) txttime.text = time;
    }
    public void LoadScene(string level)
    {
        GameController.instance.isAttacker = false;
        SceneManager.LoadScene(level);
    }
}
