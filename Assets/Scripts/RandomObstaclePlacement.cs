using UnityEngine;
using System.Collections;

public class RandomObstaclePlacement : MonoBehaviour {
	
	public GameObject[] furniture;
	public GameObject[] trash;
	//public float furnitureProb; // the probability that a random object is furniture, should be [0, 1]
	public Transform[] constraints;
	public float ratio; // the ratio of trash to furniture
	
	// Use this for initialization
	void Start () {
		// Compute size of grid cells
		Vector3[] furnitureSizes = new Vector3[furniture.Length];
		Vector3[] trashSizes = new Vector3[trash.Length];
		float stepx = 0;
		float stepz = 0;
		computeStepAndSize(furniture, furnitureSizes, ref stepx, ref stepz);
		computeStepAndSize(trash, trashSizes, ref stepx, ref stepz);

		stepx *= 2;
		stepz *= 2.2f;
		
		float minx = constraints[0].position.x;
		float maxx = constraints[1].position.x;
		float minz = constraints[0].position.z;
		float maxz = constraints[1].position.z;
		if (minx > maxx) {
			float temp = minx;
			minx = maxx;
			maxx = temp;
		}
		if (minz > maxz) {
			float temp = minz;
			minz = maxz;
			maxz = temp;
		}
		int xcount = (int)(Mathf.Abs(maxx - minx) / stepx);
		int zcount = (int)(Mathf.Abs(maxz - minz) / stepz);

		// Generate furniture
		for (int i = 0; i < xcount; i++) {
			for (int j = 0; j < zcount; j++) {
				int idx = Random.Range(0, furniture.Length);
				Vector3 size = furnitureSizes[idx];

				float localminx = minx + stepx * i;
				float localmaxx = minx + stepx * (i + 1);
				float localminz = minz + stepz * j;
				float localmaxz = minz + stepz * (j + 1);

				float x = Random.Range(localminx + 0.6f * size.x, localmaxx - 0.6f * size.x);
				float z = Random.Range(localminz + 0.7f * size.z, localmaxz - 0.7f * size.z);
				Instantiate (furniture[idx],new Vector3(x, furniture[idx].transform.position.y, z), 
				             furniture[idx].transform.rotation);
			}
		}

		// Generate trash
		int numTrash = (int)(xcount * zcount * ratio);
		for (int i = 0; i < numTrash; i++) {
			int idx = Random.Range(0, trash.Length);
			Vector3 size = trashSizes[idx];
			float x = Random.Range(minx + 0.6f * size.x, maxx - 0.6f * size.x);
			float z = Random.Range(minz + 0.6f * size.z, maxz - 0.6f * size.z);
			Instantiate (trash[idx],new Vector3(x, trash[idx].transform.position.y, z), 
			             trash[idx].transform.rotation);
		}
	}

	void computeStepAndSize(GameObject[] objects, Vector3[] sizes, ref float stepx, ref float stepz) {
		for (int i = 0; i < objects.Length; i++) {
			GameObject obj = Instantiate(objects[i], new Vector3(0, -10, 0), new Quaternion()) as GameObject;
			Collider collider = obj.collider;
			Vector3 size = collider.bounds.size;
			sizes[i] = size;
			if (stepx < size.x) {
				stepx = size.x;
			}
			if (stepz < size.z) {
				stepz = size.z;
			}
			Destroy(obj);
		}
	}

	// Update is called once per frame
	void Update () {
	}
}