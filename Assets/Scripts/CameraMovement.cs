	using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public string playerName;
	public float distanceFromPlayer = 7;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find(playerName);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = gameObject.transform.position;
		position.x = player.transform.position.x + distanceFromPlayer;
		gameObject.transform.position = position;
	}
}
