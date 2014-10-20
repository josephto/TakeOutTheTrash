using UnityEngine;
using System.Collections;

public class ScoreUIGirl : MonoBehaviour {
	GameManager gameManager;
	GUIText scoreText;

	public GameObject player;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
		scoreText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = player.GetComponent<PlayerGirl>().score.ToString("c0");
	}
}
