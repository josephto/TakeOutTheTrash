using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {
	private GUIStyle buttonStyle;
	public Font font;

	void OnGUI (){
		GUI.skin.button.font = font;
		GUI.skin.button.fontSize = 40;
		
		GUILayout.BeginArea(new Rect(Screen.width * 3.0f/8, Screen.height * 0.83f, Screen.width / 2, Screen.height / 2));
		if (GUILayout.Button("Back", GUILayout.Height(60), GUILayout.Width(Screen.width / 4)))
		{
			Application.LoadLevel("Start");
		}
		GUILayout.EndArea();
	}
}
