using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using GooglePlayGames;			//Only for Android
using GooglePlayGames.BasicApi; //Only for Android
//Does contain platform specific methods

//Bundle identifier for Android: com.Cameron_Detig.Cascade  Version: 2  Build:15
//Bundle identifier for iOS: com.camerondetig.cascadegame   Version: 1.2.1    Build:6

//Delete all unnecessary files and folders when porting to iOS;

public class MainMenu : MonoBehaviour {
    
	int highScorePosition = 1;

	public Generate gen;
    public Texture2D SettingsImage;
	bool labelActive = false;
	public int medFontSize;
	public int fontSize;
	public int bigFontSize;
	public Font MagnetoB;
	public Font Verdana;

	void Start ()
	{
        SignIn ();

		medFontSize = Screen.width / 11;
		fontSize = Screen.width / 9;
		bigFontSize = Screen.width / 8;
		gen = new Generate ();
	}

	void OnGUI ()
	{
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.color = Color.black;
		GUI.skin.font = MagnetoB;

		GUIStyle playFont = new GUIStyle ("Button");
		playFont.fontSize = Screen.width / 7;

		GUIStyle howToFont = new GUIStyle ("Button");
		howToFont.fontSize = Screen.width / 14;

		GUIStyle trianglesFont = new GUIStyle ("Button");
        trianglesFont.fontSize = Screen.width / 17;

		GUIStyle instructionsFont = new GUIStyle ("Label");
		instructionsFont.fontSize = Screen.width / 24;

        GUIStyle highScoreFont = new GUIStyle("Label");
        highScoreFont.fontSize = Screen.width / 11;

        GUIStyle currentScoreFont = new GUIStyle("Label");
        currentScoreFont.fontSize = Screen.width / 7;

        //makes the play button and loads the game if pressed 
        if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 4, Screen.height / 2, Screen.width / 2, Screen.height / 10), "Play" , playFont))
		{
			//Checks which scene to run
			switch (PlayerPrefs.GetString ("ActivePlayer")) {
			case "DefaultPlayer":
				Application.LoadLevel ("Cascade Game");
				break;
			case "Cascade":
				Application.LoadLevel ("Logo Scene");
				break;
			case "Rookie":
				Application.LoadLevel ("Rookie Scene");
				break;
			case "UpAndComer":
				Application.LoadLevel ("UpAndComer Scene");
				break;
			case "Master":
				Application.LoadLevel ("Master Scene");
				break;
			case "Veteran":
				Application.LoadLevel ("Veteran Scene");
				break;
			case "GodLike":
				Application.LoadLevel ("GodLike Scene");
				break;
			case "Donut":
				Application.LoadLevel ("Donut Scene");
				break;
			case "CautionSign":
				Application.LoadLevel ("RoadSign Scene");
				break;
			case "Illuminati":
				Application.LoadLevel ("Illuminati Scene");
				break;
			case "Illusion":
				Application.LoadLevel ("Illusion Scene");
				break;
            case "Pizza":
                Application.LoadLevel("Pizza Scene");
                break;
            default:
				Application.LoadLevel ("Cascade Game");
				break;
			}

			SignIn ();
		}

		//Instructions Button
		if (GUI.Button (new Rect(Screen.width / 2 - Screen.width / 2.2f, Screen.height / 2 + Screen.height / 2.35f, Screen.width / 10, Screen.height / 16), SettingsImage , howToFont))
		{
            Application.LoadLevel ("Settings");
		}

        /*
		GUI.skin.font = Verdana;
		if (labelActive) { //Displays instructions when the button is pressed
			GUI.Label (new Rect ((Screen.width / 2 - Screen.width / 2) + Screen.width / 12 , Screen.height / 2 + Screen.height / 3, Screen.width - Screen.width / 6, Screen.height / 8), "Tap the sides of the screen to move the triangle. Go through the walls to get to the other side. Don't let the blocks hit the ground!" , instructionsFont);
		}
        */

        //Shop Button
        GUI.skin.font = MagnetoB;
		if (GUI.Button (new Rect (Screen.width/2 + Screen.width / 30,Screen.height / 2 + Screen.height / 9, Screen.width / 3, Screen.height / 10), "Triangles" , trianglesFont))
		{
			Application.LoadLevel ("Triangles");
		}

        //Power Ups Button
        if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 3) - Screen.width / 30, Screen.height / 2 + Screen.height / 9, Screen.width / 3, Screen.height / 10), "Power Ups", trianglesFont))
        {
            Application.LoadLevel("Power Ups");
        }


        //Displays the current score
        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 2, Screen.height / 2 - Screen.height / 3, Screen.width, Screen.height / 4), PlayerPrefs.GetInt("currentScore").ToString(), currentScoreFont);
			
		//Displays the high score
		GUI.Label (new Rect (Screen.width / 2 - Screen.width / 2, Screen.height / 2 - Screen.height / 3.8f, Screen.width, Screen.height / 4), "Highscore: " + PlayerPrefs.GetInt ("highScore").ToString (), highScoreFont);

        //Displays the Amount of Coins
		GUI.Label (new Rect (Screen.width / 2 - Screen.width / 2, Screen.height / 2 - Screen.height / 5.5f, Screen.width, Screen.height / 4), "Coins: " + PlayerPrefs.GetInt ("Coins").ToString (), highScoreFont);

        AchievementsAndLeaderBoard();//Calls the Android / iOS methods to create the leaderboard and Achievements buttons
    }



