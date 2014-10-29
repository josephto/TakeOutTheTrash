using UnityEngine;
using System.Collections;

public class AttackText : MonoBehaviour {
	private ShopManager shopManager;
	private GUIText attackText;
	
	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("ShopManager");
		shopManager = obj.GetComponent<ShopManager> ();
		attackText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (shopManager.attack) {
		case Attack.none:
			attackText.text = "Attack Items: \nSelect one from\nthe three";
			break;
		case Attack.glue:
			attackText.text = "Glue: $40\nFreezes your\nopponent's\nmovement for\n5 seconds";
			break;
		case Attack.invert:
			attackText.text = "Invert controls: $30\nInverts your\nopponent's\ncontrols for\n5 seconds";
			break;
		case Attack.lightoff:
			attackText.text = "Light Off: $15\nDarkens your\nopponent's scene\nfor 5 seconds";
			break;
		}
	}
}
