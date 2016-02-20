using UnityEngine;
using System.Collections;

public class UserPointFader : MonoBehaviour
{

    public float fadeOutTime = 4;
    public float pas = 0.05f;



    private MeshRenderer meshR;

    // Use this for initialization
    void Start()
    {
        
        meshR = GetComponent<MeshRenderer>();
        Color color = meshR.material.color;
        color.a = 0f;
        meshR.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(fadeOut());
        //Debug.Log("prova");
    }

    private IEnumerator fadeOut()
    {
        var t = 0f;
        Debug.Log("Coroutine");
        Color color = meshR.material.color;
        color.a = 1f;
        meshR.material.color = color;

        while (t < fadeOutTime + .5)
        {
            color.a = Mathf.Lerp(1f, 0f, t / fadeOutTime);
            Debug.Log(color.a);
            meshR.material.color = color;
            t += pas;

            yield return new WaitForSeconds(pas);
        }
        yield return true;
    }
}
