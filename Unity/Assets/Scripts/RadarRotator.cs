using UnityEngine;
using System.Collections;

public class RadarRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Input.location.Start();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Input.compass.trueHeading);
        transform.Rotate(new Vector3(0,0,-Input.compass.trueHeading));
    }
}
