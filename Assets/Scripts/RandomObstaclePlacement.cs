using UnityEngine;
using System.Collections;

public class RandomObstaclePlacement : MonoBehaviour {

	public GameObject[] obstacles;
	public Transform[] constraints;
	private int num_Obstacles = 100;

	// Use this for initialization
	void Start () {
		float minx = constraints [0].position.x;
		float maxx = constraints [1].position.x;
		float minz = constraints [1].position.z;
		float maxz = constraints [0].position.z;
		for (int i = 0; i < num_Obstacles; i++){
			float x = Random.Range(minx,maxx);
			float z = Random.Range(minz,maxz);
			int obstacleIndex = Random.Range (0, obstacles.Length);
			Instantiate (obstacles[obstacleIndex],new Vector3(x, obstacles[obstacleIndex].transform.position.y, z), 
			             obstacles[obstacleIndex].transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
