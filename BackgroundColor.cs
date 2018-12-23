using UnityEngine;
using System.Collections;
//Contains no platform specific methods

public class BackgroundColor : MonoBehaviour {

	public GameObject Player;
	public Player PlayerScript;

	public GameObject Background;

	public Color defaultColor = new Color (.584f, .647f, .180f, 1);
	public Color oneHundredColor = new Color (0f, .6f, .8f, 1);
	public Color twoHundredColor = new Color (1f, .6f, .2f, 1);
	public Color threeHundredColor = new Color (.4f, .4f, .8f, 1);
	public Color fiveHundredColor = new Color (.0f, .8f, .4f, 1);
	public Color oneThousandColor = new Color (.9f, .72f, .0f, 1);

	new SpriteRenderer renderer;

	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerScript = Player.GetComponent<Player> ();

		renderer = gameObject.GetComponent<SpriteRenderer> ();
		renderer.color = defaultColor;
	}

	void Update () {
		if (PlayerScript.score < 100) {
			renderer.color = defaultColor;
		}
		if (PlayerScript.score >= 100 && PlayerScript.score < 200)
		{
			renderer.color = oneHundredColor;
		}
		if (PlayerScript.score >= 200 && PlayerScript.score < 300)
		{
			renderer.color = twoHundredColor;
		}
		if (PlayerScript.score >= 300 && PlayerScript.score < 500)
		{
			renderer.color = threeHundredColor;
		}
		if (PlayerScript.score >= 500 && PlayerScript.score < 1000)
		{
			renderer.color = fiveHundredColor;
		}
		if (PlayerScript.score >= 1000)
		{
			renderer.color = oneThousandColor;
		}
	}
}