//Android Specific Methods -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
	void OnApplicationQuit() {
		PlayGamesPlatform.Instance.SignOut ();
	}

	void SignIn() {
        if (PlayerPrefs.HasKey("AndroidButtons") == false)
        {
            PlayerPrefs.SetInt("AndroidButtons", 0);
        }

        /*
        if (PlayerPrefs.GetInt("AndroidButtons") == 0)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        */

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ()

			.Build ();

		Social.localUser.Authenticate((bool success) => {
			if (success) 
			{
				Debug.Log("Login Successfull");
			}
			else
			{
				Debug.Log("Login Failed");
			}
		});

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		if (PlayerPrefs.GetInt ("highScore") >= 50) {
			Social.ReportProgress ("CgkI6bqjrY4LEAIQAg", 100.0f, (bool success) => {
			});
		}
		if (PlayerPrefs.GetInt ("highScore") >= 200) {
			Social.ReportProgress ("CgkI6bqjrY4LEAIQAw", 100.0f, (bool success) => {
			});
		}
		if (PlayerPrefs.GetInt ("highScore") >= 300) {
			Social.ReportProgress ("CgkI6bqjrY4LEAIQBA", 100.0f, (bool success) => {
			});
		}
		if (PlayerPrefs.GetInt ("highScore") >= 500) {
			Social.ReportProgress ("CgkI6bqjrY4LEAIQBQ", 100.0f, (bool success) => {
			});
		}
		if (PlayerPrefs.GetInt ("highScore") >= 1000) {
			Social.ReportProgress ("CgkI6bqjrY4LEAIQBg", 100.0f, (bool success) => {
			});
			highScorePosition = 2;
		}

		//records the high score in the leaderboard
		Social.ReportScore (PlayerPrefs.GetInt ("highScore"), "CgkI6bqjrY4LEAIQAQ", (bool success) => {
		});
	}

	
	void AchievementsAndLeaderBoard() 
	{
		GUIStyle howToFont = new GUIStyle ("Button");
		howToFont.fontSize = Screen.width / 24;

		if (GUI.Button (new Rect ((Screen.width/2 - Screen.width / 3) - Screen.width / 30, Screen.height/2 + Screen.height / 4.5f, Screen.width / 3, Screen.height / 10), "Leaderboard" , howToFont))
		{
            //Social.ShowLeaderboardUI ();
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI6bqjrY4LEAIQAQ");

        }

		if (GUI.Button (new Rect (Screen.width/2 + Screen.width / 30, Screen.height/2 + Screen.height / 4.5f, Screen.width / 3, Screen.height / 10), "Achievements" , howToFont))
		{
			Social.ShowAchievementsUI ();
		}
	}
	



//iOS specific methods ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    /*
	void SignIn() {

        Social.localUser.Authenticate(success => { if (success) { Debug.Log("iOS GC authenticate ok"); } else { Debug.Log("iOS GC authenticate Failed"); } });


        if (PlayerPrefs.GetInt("highScore") >= 50)
        {
            Social.ReportProgress("grp.CascadeRookie", 100.0f, (bool success) => {
            });
        }
        if (PlayerPrefs.GetInt("highScore") >= 200)
        {
            Social.ReportProgress("grp.CascadeUpAndComer", 100.0f, (bool success) => {
            });
        }
        if (PlayerPrefs.GetInt("highScore") >= 300)
        {
            Social.ReportProgress("grp.CascadeMaster", 100.0f, (bool success) => {
            });
        }
        if (PlayerPrefs.GetInt("highScore") >= 500)
        {
            Social.ReportProgress("grp.CascadeVeteran", 100.0f, (bool success) => {
            });
        }
        if (PlayerPrefs.GetInt("highScore") >= 1000)
        {
            Social.ReportProgress("grp.CascadeGodLike", 100.0f, (bool success) => {
            });
            highScorePosition = 2;
        }

        //records the high score in the leaderboard
        long scoreLong = PlayerPrefs.GetInt("highScore");
        Social.ReportScore(scoreLong, "grp.CascadeLeaderboard", success => {
            if (success)
            {
                Debug.Log("iOS leaderboard report score ok:");
            }
            else
            {
                Debug.Log("iOS leaderboard report score Failed:");
            }
        });
    }

    void AchievementsAndLeaderBoard()
	{
		GUIStyle howToFont = new GUIStyle ("Button");
		howToFont.fontSize = Screen.width / 24;

		if (GUI.Button (new Rect ((Screen.width/2 - Screen.width / 3) - Screen.width / 30, Screen.height/2 + Screen.height / 4.5f, Screen.width / 1.36f, Screen.height / 10), "Leaderboard / Achievements" , howToFont))
		{
			Social.ShowLeaderboardUI ();
		}
	}
    */
}
