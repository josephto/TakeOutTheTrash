using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour {
	GameManager gameManager;
	GUIText scoreText;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
		scoreText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = gameManager.boyScore.ToString("c0");
	}
}
