using UnityEngine;
using System.Collections;

public class ItemCount : MonoBehaviour {
	public int price = 100;
	public int count;
	private ShopManager shopManager;

	// Use this for initialization
	void Start () {
		count = 0;
		GameObject obj = GameObject.Find ("ShopManager");
		shopManager = obj.GetComponent<ShopManager> ();

		GUIText[] activeTexts = gameObject.GetComponentsInChildren<GUIText>();
		foreach (GUIText text in activeTexts) {
			text.enabled = false;
		}
		
		GUITexture[] activeTextures = gameObject.GetComponentsInChildren<GUITexture>();
		foreach (GUITexture texture in activeTextures) {
			texture.enabled = false;
		}
	}

	public void IncreaseCount() {
		if (shopManager.money >= price) {
			count++;
			shopManager.money -= price;
			if (gameObject.tag.Equals("PowerupCount")) {
				shopManager.PUCost += price;
				shopManager.PUCount++;
			} else if (gameObject.tag.Equals("AttackCount")) {
				shopManager.attackCost += price;
				shopManager.attackCount++;
			}
		}
	}

	public void DecreaseCount() {
		if (count > 0) {
			count--;
			shopManager.money += price;
			if (gameObject.tag.Equals("PowerupCount")) {
				shopManager.PUCost -= price;
				shopManager.PUCount--;
			} else if (gameObject.tag.Equals("AttackCount")) {
				shopManager.attackCost -= price;
				shopManager.attackCount--;
			}
		}
	}

	public void ResetCount() {
		count = 0;
	}
}
