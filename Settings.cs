using UnityEngine;
using System.Collections;
//Does Contain Platform Specific Methods

public class Settings : MonoBehaviour {

    public Font MagnetoB;
    public Font Veranda;

    public float InstructionsSlider;
    public float AndroidButtonsSlider; //Just for Android ------------\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\

    // Use this for initialization
    void Start () {
        InstructionsSlider = PlayerPrefs.GetInt("InstructionsSetting");
    }

    void OnGUI()
    {
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.color = Color.black;

        GUIStyle buttonFont = new GUIStyle("Button");
        buttonFont.fontSize = Screen.width / 19;

        GUIStyle instructionsFont = new GUIStyle("Label");
        instructionsFont.fontSize = Screen.width / 24;

        GUIStyle settingsFont = new GUIStyle("Label");
        settingsFont.fontSize = Screen.width / 18;

        GUI.color = Color.black;
        GUI.skin.font = Veranda;
        GUI.Label(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2 - Screen.height / 2.5f, Screen.width - Screen.width / 6, Screen.height / 7.5f), "Tap the sides of the screen to move the triangle. Go through the walls to get to the other side. Don't let the blocks hit the ground!", instructionsFont);

        GUI.skin.font = MagnetoB;
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 6.5f, Screen.height / 2 + Screen.height / 3, Screen.width / 3, Screen.height / 8), "Main \n Menu", buttonFont))
        {
            Application.LoadLevel("Menu");
        }






        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 3f, Screen.height / 2 - Screen.height / 4, Screen.width / 2.3f, Screen.height / 6), "On Screen Instructions", settingsFont);

        
        if (PlayerPrefs.GetInt("InstructionsSetting") == 1)
        {
            GUI.color = Color.green;
        }
        

        InstructionsSlider = GUI.HorizontalSlider(new Rect(Screen.width / 2 + Screen.width / 4, Screen.height / 2 - Screen.height / 5.7f, Screen.width / 8, Screen.height / 10), InstructionsSlider, 0.0F, 1.0F);

        if (PlayerPrefs.GetInt("GamesPlayed") < 2)
        {
            InstructionsSlider = 1f;
        }
        

        if (InstructionsSlider <= .499f)
        {
            InstructionsSlider = 0;
            PlayerPrefs.SetInt("InstructionsSetting", 0);
        }
        else
        {
            InstructionsSlider = 1f;
            PlayerPrefs.SetInt("InstructionsSetting", 1);
        }




        AndroidButtons(); //Just for Android ------------\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\



    }




    //Android Specific Methods -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void AndroidButtons()
    {
        GUI.color = Color.black;

        GUIStyle settingsFont = new GUIStyle("Label");
        settingsFont.fontSize = Screen.width / 18;

        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 3f, Screen.height / 2 + Screen.height / 7, Screen.width / 2f, Screen.height / 6), "Navigation Bar (Soft Keys)", settingsFont);


        if (PlayerPrefs.GetInt("AndroidButtons") == 0)
        {
            AndroidButtonsSlider = 0;
            GUI.color = Color.black;
        }
        else
        {
            AndroidButtonsSlider = 1;
            GUI.color = Color.green;
        }

        AndroidButtonsSlider = GUI.HorizontalSlider(new Rect(Screen.width / 2 + Screen.width / 4, Screen.height / 2 + Screen.height / 5, Screen.width / 8, Screen.height / 10), AndroidButtonsSlider, 0.0F, 1.0F);

        if (AndroidButtonsSlider <= .499f)
        {
            AndroidButtonsSlider = 0;
            PlayerPrefs.SetInt("AndroidButtons", 0);
            Screen.fullScreen = true;
        }
        else
        {
            AndroidButtonsSlider = 1;
            PlayerPrefs.SetInt("AndroidButtons", 1);
            Screen.fullScreen = false;
        }

    }
}
