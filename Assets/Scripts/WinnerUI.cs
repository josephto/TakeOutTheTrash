using UnityEngine;
using System.Collections;

public class WinnerUI : MonoBehaviour {

	GameManager gameManager;
	GUIText winnerText;
	
	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
		winnerText = gameObject.GetComponent<GUIText>();
		if (gameManager.boyScore > gameManager.girlScore) {
			winnerText.text = "Boy won";
		} else if (gameManager.girlScore > gameManager.boyScore) {
			winnerText.text = "Girl won";
		} else {
			winnerText.text = "Tie";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
