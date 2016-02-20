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

    // Use this for initialization
    void Start()
    {
        int ID = -1;
        Instantiate(userPoint, new Vector3(positions[0].x, positions[0].y, 0), Quaternion.Euler(new Vector3(90f, 0f, 0f)));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
