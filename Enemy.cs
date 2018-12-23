using UnityEngine;
using System.Collections;
//Contains no platform specific methods

public class Enemy : MonoBehaviour {

	//Gets Player Script 
	public GameObject Player;
	public Player PlayerScript;

	public GameObject Scripts;
	public Generate generateScript;

    public GameObject endParticle;

    public float timer = 0.0f;
    public double delay = 3f;

    public bool dead = false;

    public Vector2 speed = new Vector2 (0, -5);

	void Start () 
	{
        PlayerPrefs.SetInt("GameOver", 0);

		//Initializes the Player Script with score variable
		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerScript = Player.GetComponent<Player> ();

		Scripts = GameObject.FindGameObjectWithTag ("Scripts");
		generateScript = Scripts.GetComponent<Generate> ();

		//Increases the speed with a higher score
		if (PlayerScript.score < 100)
		{
			speed = new Vector2 (0, -5);
			GetComponent<Rigidbody2D>().velocity = speed;
		}
		if (PlayerScript.score >= 100)
		{
			PlayerScript.RightSpeed = new Vector2(600, 0);
			PlayerScript.LeftSpeed = new Vector2(-600, 0);
			speed = new Vector2 (0, -5.1f);
			GetComponent<Rigidbody2D>().velocity = speed;
		}
		if (PlayerScript.score >= 200)
		{
			PlayerScript.RightSpeed = new Vector2(605, 0);
			PlayerScript.LeftSpeed = new Vector2(-605, 0);
			speed = new Vector2 (0, -5.2f);
			GetComponent<Rigidbody2D>().velocity = speed;
		}
		if (PlayerScript.score >= 300)
		{
			PlayerScript.RightSpeed = new Vector2(610, 0);
			PlayerScript.LeftSpeed = new Vector2(-610, 0);
			speed = new Vector2 (0, -5.25f);
			GetComponent<Rigidbody2D>().velocity = speed;
		}
		if (PlayerScript.score >= 500)
		{
			PlayerScript.RightSpeed = new Vector2(615, 0);
			PlayerScript.LeftSpeed = new Vector2(-615, 0);
			speed = new Vector2 (0, -5.3f);
			GetComponent<Rigidbody2D>().velocity = speed;
		}
	}


    void Update()
    {
        if (dead == true)
        {
            timer += Time.deltaTime;
            if (timer >= .38f)
            {
                timer = 0f;
                Application.LoadLevel("Menu");
            }
        }
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Barrier")
        {
            if (dead == false)
            {
                PlayerPrefs.SetInt("GameOver", 1);

                dead = true;

                Destroy(Player.GetComponent("Polygon Collider 2D"));

                transform.position = new Vector3(0, -6, 0);

                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("currentScore"));

                foreach (ContactPoint2D enemyHit in coll.contacts)
                {
                    Vector2 hitPoint = enemyHit.point;
                    Instantiate(endParticle, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
                }
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
