using UnityEngine;
using System.Collections;

public class ItemsIcon : MonoBehaviour {

	public bool isBoy;
	public bool isAttack;
	private GameManager gameManager;
	private GUITexture icon;
	private GUIText text;
	
	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
		icon = gameObject.GetComponent<GUITexture>();
		text = gameObject.GetComponent<GUIText> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttack) {
			text.text = isBoy? gameManager.boyAttackCount.ToString("0"):gameManager.girlAttackCount.ToString("0");
			text.guiText.pixelOffset = new Vector2(120, 30);
			switch (isBoy ? gameManager.boyAttack : gameManager.girlAttack) {
			case Attack.glue:
				icon.texture = Resources.Load ("glue") as Texture2D;
				break;
			case Attack.invert:
				icon.texture = Resources.Load ("invert") as Texture2D;
				break;
			case Attack.lightoff:
				icon.texture = Resources.Load ("lightoff") as Texture2D;
				break;
			case Attack.none:
				icon.texture = Resources.Load ("null") as Texture2D;
				break;
			}
		} else {
			text.text = isBoy? gameManager.boyPUCount.ToString("0"):gameManager.girlPUCount.ToString("0");
			text.guiText.pixelOffset = new Vector2(60, 30);
			switch (isBoy ? gameManager.boyPU : gameManager.girlPU) {
			case Powerup.faster:
				icon.texture = Resources.Load ("faster") as Texture2D;
				break;
			case Powerup.magnet:
				icon.texture = Resources.Load ("magnet") as Texture2D;
				break;
			case Powerup.shield:
				icon.texture = Resources.Load ("shield") as Texture2D;
				break;
			case Powerup.none:
				icon.texture = Resources.Load ("null") as Texture2D;
				break;
			}
		}

	}
}
