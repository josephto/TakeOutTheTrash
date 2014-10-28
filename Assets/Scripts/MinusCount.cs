using UnityEngine;
using System.Collections;

public class MinusCount : MonoBehaviour {
	public GameObject obj;
	private ItemCount itemCount;
	private float minx, maxx, miny, maxy;
	
	// Use this for initialization
	void Start () {
		itemCount = obj.GetComponent<ItemCount>();
		GUITexture button = gameObject.GetComponent<GUITexture>();
		Rect rect = button.pixelInset;
		minx = rect.xMin + Screen.width/2;
		maxx = rect.xMax + Screen.width/2;
		miny = rect.yMin + Screen.height/2;
		maxy = rect.yMax + Screen.height/2;
	}
	
	void HandleClick() {
		Debug.Log("minus");
		itemCount.DecreaseCount();
	}

	void Update() {
		if (gameObject.GetComponent<GUITexture>().enabled) {
			if (Input.GetMouseButtonDown(0)) {
				Vector3 pos = Input.mousePosition;
				if (pos.x > minx && pos.x < maxx && pos.y > miny && pos.y < maxy) {
					HandleClick();
				}
			}
		}
	}
}
