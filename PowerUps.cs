using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }


    void OnGUI()
    {
        GUIStyle buttonFont = new GUIStyle("Button");
        buttonFont.fontSize = Screen.width / 19;


        GUI.color = Color.black;

        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 2.7f, Screen.height / 2 + Screen.height / 3, Screen.width / 3, Screen.height / 8), "Main \n Menu", buttonFont))
        {
            Application.LoadLevel("Menu");
        }
    }
	
}
