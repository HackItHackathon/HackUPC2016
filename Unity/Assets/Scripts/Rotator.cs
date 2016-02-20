using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        rb.angularVelocity = speed;
	}
}
