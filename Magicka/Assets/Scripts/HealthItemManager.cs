using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthItemManager : MonoBehaviour {

	public Transform[] spPoints;
	public GameObject generator;

	float spawnTimer = 0.0f;
	float maxWaitSpawn = 30.0f;

	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;

		if (spawnTimer >= maxWaitSpawn) {
			Spawn ();
			spawnTimer = 0.0f;
		}
	}

	void Spawn() {
		int r = Random.Range (0, spPoints.Length);
		Instantiate(generator, spPoints[r].position, Quaternion.identity);
	}
}
