using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionController : MonoBehaviour
{
    private struct PlayerPoint
    {
        public long ID;
        public GameObject pointObject;

        public PlayerPoint(long ID = -1, GameObject pointObject = null)
        {
            this.ID = ID;
            this.pointObject = pointObject;
        }
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

    private PlayerPoint player;

    // Use this for initialization
    void Start()
    {
        equR2 = equR * equR;
        polR2 = polR * polR;
        equR4 = equR2 * equR2;
        polR4 = polR2 * polR2;

        coordinates.text = "";
        StartCoroutine(locationServiceStart());
    }

    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {

        }
        switch (Input.location.status)
        {
            case LocationServiceStatus.Running:
                {
                    playerPosition.x = Input.location.lastData.latitude;
                    playerPosition.y = Input.location.lastData.longitude;

                    coordinates.text = "Lat: " + playerPosition.x + " Lon: " + playerPosition.y;
                    break;
                }
            case LocationServiceStatus.Failed:
                coordinates.text = "Failed to get location";
                break;
        }
    }

    IEnumerator locationRefresh()
    {
        string url = "http://interact.siliconpeople.net/hackathon/getuserinfo?id=" + GameController.instance.ID;
        WWW web = new WWW(url);
        while (!web.isDone) ;
        Getuserinfo user = new Getuserinfo();
        JsonUtility.FromJsonOverwrite(web.text, user);
        url = "http://interact.siliconpeople.net/hackathon/getnearid?id=" + GameController.instance.ID;
        web = new WWW(url);
        while (!web.isDone) ;
        Getnearid near = new Getnearid();
        JsonUtility.FromJsonOverwrite(web.text, near);
        Debug.Log(near.table[0].id);
        yield return new WaitForSeconds(5);
    }

    IEnumerator locationServiceStart()
    {
        Input.location.Start();
        coordinates.text = "Searching current location";

        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            GameController.instance.DisplayMessageBox("Please turn on location service",
                () =>
                {
#if UNITY_EDITOR
                    //UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                });
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

        yield return true;
    }

    public float earthRadius(float lat, float lon)
    {
        float num = (equR4 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR4 * Mathf.Sin(lat) * Mathf.Sin(lat));
        float den = (equR2 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR2 * Mathf.Sin(lat) * Mathf.Sin(lat));

        return Mathf.Sqrt(num / den);
    }
}
