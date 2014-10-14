using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {

	public float movementSpeed;
	public float minz, maxz;
	private GameManager gameManager;
	CameraMovement camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Vertical") > 0 && gameObject.transform.position.z < maxz - movementSpeed) 
		{ 
			Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
			                                  gameObject.transform.position.z+movementSpeed);
			gameObject.transform.position = newPosition;
		} else if (Input.GetAxisRaw ("Vertical") < 0 && gameObject.transform.position.z > minz + movementSpeed) {
			Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
			                                  gameObject.transform.position.z-movementSpeed);
			gameObject.transform.position = newPosition;
		}

		// This seems hacky, but I can't use onCollision without rigid body
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	private GameObject lastCollided;

	void OnCollisionEnter(Collision collision) {
		Collider collider = collision.collider;
		if(collider.CompareTag("Trash"))
		{
			Trash trash = collider.gameObject.transform.parent.GetComponent< Trash >();
			trash.Die();
			gameManager.boyScore += 10;
		}
		else if (collider.CompareTag("Obstacle"))
		{
			GameObject collided = collider.gameObject;
			if (lastCollided != collided) { // so we don't keep deducting points for the same object
				gameManager.boyScore -= 10;
				lastCollided = collided;
			}
			camera.speed = 0;
		}
	}

	void OnCollisionExit(Collision collision) {
		camera.speed = camera.initialSpeed;
		lastCollided = null;
	}
}
