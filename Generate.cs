using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using GooglePlayGames;			//Only for Android
using GooglePlayGames.BasicApi; //Only for Android
//Does contain platform specific methods

public class Generate : MonoBehaviour {

	public GameObject Enemy; //Game Objects
	public GameObject Player;
	public Player PlayerScript;

	public bool gamePaused = false; //Pause Button
	public Texture2D pauseButton;
    public int pauseCounter = 0;
    public bool Exit = false;

    public GameObject InstructionOverlay; //Instruction Variables
    public GameObject InstructionArrows; 
    public GameObject OverlayObject;
    public GameObject ArrowObject;
    public bool InstructionsDelay = false;
    
    public Font MagnetoB; //Fonts and sizes
    public Font Verdana;
	public int scorePosition = 1;

	public float genRate = .73f; //Timers 
	public float timer = 0.0f;

    new SpriteRenderer renderer; //Allows the changing of colors of Objects

    void Start ()
	{

        if (PlayerPrefs.HasKey("GamesPlayed") == false)
        {
            PlayerPrefs.SetInt("GamesPlayed", 1);
        }

        if (PlayerPrefs.GetInt("GamesPlayed") <= 2 || PlayerPrefs.GetInt("InstructionsSetting") == 1)
        {
            OverlayObject = Instantiate(InstructionOverlay , new Vector3(0, .25f, .5f), Quaternion.identity) as GameObject;
            ArrowObject = Instantiate(InstructionArrows, new Vector3(-.3f, -2, .5f), Quaternion.identity) as GameObject;
            InstructionsDelay = true;
        }

        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
        

        Player = GameObject.FindGameObjectWithTag ("Player"); //Gets the score from the player script
		PlayerScript = Player.GetComponent<Player> ();
	}

	void Update () {

        //centers the score
        if (PlayerScript.score < 10) {
			scorePosition = 1;
		}
		if (PlayerScript.score >= 10 && PlayerScript.score < 100) {
			scorePosition = 2;
		}
		if (PlayerScript.score >= 100 && PlayerScript.score < 1000) {
			scorePosition = 3;
		}

		//Manages block respawn rate and achievement unlocking
		if (PlayerScript.score >= 50) {
			Rookie ();
		}
		if (PlayerScript.score < 100) {
            if ((PlayerPrefs.GetInt("GamesPlayed") <= 3 || PlayerPrefs.GetInt("InstructionsSetting") == 1 ) && InstructionsDelay == true) //If first play, delay blocks and then destroy Instruction Overlays
            {
                timer += Time.deltaTime;
                if (timer >= 2.55f)
                {
                    timer = 0f;
                    CreateBlocks();
                    InstructionsDelay = false;
                    GameObject.Destroy(OverlayObject);
                    GameObject.Destroy(ArrowObject);
                    if ((PlayerPrefs.GetInt("GamesPlayed") == 3))
                    {
                        PlayerPrefs.SetInt("InstructionsSetting", 0);
                    }
                }
            }
            else
            {
                genRate = .73f;
                timer += Time.deltaTime;
                if (timer >= genRate)
                {
                    timer = 0f;
                    CreateBlocks();
                }
            }
		}
		if (PlayerScript.score >= 100 && PlayerScript.score < 200) {
			genRate = .72f;
			timer += Time.deltaTime;
			if (timer >= genRate) 
			{
				timer = 0f;
				CreateBlocks();
			}
		}
		if (PlayerScript.score >= 200 && PlayerScript.score < 300) {
			UpAndComer ();
			genRate = .7f;
			timer += Time.deltaTime;
			if (timer >= genRate) 
			{
				timer = 0f;
				CreateBlocks();
			}
		}
		if (PlayerScript.score >= 300  && PlayerScript.score < 500) {
			Master ();
			genRate = .68f;
			timer += Time.deltaTime;
			if (timer >= genRate) 
			{
				timer = 0f;
				CreateBlocks();
			}
		}
		if (PlayerScript.score >= 500) {
			Veteran ();
			genRate = .67f;
			timer += Time.deltaTime;
			if (timer >= genRate) 
			{
				timer = 0f;
				CreateBlocks();
			}
		}
		if (PlayerScript.score >= 1000) {
			GodLike ();
			scorePosition = 4;
		}
	}



