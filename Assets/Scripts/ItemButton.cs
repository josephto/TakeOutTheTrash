using UnityEngine;
using System.Collections;

public class ItemButton : MonoBehaviour {
	private bool isPU;
	string name;
	private ShopManager shopManager;
	private float minx, maxx, miny, maxy;

	// Use this for initialization
	void Start () {
		name = gameObject.name;
		if (name.Equals("shield") || name.Equals("magnet") || name.Equals("faster")) {
			isPU = true;
		} else {
			isPU = false;
		}

		GameObject obj = GameObject.Find ("ShopManager");
		shopManager = obj.GetComponent<ShopManager> ();

		GUITexture button = gameObject.GetComponent<GUITexture>();
		Rect rect = button.pixelInset;
		minx = rect.xMin + Screen.width/2;
		maxx = rect.xMax + Screen.width/2;
		miny = rect.yMin + Screen.height/2;
		maxy = rect.yMax + Screen.height/2;
	}
	
	void HandleClick() {
		bool reset = shopManager.Reset(name);
		if (reset) {
			string tag;
			if (isPU) {
				tag = "PowerupCount";
			} else {
				tag = "AttackCount";
			}
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

			GameObject activeObj = GameObject.Find(name + "count");

			GUIText[] activeTexts = activeObj.GetComponentsInChildren<GUIText>();
			foreach (GUIText text in activeTexts) {
				text.enabled = true;
			}
			
			GUITexture[] activeTextures = activeObj.GetComponentsInChildren<GUITexture>();
			foreach (GUITexture texture in activeTextures) {
				texture.enabled = true;
			}
		}
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 pos = Input.mousePosition;
			if (pos.x > minx && pos.x < maxx && pos.y > miny && pos.y < maxy) {
				HandleClick();
			}
		}
	}
}
