using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {
	public GameObject boss;
	public float spawnTime = 30f;
	public float bossHealth;
	public GameObject StageClearHolder;
	public GameObject BossWarningHolder;

	private System.Random rand;
	private bool isBossAlreadySpawned;
	private GameObject SpawnedBoss;
	private GameObject enemyExist;
	private GameObject Player;
	private EnemySpawner enemySpawner;
	private SweeperSpawnerScript sweeperSpawner;
	private SweeperSpawnerScript disturberSpawner;
	private int direction = 1;
	private StageUIScript StageClearUI;
	private BossWarningUIScript BossWarningUI;

	private int spawnCounter;
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
		PlayerPrefs.SetFloat ("BossHealth", bossHealth);
	}

	void SpawnBoss(){
		foreach (Transform child in this.transform) {
			SpawnedBoss = Instantiate (boss, this.transform.position, Quaternion.identity);
			SpawnedBoss.transform.parent = child.transform;
		}
		bossHealth += 2000;
		PlayerPrefs.SetFloat ("BossHealth", bossHealth);
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, new Vector3(9.5f, 7.0f));
	}

	void Update(){
		if(!isBossAlreadySpawned)
			spawnTime -= Time.deltaTime;

		if (spawnTime <= 0 && !Player.GetComponent<PlayerControler>().Boost && !isBossAlreadySpawned) {
			enemyExist = GameObject.Find ("WaveObserver");
			BossWarningUI.PlayAnimationWarning ();
			if (enemyExist == null) {
				SpawnBoss ();
				spawnTime = 30;
				isBossAlreadySpawned = true;
			}
			enemySpawner.CanSpawn = false;
			sweeperSpawner.CanSpawn = false;
			disturberSpawner.CanSpawn =  false;
			enemySpawner.WaveChanged ();
		}

		if (isBossAlreadySpawned) {
			if (SpawnedBoss.transform.GetChild(0).GetComponent<BossBehaviour> ().Health <= 0) {
				Debug.Log ("ohyeah");
				SpawnedBoss.transform.GetChild (0).GetComponent<BossBehaviour> ().BurstDrop ();
				Destroy (SpawnedBoss.gameObject);
				enemySpawner.CanSpawn = true;
				sweeperSpawner.CanSpawn = true;
				disturberSpawner.CanSpawn = true;
				isBossAlreadySpawned = false;
				enemySpawner.WaveChanged ();
				StageClearUI.increaseCount ();
			}
		}
	}

	public bool IsBossAlreadySpawned{
		get{ 
			return isBossAlreadySpawned;
		}
	}
}
