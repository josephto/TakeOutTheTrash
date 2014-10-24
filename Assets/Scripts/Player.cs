using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float horizontalSpeed = 0.06f;
	public float verticalSpeed = 0.04f;
	public float minz, maxz;
	public bool isBoy;
	private GameManager gameManager;
	private CameraMovement camera;
	public float rotationSpeed = 5;
	private float rotation;
	private float objectRotation;
	private GameObject lastCollided;
	public AudioClip hitObstacleSound;
	public AudioClip collectTrashSound;
	
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
		if(isBoy){
			if(Input.GetAxisRaw("Vertical") > 0 && rotation > -45 + rotationSpeed) 
			{
				rotation -= rotationSpeed;
			} else if (Input.GetAxisRaw ("Vertical") < 0 && rotation < 45 - rotationSpeed) {
				rotation += rotationSpeed;
			}
		}
		else{
			if(Input.GetAxisRaw("Vertical2") > 0 && rotation > -45 + rotationSpeed) 
			{
				rotation -= rotationSpeed;
			} else if (Input.GetAxisRaw("Vertical2") < 0 && rotation < 45 - rotationSpeed) {
				rotation += rotationSpeed;
			}
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
			if (isBoy) {
				gameManager.boyScore += 10;
			} else {
				gameManager.girlScore += 10;
			}
			AudioSource.PlayClipAtPoint(collectTrashSound,Camera.main.transform.position);
		}
		else if (collider.CompareTag("Obstacle"))
		{
			GameObject collided = collider.gameObject;
			if (lastCollided != collided) { // so we don't keep deducting points for the same object
				if (isBoy) {
					if(gameManager.boyScore == 0){
						gameManager.girlScore += 10;
					}else{
						gameManager.boyScore -= 10;
					}
				} else {
					if(gameManager.girlScore == 0){
						gameManager.boyScore += 10;
					}else{
						gameManager.girlScore -= 10;
					}
				}
				AudioSource.PlayClipAtPoint(hitObstacleSound,Camera.main.transform.position+new Vector3(0,-100,0));
				lastCollided = collided;
			}
		}
	}

	void OnCollisionExit(Collision collision) {
		lastCollided = null;
	}
}
