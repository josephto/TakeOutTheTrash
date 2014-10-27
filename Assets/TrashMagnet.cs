using UnityEngine;
using System.Collections;

public class TrashMagnet : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start(){ 
		player = gameObject.GetComponentInParent<Player> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
	}

	void OnCollisionEnter(Collision collision){
		Collider collider = collision.collider;
		if(collider.CompareTag("Trash")){
			player.trashMagnetCollision(collision);
		}
	}

	void OnCollisionStay(Collision collision){
		Collider collider = collision.collider;
		if(collider.CompareTag("Trash")){
			player.trashMagnetCollision(collision);
		}
	}
}
