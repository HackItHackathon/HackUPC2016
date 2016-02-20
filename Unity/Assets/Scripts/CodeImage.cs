using UnityEngine;
using System;

public class CodeImage : MonoBehaviour {

    public Sprite[] sprites;

    private CodeBackgroundInput codeBackgroundInput;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () { // TODO: ferho nomes quan volem que sigui random...
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite rand = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        spriteRenderer.sprite = rand;
        GameController.setCodeElement(rand.ToString());

        GameObject go = GameObject.FindGameObjectWithTag("CodeBackgroundInput");
        codeBackgroundInput = go.GetComponent<CodeBackgroundInput>();
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void AssignSprite(int id)
    {
        Sprite aux = sprites[id];
        spriteRenderer.sprite = aux;
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        string spr = spriteRenderer.sprite.name;
        codeBackgroundInput.AddElement(Int32.Parse(spr) - 1);
    }

}
