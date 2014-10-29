using UnityEngine;
using System.Collections;

public class Winner : MonoBehaviour {

	private GameManager gameManager;
	private GUIText winnerText;
	private float rotation;
	public float rotationSpeed = 1;
	private int result = 0; // 1 = boy won, 2 = girl won, 0 = tie
	private GameObject boy;
	private GameObject girl;
	public Transform boyTrans;
	public Transform girlTrans;

	public Font font;

	// Use this for initialization
	void Start () {
		boy = GameObject.Find("Boy");
		girl = GameObject.Find("Girl");

		GameObject textObj = GameObject.Find("WinnerText");
		winnerText = textObj.GetComponent<GUIText>();
		GameObject g = GameObject.Find ("GameManager");

		if (g == null) {
			result = 0;
			winnerText.pixelOffset = new Vector2(-76.23f, -20.49f);
			winnerText.text = "Tie";
		} else {
			gameManager = g.GetComponent<GameManager> ();
			if (gameManager.boyScore > gameManager.girlScore) {
				winnerText.text = "Brother won";
				result = 1;
			} else if (gameManager.girlScore > gameManager.boyScore) {
				winnerText.pixelOffset = new Vector2(-229.47f, -20.49f);
				winnerText.text = "Sister won";
				result = 2;
			} else {
				winnerText.pixelOffset = new Vector2(-76.23f, -20.49f);
				winnerText.text = "Tie";
			}
		}
		winnerText.enabled = false;
	}

	private float timer = 0;
	private bool finished = false;
	
	// Update is called once per frame
	void Update () {
		if (result != 0 && rotation < 90) {
			rotation += rotationSpeed;
			rotationSpeed += 0.1f;
			if (result == 1) {
				girl.transform.RotateAround(girlTrans.position, new Vector3(-1, 0, 0), rotationSpeed);
			} else {
				boy.transform.RotateAround(boyTrans.position, new Vector3(-1, 0, 0), rotationSpeed);
			}
			if (rotation >= 90) {
				if (result == 1) {
					girl.renderer.enabled = false;
				} else {
					boy.renderer.enabled = false;
				}
			}
		}
		else if (timer < 1) {
			timer += Time.deltaTime;
			if (timer >= 0.5) {
				winnerText.enabled = true;
				finished = true;
			}
		}
	}

	void OnGUI (){
		if (finished) {
			GUI.skin.button.font = font;
			GUI.skin.button.fontSize = 40;
			
			GUILayout.BeginArea(new Rect(Screen.width * 3.0f/8, Screen.height * 0.7f, Screen.width / 2, Screen.height / 2));
			// Load the main scene
			// The scene needs to be added into build setting to be loaded!
			if (GUILayout.Button("Start Over", GUILayout.Height(60), GUILayout.Width(Screen.width / 4)))
			{
				gameManager.Reset();
				Application.LoadLevel("GamePlay");
			}
			if (GUILayout.Button("Quit", GUILayout.Height(60), GUILayout.Width(Screen.width / 4)))
			{
				Application.Quit();
			}
			GUILayout.EndArea();
		}
	}
}
