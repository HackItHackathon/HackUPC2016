using UnityEngine;
using System.Collections;

public class CodeImage : MonoBehaviour {

    public Sprite[] sprites;

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        Sprite rand = sprites[Random.Range(0, sprites.Length)];
        sprite.sprite = rand;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
