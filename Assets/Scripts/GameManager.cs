using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public float initialTime;
	private int round = 1;
	public float time;
	public int numRounds;
	private bool gameover = false;

	public int boyScore;
	public int girlScore;
	
	// Use this for initialization
	void Start () {
		time = initialTime;
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