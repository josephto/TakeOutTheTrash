using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour {
	public bool isBoy;
	private GameManager gameManager;
	private GUIText scoreText;
	public Font font;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
		scoreText = gameObject.GetComponent<GUIText>();
		scoreText.font = font;
	}
	
	// Update is called once per frame
	void Update () {
		if (isBoy) {
			scoreText.text = gameManager.boyScore.ToString("c0");
		} else {
			scoreText.text = gameManager.girlScore.ToString("c0");
		}
	}
}
