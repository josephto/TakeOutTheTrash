using UnityEngine;
using System.Collections;

// Define enums and make them accessible from other scripts
public enum Powerup { none, shield, magnet, faster };
public enum Attack { none, glue, invert, lightoff };

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

	//how long the attack will be in effect for opposing player
	public float attackTime;
	//how long the powerup will be in effect themselves
	public float PUTime;

	private bool isShop;
	
	// Use this for initialization
	void Start () {
		time = initialTime;
		isShop = false;
	}
	
	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
//		//for test
//		boyPU = Powerup.shield;
//		girlPU = Powerup.magnet;
//		boyPUCount = 2;
//		girlPUCount = 2;
//		girlAttack = Attack.invert;
//		boyAttack = Attack.invert;
//		boyAttackCount = 5;
//		girlAttackCount = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameover){
			if (!isShop) {
				time -= Time.deltaTime;
			}
			if (time <= 0){
				time = initialTime;
				round++;
				if (round <= numRounds){
					Application.LoadLevel ("Shop");
					isShop = true;
				}else{
					Application.LoadLevel ("Winner");
					gameover = true;
				}
			}
		}
	}

	public void LoadNextRound() {
		if (round < numRounds){
			Application.LoadLevel ("Round2");
		}else if(round == numRounds){
			Application.LoadLevel ("Round3");
		}
		isShop = false;
	}

	public void Reset() {
		boyPU = Powerup.none;
		girlPU = Powerup.none;
		boyPUCount = 0;
		girlPUCount = 0;
		girlAttack = Attack.none;
		boyAttack = Attack.none;
		boyAttackCount = 0;
		girlAttackCount = 0;
		boyScore = 0;
		girlScore = 0;
		gameover = false;
		round = 1;
	}
}