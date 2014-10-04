using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {

	public float movementSpeed;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() 
	{ 
		if( Input.GetAxisRaw("Vertical") > 0 ) 
		{ 
			Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
			                                  gameObject.transform.position.z+movementSpeed);
			gameObject.transform.position = newPosition;
		}else if (Input.GetAxisRaw ("Vertical") < 0){
			Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
			                                  gameObject.transform.position.z-movementSpeed);
			gameObject.transform.position = newPosition;
		}

		//test
	}

	// Update is called once per frame
	void Update () {
	
	}
}
