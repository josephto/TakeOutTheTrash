using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// On generation, if a trash object collides with another object, destroy the trash
	void OnCollisionEnter(Collision collision) {
		Collider collider = collision.collider;
		if(collider.CompareTag("Obstacle") || collider.CompareTag("Trash"))
		{
			Die();
		}
	}

	public void Die() {
		Destroy(gameObject);
	}
}
