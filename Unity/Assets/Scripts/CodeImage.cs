using UnityEngine;
using System.Collections;

public class CodeImage : MonoBehaviour {

    public Sprite[] sprites;

    private SpriteRenderer sprite;

	// Use this for initialization
	void Awake () { // TODO: ferho nomes quan volem que sigui random...
        sprite = GetComponent<SpriteRenderer>();
        Sprite rand = sprites[Random.Range(0, sprites.Length)];
        sprite.sprite = rand;
        //Debug.Log(rand.ToString());
        GameController.setCodeElement(rand.ToString());
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void AssignSprite(int id)
    {
        Sprite aux = sprites[id];
        sprite.sprite = aux;
    }

}
