using UnityEngine;
using System.Collections;
//Contains no platform specific methods

public class Fader : MonoBehaviour {

    public GameObject Background;
    public Color defaultColor = new Color(1f, 1f, 1f, 1);
    new SpriteRenderer renderer;

    public Color Fade = new Color(.8f, .8f, .8f, 1);
    public float FadeTime = .8f;


    void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = defaultColor;
    }


	void Update () {
        Color lerpedColor = Color.Lerp(Color.white, Fade, Mathf.PingPong(Time.time, FadeTime));
        renderer.color = lerpedColor;
    }
}
