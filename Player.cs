using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using GooglePlayGames;			//Only for Android
using GooglePlayGames.BasicApi; //Only for Android
//Does contain platform specific methods

public class Player : MonoBehaviour {

	public Vector2 RightSpeed = new Vector2(600, 0);
	public Vector2 LeftSpeed = new Vector2(-600, 0);

	public Vector3 startPosition = new Vector3( 0, -3, 0 );
	public Vector3 leftPosition = new Vector3(-3, -3, 0);
    public Vector3 rightPosition = new Vector3(3, -3, 0);

    public GameObject deathParticle;
    public GameObject teleportParticle;

    public int score = 0;

	public float timer = 0.0f;
    public double frameRate = .05f;

    Vector3 screenPosition;


    void Start () 
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 80;

		PlayerPrefs.SetInt ("currentScore", score);

		transform.position = startPosition;
    }


	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= frameRate) 
		{
		
        leftPosition = new Vector3(-3, -3, 0);
        rightPosition = new Vector3(3, -3, 0);

        
        //if player goes off rightside they come back on the left. and vice versa
		if (transform.position.x >= 3) 
		{
            Instantiate (teleportParticle, new Vector3 (3f, -3, 0), Quaternion.identity);
			transform.position = leftPosition;
		}
		else if (transform.position.x <= -3) 
		{
            Instantiate (teleportParticle, new Vector3 (-3f, -3, 0), Quaternion.identity);
			transform.position = rightPosition;
		}
        

		//if there is a touch on the screen, the block will move, if 0 or multiple touches, it will stay still
		if (Input.touchCount == 1) {
			var touch = Input.touches [0];

			if (touch.position.x > Screen.width / 2) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				GetComponent<Rigidbody2D> ().AddForce (RightSpeed);
			} else if (touch.position.x < Screen.width / 2) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				GetComponent<Rigidbody2D> ().AddForce (LeftSpeed);
			}
		} 
		else if (Input.touchCount == 0 || Input.touchCount >= 2) 
		{
			GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		} 

			timer = 0f;
		}

        if (PlayerPrefs.GetInt("GameOver") == 1)
        {
            Destroy(GetComponent<PolygonCollider2D>());
        }
	}


    void OnCollisionEnter2D(Collision2D coll)
    { 
        score++;

        foreach (ContactPoint2D enemyHit in coll.contacts)
        {
            Vector2 hitPoint = enemyHit.point;
            Instantiate(deathParticle, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
        }

        //logs the high score and current score
        PlayerPrefs.SetInt("currentScore", score);
        if (score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
        } 
    }

    //Android Specific Methods -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




    //iOS Specific Methods -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //None
}
