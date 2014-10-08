using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour {
	Boy boy;
	GUIText scoreText;

	// Use this for initialization
	void Start () {
		GameObject b = GameObject.Find("Boy");
		boy = b.GetComponent<Boy>();
		scoreText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = boy.score.ToString();
	}
}