	void CreateBlocks () //Generates the blocks
	{	
		Vector3 startPosition = new Vector3 (Random.Range (-2.8F, 2.8F), 6, 0);
		Instantiate(Enemy, startPosition, Quaternion.identity);
	}



    void OnGUI()
    {
        //displays the score
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.font = MagnetoB;
        GUI.color = Color.black;
        GUIStyle ScoreFont = new GUIStyle("Label");
        ScoreFont.fontSize = Screen.width / 8;


        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 2, Screen.height / 2 - Screen.height / 2, Screen.width, Screen.height / 6), PlayerPrefs.GetInt("currentScore").ToString(), ScoreFont);

        //pause Button
        if (pauseCounter <= 5)
        {
            if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 3, Screen.height / 2 - Screen.height / 2.2f, Screen.width / 8, Screen.height / 14), pauseButton))
            {
                pauseCounter += 1;
                gamePaused = !gamePaused;
            }
        }


        if (gamePaused == true)
        {
            Time.timeScale = 0;
            
            GUIStyle ButtonExitFont = new GUIStyle("Button");
            ButtonExitFont.fontSize = Screen.width / 12;

            GUIStyle AreYouSureFont = new GUIStyle("Label");
            AreYouSureFont.fontSize = Screen.width / 13;

            GUIStyle PauseFont = new GUIStyle("Label");
            PauseFont.fontSize = Screen.width / 6;

            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 3.2f, Screen.height / 2 - Screen.height / 6, Screen.width / 1.5f, Screen.height / 4), "Paused", PauseFont);

            
            if (Exit == false)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 + Screen.height / 8, Screen.width / 3f, Screen.height / 8), "Quit", ButtonExitFont))
                {
                    Exit = true;
                }
            }
            else if (Exit == true)
            {
                GUI.skin.font = Verdana;
                GUI.Label(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2, Screen.width / 1.2f, Screen.height / 5), "Are you sure you want to quit? your score will be lost.", AreYouSureFont);

                GUI.skin.font = MagnetoB;
                //Quit
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 2.7f, Screen.height / 2 + Screen.height / 5, Screen.width / 3, Screen.height / 8), "Quit", ButtonExitFont))
                {
                    Application.LoadLevel("Menu");
                }
                //Cancel
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 20, Screen.height / 2 + Screen.height / 5, Screen.width / 3, Screen.height / 8), "Cancel", ButtonExitFont))
                {
                    Exit = false;
                }
            }
        }
        else if (gamePaused == false)
        {
            Time.timeScale = 1;
        }
	}



	//Android Specific Methods ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 

	void Rookie()
	{
		Social.ReportProgress ("CgkI6bqjrY4LEAIQAg", 100.0f, (bool success) => {
		});
	}
	void UpAndComer()
	{
		Social.ReportProgress ("CgkI6bqjrY4LEAIQAw", 100.0f, (bool success) => {
		});
	}
	void Master()
	{
		Social.ReportProgress ("CgkI6bqjrY4LEAIQBA", 100.0f, (bool success) => {
		});
	}
	void Veteran()
	{
		Social.ReportProgress ("CgkI6bqjrY4LEAIQBQ", 100.0f, (bool success) => {
		});
	}
	void GodLike()
	{
		Social.ReportProgress ("CgkI6bqjrY4LEAIQBg", 100.0f, (bool success) => {
		});
	}
    


    //iOS Specific Methods -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    /*
    void noFullScreen()
    {

    }
    void Rookie()
    {
        Social.ReportProgress("grp.CascadeRookie", 100.0f, (bool success) => {
        });
    }
    void UpAndComer()
    {
        Social.ReportProgress("grp.CascadeUpAndComer", 100.0f, (bool success) => {
        });
    }
    void Master()
    {
        Social.ReportProgress("grp.CascadeMaster", 100.0f, (bool success) => {
        });
    }
    void Veteran()
    {
        Social.ReportProgress("grp.CascadeVeteran", 100.0f, (bool success) => {
        });
    }
    void GodLike()
    {
        Social.ReportProgress("grp.CascadeGodLike", 100.0f, (bool success) => {
        });
    }
    */
}
