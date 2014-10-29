using UnityEngine;
using System.Collections;

public class PUText : MonoBehaviour {
	private ShopManager shopManager;
	private GUIText puText;

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("ShopManager");
		shopManager = obj.GetComponent<ShopManager> ();
		puText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (shopManager.powerup) {
		case Powerup.none:
			puText.text = "Power-ups: \nSelect one from\nthe three";
			break;
		case Powerup.magnet:
			puText.text = "Trash Magnet: $50\nAttracts trash\naround you for 5\nseconds";
			break;
		case Powerup.shield:
			puText.text = "Shield: $30\nFor 5 seconds,\nprevents you from\nlosing points\nwhen you run into\nobstacles";
			break;
		case Powerup.faster:
			puText.text = "Speed Up: $20\nAllows you to\nmove at twice\nthe speed for 5\nseconds";
			break;
		}
	}
}
