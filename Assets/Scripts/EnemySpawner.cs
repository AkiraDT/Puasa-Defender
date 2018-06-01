﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;
	public GameObject enemyS;	//Unique enemy
	public GameObject waveObserver;
	public float spawnTime;

	private int counter = 5;
	private int spesialPlace;	//for enemyS placing
	private System.Random rand;
	private int maxCount = 7;
	private float width = 9.5f;
	private float height = 7.0f;
	private bool canSpawn = true;

	// Use this for initialization
	void Start () {
		rand = new System.Random();
		//InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
		Invoke("SpawnEnemies", spawnTime);
	}

	public void WaveChanged(){
		CancelInvoke ("SpawnEnemies");
		if(canSpawn)
			Invoke("SpawnEnemies", spawnTime);
	}

	void SpawnEnemies(){
		counter = 6;
		spesialPlace = rand.Next(2, maxCount);
		foreach (Transform child in this.transform) {
			GameObject spawn;
			if(counter == spesialPlace)
				spawn = Instantiate (enemyS, child.transform.position, Quaternion.identity);// as GameObject;
			else if(counter == 1)
				spawn = Instantiate (waveObserver, child.transform.position, Quaternion.identity);// as GameObject;
			else
				spawn = Instantiate (enemy, child.transform.position, Quaternion.identity);// as GameObject;
			spawn.transform.position = child.transform.position;
			counter--;
		}
		Invoke("SpawnEnemies", spawnTime);
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width,height));
	}

	public bool CanSpawn{
		get{ 
			return canSpawn;
		}
		set{
			canSpawn = value;
		}
	}
		
}