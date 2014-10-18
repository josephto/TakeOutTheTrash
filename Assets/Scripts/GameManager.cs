using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float time = 10f;
	public float boyScore;
	public float girlScore;
	private int round = 1;
	public int numRounds = 3;
	private bool gameover = false;

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
		if (!gameover){
			time -= Time.deltaTime;
			if (time <= 0){
				time = 10f;
				round++;
				if (round < numRounds){
					Application.LoadLevel ("Round2");
				}else if(round == numRounds){
					Application.LoadLevel ("Round3");
				}else{
					Application.LoadLevel ("Winner");
					gameover = true;
				}
			}
		}
	}
}
