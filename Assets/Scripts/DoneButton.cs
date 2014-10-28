using UnityEngine;
using System.Collections;

public class DoneButton : MonoBehaviour {
	private ShopManager shopManager;
	private float minx, maxx, miny, maxy;

	// Use this for initialization
	void Start () {
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
		shopManager.Next();
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
