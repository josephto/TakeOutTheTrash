using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public float initialTime;
	public float boyScore;
	public float girlScore;
	private int round = 1;
	public float time;
	public int numRounds;
	private bool gameover = false;
	
	// Use this for initialization
	void Start () {
		time = initialTime;
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
				time = initialTime;
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