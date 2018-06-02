using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManagerScript : MonoBehaviour {
	
	public float n_enemySpeed;		//kecepatan enemy normal
	public float n_enemyHealth;
	public float n_enemyBosstSpeed;	//kecepatan enemy saat player mendapat powerUp lupa

	private bool wave = false;		//untuk membantu dalam mengontrol wave
	private float timer;				
	private int roundTime;			//timer dibulatkan
	private EnemySpawner enemySpawner;
	private SweeperSpawnerScript sweeperSpawner;
	private SweeperSpawnerScript disturberSpawner;

	// Use this for initialization
	void Start () {
		enemySpawner = GameObject.Find ("enemyFormation").GetComponent<EnemySpawner>();
		sweeperSpawner = GameObject.Find ("SweeperSpawner").GetComponent<SweeperSpawnerScript>();
		disturberSpawner = GameObject.Find ("DisturbingEnemySpawner").GetComponent<SweeperSpawnerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		roundTime = (int)timer;

		if (roundTime % 30 == 0) {		//setiap 30 detik
			if (wave) {
				//Debug.Log ("wave :" + roundTime / 30);
				enemySpawner.spawnTime -= 0.2f;			//spawnTime enemy berkurang
				sweeperSpawner.minTime -= 1f;			//spawnTime dajjal berkurang
				disturberSpawner.minTime -= 1f;			//spawnTime kotak amal berkurang
				n_enemySpeed -= 0.2f;					//kecepatan enemy bertambah
				n_enemyHealth += 40;					//health enemy bertambah
				enemySpawner.WaveChanged ();			//untuk mengaturulang kecepatan spawn enemy
				wave = false;			
			}
			wave = false;
		} else
			wave = true;
	}
}
