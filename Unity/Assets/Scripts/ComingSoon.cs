using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ComingSoon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
    }
    public void LoadScene(string level)
    {
        //GameController.instance.scenesStack.Push(level);
        SceneManager.LoadScene(level);
    }

}
