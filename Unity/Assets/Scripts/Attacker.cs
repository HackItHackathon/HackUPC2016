using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Attacker : MonoBehaviour {

    private PositionController posC;
    
    // Use this for initialization
	void Start () {
        posC = GetComponent<PositionController>();
	}

    public void back()
    {
        SceneManager.LoadScene("Menu");
    }

	public void AttackVictim()
    {
        string url = "http://interact.siliconpeople.net/hackathon/setgame?ida=" + GameController.instance.ID + "&idd=" + posC.victim.ID;
        WWW web = new WWW(url);
        while (!web.isDone) ;
        Game game = new Game();
        JsonUtility.FromJsonOverwrite(web.text, game);

        GameController.instance.gameId = game.gameId;
        GameController.instance.isAttacker = true;

        SceneManager.LoadScene("Minigame_1");
    }
}
