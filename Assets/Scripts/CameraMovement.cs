using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float initialSpeed = 0.06f;
	public float speed;

	// Use this for initialization
	void Start () {
		speed = initialSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3 (gameObject.transform.position.x + speed, gameObject.transform.position.y, gameObject.transform.position.z);
		gameObject.transform.position = newPosition;
	}
}
