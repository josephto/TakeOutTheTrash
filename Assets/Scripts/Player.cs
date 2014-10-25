using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float horizontalSpeed = 0.07f;
	//public float verticalSpeed = 0.04f;
	public float minz, maxz;
	public bool isBoy;
	private GameManager gameManager;
	private CameraMovement camera;
	public float rotationSpeed = 2;
	public float fallBackSpeed = 3; // the speed at which the character gets back to the horizontal orientation
	public float rotationRange = 60;
	private float rotation;
	private float objectRotation;
	private GameObject lastCollided;
	private Vector3 lastCollisionPoint;
	public AudioClip hitObstacleSound;
	public AudioClip collectTrashSound;

	//powerups
	private Powerup powerUp;
	private Attack attackItem;
	private int powerUpCount;
	private int attackItemCount;
	private bool isPUActive;
	private float puTime;
	private float attackTime;

	//shield
	private GameObject shield;
	private GameObject trashMagnet;
	
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rotation = 0;
		objectRotation = gameObject.transform.rotation.eulerAngles.y;
		if(isBoy){
			powerUp = gameManager.boyPU;
			powerUpCount = gameManager.boyPUCount;
			attackItem = gameManager.boyAttack;
			attackItemCount = gameManager.boyAttackCount;
		}else{
			powerUp = gameManager.girlPU;
			powerUpCount = gameManager.girlPUCount;
			attackItem = gameManager.girlAttack;
			attackItemCount = gameManager.girlAttackCount;
		}
		
		puTime = gameManager.PUTime;
		attackTime = gameManager.attackTime;

		shield = transform.FindChild ("Shield").gameObject;
		trashMagnet = transform.FindChild ("TrashMagnet").gameObject;
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
			if(Input.GetAxisRaw("Vertical") > 0 && rotation > -rotationRange + rotationSpeed) 
			{
				rotation -= rotationSpeed;
			} else if (Input.GetAxisRaw ("Vertical") < 0 && rotation < rotationRange - rotationSpeed) {
				rotation += rotationSpeed;
			} else if (rotation > 0) {
				rotation -= fallBackSpeed;
			} else if (rotation < 0) {
				rotation += fallBackSpeed;
			}

			if(Input.GetAxisRaw("BoyPowerUp") > 0 && powerUp != Powerup.none && !isPUActive){
				Debug.LogError (powerUpCount);
				if (powerUpCount > 0){
					isPUActive = true;
					powerUpCount--;
				}
			}

		}
		else{
			if(Input.GetAxisRaw("Vertical2") > 0 && rotation > -rotationRange + rotationSpeed) 
			{
				rotation -= rotationSpeed;
			} else if (Input.GetAxisRaw("Vertical2") < 0 && rotation < rotationRange - rotationSpeed) {
				rotation += rotationSpeed;
			} else if (rotation > 0) {
				rotation -= fallBackSpeed;
			} else if (rotation < 0) {
				rotation += fallBackSpeed;
			}

			if(Input.GetAxisRaw("GirlPowerUp") > 0 && powerUp != Powerup.none && !isPUActive){
				if (powerUpCount > 0){
					isPUActive = true;
					powerUpCount--;
				}
			}
		}
		velocity.x = horizontalSpeed * Mathf.Cos(rotation / 180 * Mathf.PI);
		velocity.z = -horizontalSpeed * Mathf.Sin(rotation / 180 * Mathf.PI);
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

		if (powerUp == Powerup.shield && isPUActive){
			if (!shield.activeSelf)
				shield.SetActive (true);
		}else if (powerUp == Powerup.magnet && isPUActive){
			if (!trashMagnet.activeSelf)
				trashMagnet.SetActive (true);
		}


		if(isPUActive){
			puTime -= Time.deltaTime;
			if (puTime < 0){
				isPUActive = false;
				puTime = gameManager.PUTime;
				shield.SetActive (false);
				trashMagnet.SetActive (false);
			}

		}

	}

	public void trashMagnetCollision(Collision collision){
		OnCollisionEnter (collision);
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
			if (powerUp == Powerup.shield && isPUActive){

			}else{
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
	}

	void OnCollisionExit(Collision collision) {
		//lastCollided = null;
	}
}
