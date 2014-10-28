using UnityEngine;
using System.Collections;

public class MoneyText : MonoBehaviour {
	private ShopManager shopManager;
	private GUIText moneyText;

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("ShopManager");
		shopManager = obj.GetComponent<ShopManager> ();
		moneyText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "Money Allowance : " + shopManager.money.ToString();
	}
}
