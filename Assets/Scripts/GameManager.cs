using UnityEngine;
using System.Collections;

// Define enums and make them accessible from other scripts
public enum Powerup { none, shield, magnet, faster };
public enum Attack { none, glue, invert, placeholder };

public class GameManager : MonoBehaviour {
	
	public float initialTime;
	private int round = 1;
	public float time;
	public int numRounds;
	private bool gameover = false;

	public int boyScore;
	public int girlScore;
	
	// Keep track of boy's and girl's powerups and attack items
	public Powerup boyPU = Powerup.none;
	public Powerup girlPU = Powerup.none;
	public Attack boyAttack = Attack.none;
	public Attack girlAttack = Attack.none;
	public int boyPUCount = 0;
	public int girlPUCount = 0;
	public int boyAttackCount = 0;
	public int girlAttackCount = 0;
	
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