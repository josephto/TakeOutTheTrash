using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float horizontalSpeed = 0.07f;
	//public float verticalSpeed = 0.04f;
	public float minz, maxz;
	public bool isBoy;
	public Player otherPlayer;
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
	public GameObject light;
	public GameObject mesh;
	public Material frozenMat;
	public Material invertMat;
	public Material normalMat;

	//powerups
	private Powerup powerUp;
	private Attack attackItem;
	private int powerUpCount;
	private int attackItemCount;
	private bool isPUActive;
	private float puTime;
	private float attackTime;

	//attack status
	private bool isFreeze;
	private bool isInvert;
	private bool isLightOff;


	//shield
	private GameObject shield;
	private GameObject trashMagnet;
	private float originalSpeed;
	
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		light = isBoy? GameObject.Find("BoyLight").gameObject: GameObject.Find("GirlLight").gameObject;
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
		originalSpeed = horizontalSpeed;

		isFreeze = false;
		isInvert = false;
		isLightOff = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isLightOff) {
			light.light.intensity = 0;
			attackTime -= Time.deltaTime;
			if(attackTime <= 0){
				attackTime = gameManager.attackTime;
				light.light.intensity = 2;
				isLightOff = false;
			}
		}
		if (isFreeze) {
			animation.enabled = false;
			attackTime -= Time.deltaTime;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			mesh.renderer.material = frozenMat;

			if(attackTime <= 0){
				mesh.renderer.material = normalMat;
				attackTime = gameManager.attackTime;
				isFreeze = false;
				animation.enabled = true;
				rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ |RigidbodyConstraints.FreezePositionY;

			}
		}
		else{
			Vector3 velocity;
			if (lastCollided == null) {
				velocity = new Vector3(horizontalSpeed, 0, 0);
			} else {
				velocity = new Vector3(0, 0, 0);
			}
			bool isUp, isDown, isPowerUp, isAttack;
			if(isInvert){
				mesh.renderer.material = invertMat;
				attackTime -= Time.deltaTime;
				isUp = isBoy? Input.GetAxisRaw("Vertical") < 0: Input.GetAxisRaw("Vertical2") < 0;
				isDown = isBoy? Input.GetAxisRaw("Vertical") > 0: Input.GetAxisRaw("Vertical2") > 0;
				if(attackTime <= 0){
					mesh.renderer.material = normalMat;
					attackTime = gameManager.attackTime;
					isInvert = false;
				}
			}
			else{
				isUp = isBoy? Input.GetAxisRaw("Vertical") > 0: Input.GetAxisRaw("Vertical2") > 0;
				isDown = isBoy? Input.GetAxisRaw("Vertical") < 0: Input.GetAxisRaw("Vertical2") < 0;
			}
			isPowerUp = isBoy? Input.GetAxisRaw("BoyPowerUp") > 0 : Input.GetAxisRaw("GirlPowerUp") > 0;
			isAttack = isBoy? Input.GetAxisRaw("BoyPowerUp") < 0 : Input.GetAxisRaw("GirlPowerUp") < 0;

			if(isUp && rotation > -rotationRange + rotationSpeed) 
			{
				rotation -= rotationSpeed;
			} else if (isDown && rotation < rotationRange - rotationSpeed) {
				rotation += rotationSpeed;
			} else if (rotation > 0.0001f) {
				rotation -= Mathf.Min(fallBackSpeed, rotation);
			} else if (rotation < -0.0001f) {
				rotation += Mathf.Min(fallBackSpeed, -rotation);
			}

			if(isPowerUp && powerUp != Powerup.none && !isPUActive){
				if (powerUpCount > 0){
					isPUActive = true;
					powerUpCount = isBoy? --(gameManager.boyPUCount): --(gameManager.girlPUCount);;
				}
			}
			if(isAttack && attackItemCount > 0 && !(otherPlayer.isFreeze||otherPlayer.isInvert||otherPlayer.isLightOff)){
				if(attackItem == Attack.glue){
					otherPlayer.isFreeze = true;

				}
				if(attackItem == Attack.invert){
					otherPlayer.isInvert = true;
				}
				if(attackItem == Attack.lightoff){
					otherPlayer.isLightOff = true;
				}
				attackItemCount = isBoy? --(gameManager.boyAttackCount): --(gameManager.girlAttackCount);
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
			}else if (powerUp == Powerup.faster && isPUActive){
				if (horizontalSpeed == originalSpeed)
					horizontalSpeed = horizontalSpeed*2;
			}


			if(isPUActive){
				puTime -= Time.deltaTime;
				if (puTime < 0){
					isPUActive = false;
					puTime = gameManager.PUTime;
					shield.SetActive (false);
					trashMagnet.SetActive (false);
					horizontalSpeed = originalSpeed;
				}

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
