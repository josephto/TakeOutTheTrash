using UnityEngine;
using System.Collections;

public class StartUI : MonoBehaviour {
	private GUIStyle buttonStyle;
	public Font font;
	
	void OnGUI (){
		GUI.skin.button.font = font;
		GUI.skin.button.fontSize = 40;

		GUILayout.BeginArea(new Rect(Screen.width * 3.0f/8, Screen.height / 2 + 130, Screen.width / 2, Screen.height / 2));
		// Load the main scene
		// The scene needs to be added into build setting to be loaded!
		if (GUILayout.Button("Start Game", GUILayout.Height(60), GUILayout.Width(Screen.width / 4)))
		{
			Application.LoadLevel("GamePlay");
		}
		if (GUILayout.Button("Instructions", GUILayout.Height(60), GUILayout.Width(Screen.width / 4)))
		{
			Application.LoadLevel("Instructions");
		}
		if (GUILayout.Button("Quit", GUILayout.Height(60), GUILayout.Width(Screen.width / 4)))
		{
			Application.Quit();
		}
		GUILayout.EndArea();
	}
}
