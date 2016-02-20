﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionController : MonoBehaviour
{
    private struct PlayerPoint
    {
        public long ID;
        public Vector2 position;
        public Vector3 cartesianPosition;
        public GameObject pointObject;
    };

    public Vector2[] positions = { new Vector2(2f, 3f), new Vector2(-1f, 4f) };
    public Vector2 playerPosition;
    public GameObject userPoint;

    public Text coordinates;
    public int timeOut = 30;

    public static float equR = 6.3844e6f;  // Equatorial radius
    public static float polR = 6.3528e6f;  // Polar radius
    public static float equR2;             // Equatorial radius square
    public static float polR2;             // Polar radius square
    public static float equR4;             // Equatorial radius pow 4
    public static float polR4;             // Polar radius pow 4

    private PlayerPoint victim = new PlayerPoint();

    // Use this for initialization
    void Start()
    {
        equR2 = equR * equR;
        polR2 = polR * polR;
        equR4 = equR2 * equR2;
        polR4 = polR2 * polR2;

        victim.ID = -1;

        coordinates.text = "";
        StartCoroutine(locationServiceStart());
        StartCoroutine(locationRefresh());
    }

    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {

        }
        switch (Input.location.status)
        {
            case LocationServiceStatus.Running:
                coordinates.text = "Lat: " + playerPosition.x + " Lon: " + playerPosition.y;
                break;
            case LocationServiceStatus.Failed:
                coordinates.text = "Failed to get location";
                break;
        }
        // TEMP
        coordinates.text = "Llat: " + playerPosition.x + " Lon: " + playerPosition.y;
    }

    IEnumerator locationRefresh()
    {
        //playerPosition.x = Mathf.Deg2Rad * Input.location.lastData.latitude;
        //playerPosition.y = Mathf.Deg2Rad * Input.location.lastData.longitude;

        playerPosition = new Vector2(41.3892279f, 2.1133999f);

        // Get User info
        //string url = "http://interact.siliconpeople.net/hackathon/getuserinfo?id=" + GameController.instance.ID;
        //WWW web = new WWW(url);
        //while (!web.isDone) ;
        Getuserinfo user = new Getuserinfo();
        //JsonUtility.FromJsonOverwrite(web.text, user);
        user.ida = -1;

        // Check if we already have a victim selected
        if (user.ida >= 0)
        {
           // Get victim info
           //url = "http://interact.siliconpeople.net/hackathon/getuserinfo?id=" + user.ida;
           // web = new WWW(url);
           // while (!web.isDone) ;
           // Getuserinfo aux = new Getuserinfo();
           // JsonUtility.FromJsonOverwrite(web.text, aux);

            if (victim.ID < 0)
            {
                victim.ID = user.ida;
                //victim.position = new Vector2(aux.latitude, aux.longitude);
            }
            changePoint(victim);
        }
        else
        {
            // Get near victims info
            /*
            url = "http://interact.siliconpeople.net/hackathon/getnearid?id=" + GameController.instance.ID;
            web = new WWW(url);
            while (!web.isDone) ;
            Getnearid near = new Getnearid();
            JsonUtility.FromJsonOverwrite(web.text, near);
            Debug.Log(near.table[0].id); 

            // Get the nearest
            int index = near.table.Length > 0 ? 0 : -1;
            for (int i = 0; i < near.table.Length; ++i)
            {
                if (near.table[i].distance < near.table[index].distance) index = i;
            }

            DBUser aux = near.table[index]; 

            victim.ID = aux.id; 
            victim.position = new Vector2(aux.latitude, aux.longitude); */
            victim.position = new Vector2(41.389449f, 2.1131448f);
            drawPoint(victim);
        }

        yield return new WaitForSeconds(5);
    }

    IEnumerator locationServiceStart()
    {
        Input.location.Start();
        coordinates.text = "Searching current location";

        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            GameController.instance.DisplayMessageBox("Please turn on location service", () => { Application.Quit(); });
            yield break;
        }

        // Start service before quering location
        Input.location.Start();


        // Wait until service initializates
        int maxWait = timeOut;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            coordinates.text = "Searching current location, remaining time: " + maxWait;
            yield return new WaitForSeconds(1);
            --maxWait;
        }

        // Service didn't initalizate in 20 seconds
        if (maxWait < 1)
        {
            GameController.instance.DisplayMessageBox("Initialization timed out", () => { Application.Quit(); });
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GameController.instance.DisplayMessageBox("Unable to determine device location", () => { Application.Quit(); });
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            StartCoroutine(locationRefresh());
        }

        yield return true;
    }

    // lat and lon in radians
    public float earthRadius(float lat, float lon)
    {
        float num = (equR4 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR4 * Mathf.Sin(lat) * Mathf.Sin(lat));
        float den = (equR2 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR2 * Mathf.Sin(lat) * Mathf.Sin(lat));

        return Mathf.Sqrt(num / den);
    }

    public Vector3 getPolar(Vector2 vec)
    {
        return getPolar(vec.x, vec.y);
    }

    public Vector3 getPolar(float lat, float lon)
    {
        float r = earthRadius(lat, lon);
        return new Vector3(r * Mathf.Sin(lat) * Mathf.Cos(lon), r * Mathf.Sin(lat) * Mathf.Sin(lon), r * Mathf.Cos(lat));
    }

    public Vector3 getCartesian2D(Vector2 victimPos)
    {
        Vector3 origin = getPolar(playerPosition);
        Vector3 dest = getPolar(victimPos);
        Vector3 diff = dest - origin;
        diff.Normalize();

        return Vector3.ProjectOnPlane(diff, origin);
    }

    void drawPoint(PlayerPoint vict)
    {
        Vector3 pos = getCartesian2D(vict.position);
        pos.z = 0;

        Debug.Log(pos);
        victim.pointObject = Instantiate(userPoint, pos, Quaternion.identity) as GameObject;
    }

    void changePoint(PlayerPoint vict)
    {
        Vector3 pos = getCartesian2D(vict.position);
        pos.z = 0;

        Debug.Log(pos);
        victim.pointObject.transform.position = pos;
    }
}
