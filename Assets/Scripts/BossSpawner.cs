using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {
	//Untuk spawn Boss

	public GameObject boss;
	public float spawnTime = 30f;		//waktu spawn
	public float bossHealth;
	public GameObject StageClearHolder;
	public GameObject BossWarningHolder;

	private System.Random rand;
	private bool isBossAlreadySpawned;	//apakah boss sudah ada?
	private GameObject SpawnedBoss;		//Objek untuk menampung boss
	private GameObject enemyExist;
	private GameObject Player;
	private EnemySpawner enemySpawner;
	private SweeperSpawnerScript sweeperSpawner;
	private SweeperSpawnerScript disturberSpawner;
	private StageUIScript StageClearUI;
	private BossWarningUIScript BossWarningUI;
	private int spawnCounter;			//menghitung sudah berapa kali boss keluar

	// Use this for initialization
	void Start () {
		rand = new System.Random();
		Player = GameObject.Find ("player");
		enemySpawner = GameObject.Find ("enemyFormation").GetComponent<EnemySpawner>();
		sweeperSpawner = GameObject.Find ("SweeperSpawner").GetComponent<SweeperSpawnerScript>();
		disturberSpawner = GameObject.Find ("DisturbingEnemySpawner").GetComponent<SweeperSpawnerScript>();
		isBossAlreadySpawned = false;
		StageClearUI = StageClearHolder.GetComponent<StageUIScript> ();
		BossWarningUI = BossWarningHolder.GetComponent<BossWarningUIScript> ();
		PlayerPrefs.SetFloat ("BossHealth", bossHealth);			//pertama kali mulai, health boss awal
	}

	void SpawnBoss(){
		foreach (Transform child in this.transform) {
			SpawnedBoss = Instantiate (boss, this.transform.position, Quaternion.identity);
			SpawnedBoss.transform.parent = child.transform;
		}
		//tambah nyawa boss setiap kali spawn
		bossHealth += 2000;	
		PlayerPrefs.SetFloat ("BossHealth", bossHealth);		//simpan di playerPrefs
	}

	//untuk membantu di editor
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, new Vector3(9.5f, 7.0f));
	}

	void Update(){
		if(!isBossAlreadySpawned)
			spawnTime -= Time.deltaTime;

		if (spawnTime <= 0 && !Player.GetComponent<PlayerControler>().Boost && !isBossAlreadySpawned) {
			enemyExist = GameObject.Find ("WaveObserver");
			BossWarningUI.PlayAnimationWarning ();		//notifikasi boss datang
			//jika masih ada musuh tidak bisa spawn
			if (enemyExist == null) {		
				SpawnBoss ();
				spawnTime = 30;
				isBossAlreadySpawned = true;
			}
			//Enemy, kotak amal, dan dajjal tidak di spawn lagi setelah boss terspawn
			enemySpawner.CanSpawn = false;
			sweeperSpawner.CanSpawn = false;
			disturberSpawner.CanSpawn =  false;
			enemySpawner.WaveChanged ();
		}

		if (isBossAlreadySpawned) {
			//ketika health boss habis
			if (SpawnedBoss.transform.GetChild(0).GetComponent<BossBehaviour> ().Health <= 0) {
				SpawnedBoss.transform.GetChild (0).GetComponent<BossBehaviour> ().BurstDrop ();	//dropItem
				Destroy (SpawnedBoss.gameObject);	//hancurkan boss

				//Semua spawner kembali aktif
				enemySpawner.CanSpawn = true;
				sweeperSpawner.CanSpawn = true;
				disturberSpawner.CanSpawn = true;
				isBossAlreadySpawned = false;
				enemySpawner.WaveChanged ();
				StageClearUI.increaseCount ();	//naik stage
			}
		}
	}

	public bool IsBossAlreadySpawned{
		get{ 
			return isBossAlreadySpawned;
		}
	}
}
