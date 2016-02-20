using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CodeImage : MonoBehaviour {

    public Sprite[] sprites;

    private CodeBackgroundInput codeBackgroundInput;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () { // TODO: ferho nomes quan volem que sigui random...
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite rand = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        spriteRenderer.sprite = rand;
        //GameController.setCodeElement(rand.ToString());

        if (SceneManager.GetActiveScene().name.Equals("Minigame_1_input")) {
            GameObject go = GameObject.FindGameObjectWithTag("CodeBackgroundInput");
            codeBackgroundInput = go.GetComponent<CodeBackgroundInput>();
        }
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void AssignSprite(int id)
    {
        Sprite aux = sprites[id];
        spriteRenderer.sprite = aux;
    }

    public string GetSprite()
    {
        return spriteRenderer.sprite.name;
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        if (SceneManager.GetActiveScene().name.Equals("Minigame_1_input"))
        {
            string spr = spriteRenderer.sprite.name;
            codeBackgroundInput.AddElement(Int32.Parse(spr) - 1);
        }
    }

}
