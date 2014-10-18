using UnityEngine;
using System.Collections;

public class TimeUI : MonoBehaviour {

	GameManager gameManager;
	GUIText timeText;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
		timeText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = gameManager.time.ToString ("0");
	}
}
