using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionController : MonoBehaviour
{
    public struct PlayerPoint
    {
        public long ID;
        public Vector2 position;
    };

    public Vector2 playerPosition;
    public GameObject userPoint;
    public GameObject radarLines;
    public GameObject radarParent;
    public Button hackButton;

    public Text coordinates;
    public int timeOut = 30;
    public float radarRadius;
    public float attackRadius;


    public static float equR = 6.3844e6f;  // Equatorial radius
    public static float polR = 6.3528e6f;  // Polar radius
    public static float equR2;             // Equatorial radius square
    public static float polR2;             // Polar radius square
    public static float equR4;             // Equatorial radius pow 4
    public static float polR4;             // Polar radius pow 4

    public PlayerPoint victim = new PlayerPoint();
    private float scaleFactor;

    // Use this for initialization
    void Start()
    {
        equR2 = equR * equR;
        polR2 = polR * polR;
        equR4 = equR2 * equR2;
        polR4 = polR2 * polR2;

        victim.ID = -1;

        coordinates.text = "";

        SpriteRenderer radarRenderer = radarLines.GetComponent<SpriteRenderer>();
        scaleFactor = radarRenderer.bounds.size.x / radarRadius;
        Debug.Log("Bounds: " + radarRenderer.bounds.size.x);

        StartCoroutine(locationServiceStart());
        StartCoroutine(locationRefresh());
    }

    void Update()
    {
#if UNITY_EDITOR
        coordinates.text = "Lat: " + Mathf.Rad2Deg * playerPosition.x + " Lon: " + Mathf.Rad2Deg * playerPosition.y;
#else
        switch (Input.location.status)
        {
            case LocationServiceStatus.Running:
                coordinates.text = "Lat: " + playerPosition.x + " Lon: " + playerPosition.y;
                break;
            case LocationServiceStatus.Failed:
                coordinates.text = "Failed to get location";
                break;
        }
#endif
    }

    IEnumerator locationRefresh()
    {

#if UNITY_EDITOR
        int ID = 22;
        playerPosition = new Vector2(41.389373f, 2.1116378f);
        playerPosition *= Mathf.Deg2Rad;
#else
        int ID = GameController.instance.ID;
        playerPosition.x = Mathf.Deg2Rad * Input.location.lastData.latitude;
        playerPosition.y = Mathf.Deg2Rad * Input.location.lastData.longitude;
        Debug.Log(playerPosition);
#endif

        // Get User info
        string url = "http://interact.siliconpeople.net/hackathon/getuserinfo?id=" + ID;
        WWW web = new WWW(url);
        while (!web.isDone) ;
        Getuserinfo user = new Getuserinfo();
        JsonUtility.FromJsonOverwrite(web.text, user);
        user.ida = -1;

        // Check if we already have a victim selected
        if (user.ida >= 0)
        {
            // Get victim info
            url = "http://interact.siliconpeople.net/hackathon/getuserinfo?id=" + user.ida;
            web = new WWW(url);
            while (!web.isDone) ;
            Getuserinfo aux = new Getuserinfo();
            JsonUtility.FromJsonOverwrite(web.text, aux);

            if (victim.ID < 0)
            {
                victim.ID = user.ida;
                victim.position = new Vector2(aux.latitude, aux.longitude);
            }
            changePoint(victim);
        }
        else
        {
            // Get near victims info

            url = "http://interact.siliconpeople.net/hackathon/getidnear?id=" + ID;
            web = new WWW(url);
            while (!web.isDone) ;
            Getnearid near = new Getnearid();
            near.table = new DBUser[] { };
            Debug.Log(web.text);
            JsonUtility.FromJsonOverwrite(web.text, near);

            GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in objects) Destroy(obj);

            // Get the nearest
            int index = near.table.Length > 0 ? 0 : -1;
            for (int i = 0; i < near.table.Length; ++i)
            {
                // Find the nearest
                if (near.table[i].distance < near.table[index].distance) index = i;

                drawPoint(new Vector2(near.table[i].latitude, near.table[i].longitude));
            }

            if (index >= 0)
            {
                hackButton.interactable = (near.table[index].distance < attackRadius);
                victim.ID = near.table[index].id;
                victim.position = new Vector2(near.table[index].latitude, near.table[index].longitude);
            }
            
        }

        url = "http://interact.siliconpeople.net/hackathon/setlocation?id=" + ID + "&latitude=" + playerPosition.x + "&longitude=" + playerPosition.y;
        web = new WWW(url);

        yield return new WaitForSeconds(2);
    }

    IEnumerator locationServiceStart()
    {
        Input.location.Start(0.5f, 0.5f);
        coordinates.text = "Searching current location";
        Debug.Log("Location Service start");

#if UNITY_EDITOR
        yield break;
#endif

        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            GameController.instance.DisplayMessageBox("Please turn on location service",
                                                        () => { Application.Quit(); });
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
            GameController.instance.DisplayMessageBox("Initialization timed out",
                                                        () => { Application.Quit(); });
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GameController.instance.DisplayMessageBox("Unable to determine device location",
                                                      () => { Application.Quit(); });
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
        // Numerator
        float num = (equR4 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR4 * Mathf.Sin(lat) * Mathf.Sin(lat));
        // Denominator
        float den = (equR2 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR2 * Mathf.Sin(lat) * Mathf.Sin(lat));

        return Mathf.Sqrt(num / den);
    }

    public Vector3 getCartesian(Vector2 vec)
    {
        return getCartesian(vec.x, vec.y);
    }

    public Vector3 getCartesian(float lat, float lon)
    {
        float r = earthRadius(lat, lon);
        return new Vector3(r * Mathf.Cos(lat) * Mathf.Cos(lon),
                            r * Mathf.Cos(lat) * Mathf.Sin(lon),
                            r * Mathf.Sin(lat));
    }

    public Vector3 getCartesian2D(Vector2 victimPos)
    {
        Vector3 origin = getCartesian(playerPosition);
        Vector3 dest = getCartesian(victimPos);
        Vector3 diff = dest - origin;
        Vector3 nord = getCartesian(Mathf.PI / 2, 0);
        Vector3 N0 = nord - origin;
        N0.Normalize();
        origin.Normalize();

        Vector3 E = Vector3.Cross(N0, origin);
        E.Normalize();
        Vector3 N = Vector3.Cross(origin, E);
        N.Normalize();

        return new Vector3(Vector3.Dot(diff, E), Vector3.Dot(diff, N), 0);
    }

    void drawPoint(Vector2 vict)
    {
        Vector3 pos = getCartesian2D(vict);
        pos *= scaleFactor;

        GameObject aux = Instantiate(userPoint, pos, Quaternion.Euler(90f, 0, 0)) as GameObject;
        aux.transform.SetParent(radarParent.transform);
    }

    void changePoint(PlayerPoint vict)
    {
        Vector3 pos = getCartesian2D(vict.position);
        //vict.cartesianPosition = pos;
        pos *= scaleFactor;

        //victim.pointObject.transform.position = pos;
    }
}
