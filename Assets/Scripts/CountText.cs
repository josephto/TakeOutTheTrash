using UnityEngine;
using System.Collections;

public class CountText : MonoBehaviour {
	public GameObject obj;
	private ItemCount itemCount;
	private GUIText countText;
	
	// Use this for initialization
	void Start () {
		itemCount = obj.GetComponent<ItemCount>();
		countText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		countText.text = itemCount.count.ToString();
	}
}
