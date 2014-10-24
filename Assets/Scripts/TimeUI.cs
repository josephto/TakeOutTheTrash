using UnityEngine;
using System.Collections;

public class TimeUI : MonoBehaviour {

	private GameManager gameManager;
	private GUIText timeText;
	public Font font;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
		timeText = gameObject.GetComponent<GUIText>();
		timeText.font = font;
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = gameManager.time.ToString ("0");
	}
}
