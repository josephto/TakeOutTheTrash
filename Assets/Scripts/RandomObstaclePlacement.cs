using UnityEngine;
using System.Collections;

public class RandomObstaclePlacement : MonoBehaviour {
	
	public GameObject[] objects; // obstacles and trash
	public Transform[] constraints;
	public int numObjects;
	
	// Use this for initialization
	void Start () {
		float minx = constraints [0].position.x;
		float maxx = constraints [1].position.x;
		float minz = constraints [1].position.z;
		float maxz = constraints [0].position.z;
		float stepx = (maxx - minx) / numObjects;
		for (int i = 0; i < numObjects; i++){
			float localminx = stepx * i;
			float localmaxx = stepx * (i + 1);
			float x = Random.Range(localminx,localmaxx);
			float z = Random.Range(minz,maxz);
			int objectIndex = Random.Range(0, objects.Length);
			Instantiate (objects[objectIndex],new Vector3(x, objects[objectIndex].transform.position.y, z), 
			             objects[objectIndex].transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}