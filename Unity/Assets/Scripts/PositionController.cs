using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour
{
    private struct PlayerPoint
    {
        public long ID;
        public GameObject pointObject;

        public PlayerPoint(long ID = -1, GameObject pointObject  = null)
        {
            this.ID = ID;
            this.pointObject = pointObject;
        }
    };

    public Vector2[] positions = { new Vector2(2f, 3f), new Vector2(-1f, 4f) };
    public Vector2 playerPosition;
    public GameObject userPoint;

    private PlayerPoint player;

    public float equR = 6.3844e6f;   // Equatorial radius
    public float polR = 6.3528e6f;  // Polar radius
    public float equR2;             // Equatorial radius square
    public float polR2;             // Polar radius square
    public float equR4;             // Equatorial radius pow 4
    public float polR4;             // Polar radius pow 4

    // Use this for initialization
    void Start()
    {
        equR2 = equR * equR;
        polR2 = polR * polR;
        equR4 = equR2 * equR2;
        polR4 = polR2 * polR2;

        int ID = -1;
        Instantiate(userPoint, new Vector3(positions[0].x, positions[0].y, 0), Quaternion.Euler(new Vector3(90f, 0f, 0f)));
    }

    // Update is called once per frame
    void Update()
    {

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
