using UnityEngine;
using System.Collections;

public class UserPointFader : MonoBehaviour
{

    public float fadeOutTime = 4;
    public float pas = 0.05f;

    public float equR= 6.3844e6f;   // Equatorial radius
    public float polR = 6.3528e6f;  // Polar radius
    public float equR2;             // Equatorial radius square
    public float polR2;             // Polar radius square
    public float equR4;             // Equatorial radius pow 4
    public float polR4;             // Polar radius pow 4

    private MeshRenderer meshR;

    // Use this for initialization
    void Start()
    {
        equR2 = equR * equR;
        polR2 = polR * polR;
        equR4 = equR2 * equR2;
        polR4 = polR2 * polR2;

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

    public float earthRadius(float lat, float lon)
    {
        float num = (equR4 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR4 * Mathf.Sin(lat) * Mathf.Sin(lat));
        float den = (equR2 * Mathf.Cos(lat) * Mathf.Cos(lat)) +
                    (polR2 * Mathf.Sin(lat) * Mathf.Sin(lat));

        return Mathf.Sqrt(num / den);
    }
}
