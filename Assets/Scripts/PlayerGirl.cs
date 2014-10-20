using UnityEngine;
using System.Collections;

public class PlayerGirl : MonoBehaviour {
	
	public float horizontalSpeed = 0.06f;
	public float verticalSpeed = 0.04f;
	public float minz, maxz;
	private GameManager gameManager;
	private CameraMovement camera;
	public float rotationSpeed = 5;
	private float rotation;
	private float objectRotation;
	private GameObject lastCollided;

	public int score = 0;
	public GameObject other; // the other player
	private Player otherPlayer;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find("Main Camera2").GetComponent<CameraMovement>();
		if (other != null) {
			otherPlayer = other.GetComponent<Player>();
		}

		rotation = 0;
		objectRotation = gameObject.transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity;
		if (lastCollided == null) {
			velocity = new Vector3(horizontalSpeed, 0, 0);
		} else {
			velocity = new Vector3(0, 0, 0);
		}
		if(Input.GetAxisRaw("Vertical2") > 0 && rotation > -45 + rotationSpeed) 
		{
			rotation -= rotationSpeed;
		} else if (Input.GetAxisRaw("Vertical2") < 0 && rotation < 45 - rotationSpeed) {
			rotation += rotationSpeed;
		}
		velocity.z = -verticalSpeed * Mathf.Tan(rotation / 180 * Mathf.PI);
		gameObject.transform.rotation = Quaternion.Euler(0, objectRotation + rotation, 0);
		Vector3 position = gameObject.transform.position;
		position += velocity;
		if (position.z > maxz) {
			position.z = maxz;
		}
		if (position.z < minz) {
			position.z = minz;
		}
		gameObject.transform.position = position;
	}

	void OnCollisionEnter(Collision collision) {
		Collider collider = collision.collider;
		if(collider.CompareTag("Trash"))
		{
			Trash trash = collider.gameObject.GetComponent< Trash >();
			trash.Die();
			score += 10;
		}
		else if (collider.CompareTag("Obstacle"))
		{
			GameObject collided = collider.gameObject;
			if (lastCollided != collided) { // so we don't keep deducting points for the same object
				if(score == 0 && otherPlayer != null){
					otherPlayer.score += 10;
				}else{
					score -= 10;
				}
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
