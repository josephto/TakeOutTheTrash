﻿using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Die() {
		Destroy(gameObject);
	}
}