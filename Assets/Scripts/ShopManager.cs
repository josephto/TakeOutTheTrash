using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {
	private GameManager gameManager;
	private GUITexture playerIcon;
	private bool isGirl;
	public Powerup powerup;
	public int PUCount;
	public Attack attack;
	public int attackCount;

	public int money = 0;
	public int PUCost;
	public int attackCost;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager");
		if (g != null) { // for debugging
			gameManager = g.GetComponent<GameManager> ();
			money = gameManager.girlScore;
		}

		GameObject obj = GameObject.Find("playericon");
		playerIcon = obj.GetComponent<GUITexture>();

		isGirl = true;
		powerup = Powerup.none;
		PUCount = 0;
		attack = Attack.none;
		attackCount = 0;
		PUCost = 0;
		attackCost = 0;
	}

	public bool Reset(string itemName) {
		if (itemName.Equals("shield") || itemName.Equals("magnet") || itemName.Equals("faster")) {
			Powerup newPU;
			if (itemName.Equals("shield")) {
				newPU = Powerup.shield;
			} else if (itemName.Equals("magnet")) {
				newPU = Powerup.magnet;
			} else {
				newPU = Powerup.faster;
			}

			if (newPU != powerup) {
				money += PUCost;
				PUCost = 0;
				PUCount = 0;
				return true;
			}

		} else {
			Attack newAttack;
			if (itemName.Equals("glue")) {
				newAttack = Attack.glue;
			} else if (itemName.Equals("invert")) {
				newAttack = Attack.invert;
			} else {
				newAttack = Attack.lightoff;
			}

			if (newAttack != attack) {
				money += attackCost;
				attackCost = 0;
				attackCount = 0;
				return true;
			}
		}
		return false;
	}

	public void Next() {
		if (gameManager != null) { // for debugging
			if (isGirl) {
				gameManager.girlScore = money;
				gameManager.girlPU = powerup;
				gameManager.girlPUCount = PUCount;
				gameManager.girlAttack = attack;
				gameManager.girlAttackCount = attackCount;

				isGirl = false;
				playerIcon.texture = Resources.Load ("boyface") as Texture2D;
				powerup = Powerup.none;
				PUCount = 0;
				attack = Attack.none;
				attackCount = 0;
				money = gameManager.boyScore;
				PUCost = 0;
				attackCost = 0;
				clearItemCount("PowerupCount");
				clearItemCount("AttackCount");
			}
			else {
				gameManager.boyScore = money;
				gameManager.boyPU = powerup;
				gameManager.boyPUCount = PUCount;
				gameManager.boyAttack = attack;
				gameManager.boyAttackCount = attackCount;
				gameManager.LoadNextRound();
			}
		}
		Debug.Log("next");
	}

	void clearItemCount(string tag) {
		GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject obj in objects) {
			GUIText[] texts = obj.GetComponentsInChildren<GUIText>();
			foreach (GUIText text in texts) {
				text.enabled = false;
			}
			
			GUITexture[] textures = obj.GetComponentsInChildren<GUITexture>();
			foreach (GUITexture texture in textures) {
				texture.enabled = false;
			}
			
			ItemCount itemCount = obj.GetComponent<ItemCount>();
			itemCount.ResetCount();
		}
	}
}
