using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Defenses : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
}
