using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float time = 10f;
	public float boyScore;
	public float girlScore;

	// Use this for initialization
	void Start () {
		boyScore = 0;
		girlScore = 0;
	}

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time <= 0){
			time = 10f;
			Application.LoadLevel ("Round2");
		}
	}
}
