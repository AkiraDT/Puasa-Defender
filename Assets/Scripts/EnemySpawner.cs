using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	//Untuk spawn Enemy

	public GameObject enemy;
	public GameObject enemyS;			//enemy dengan dropitem powerUp
	public GameObject waveObserver;		//pengecek apakah enemy dalam satu baris hancur semua
	public float spawnTime;

	private int counter = 5;			//untuk menentukan posisi enemyS
	private int spesialPlace;			//for enemyS placing
	private System.Random rand;
	private int maxCount = 7;			// enemy 5 dan 1 waveObserver, 7 tidak dihitung
	private bool canSpawn = true;		

	// Use this for initialization
	void Start () {
		rand = new System.Random();
		Invoke("SpawnEnemies", spawnTime);		//agar trus berulang
	}

	public void WaveChanged(){					//jika ada yg berubah pada enemy panggil ini untuk mereset
		CancelInvoke ("SpawnEnemies");
		if(canSpawn)
			Invoke("SpawnEnemies", spawnTime);
	}

	void SpawnEnemies(){
		counter = 6;
		spesialPlace = rand.Next(2, maxCount);
		foreach (Transform child in this.transform) {
			GameObject spawn;
			if(counter == spesialPlace)		//tempat enemyS
				spawn = Instantiate (enemyS, child.transform.position, Quaternion.identity);
			else if(counter == 1)			//WaveObserver pasti di paling kanan
				spawn = Instantiate (waveObserver, child.transform.position, Quaternion.identity);
			else
				spawn = Instantiate (enemy, child.transform.position, Quaternion.identity);
			spawn.transform.position = child.transform.position;
			counter--;
		}
		Invoke("SpawnEnemies", spawnTime);		//agar berulang (nested loop)
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
