using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperSpawnerScript : MonoBehaviour {
	//untuk spawn kotak amal dan dajjal

	public int id = 0;		//id 0 untuk dajjal dan 1 untuk kotak amal
	public GameObject sweeper;
	public float minTime =20f;		//waktu minimal	spawn
	public float maxTime =30f;		//waktu maksimal spawn
	public static float xMin;
	public static float xMax;

	private float time;
	private float spawnTime;
	private bool canSpawn;
	private GameObject Player;
	private int direction = 1;
	private float width = 0.5f;
	private float spawnerSpeed = 4f;		//spawner bergerak kekiri kekanan

	float padding = 0.2f;

	// Use this for initialization
	void Start () {
		time = 0f;
		Player = GameObject.Find ("player");
		setRandomTime ();
		canSpawn = true;
		Camera camera = Camera.main;

		xMin = camera.ViewportToWorldPoint (new Vector3 (0, 0)).x +padding;		//batas kiri gerakan
		xMax = camera.ViewportToWorldPoint (new Vector3 (1, 0)).x -padding;		//batas kanan gerakan
	}

	void SpawnSweeper(){
		Instantiate (sweeper, this.transform.position, Quaternion.identity);// as GameObject;
		if(id == 1){		//jika kotak amal, hanya spawn sekali sampai kotak amal yang ada hancur
			canSpawn = false;
		}
	}

	void OnDrawGizmos(){		//untuk mempermudah di editor
		Gizmos.DrawWireSphere (this.transform.position, 0.5f);
	}

	void Update(){
		if (canSpawn) {
			time += Time.deltaTime;

			if (time >= spawnTime && !Player.GetComponent<PlayerControler> ().Boost) {		//jika waktu spawn tercapai dan player tidak dalam keadaan powerUplupa
				SpawnSweeper ();
				setRandomTime ();
			}

					//spawner terus bergerak kekiri kekanan
			float FormationRightEdge = this.transform.position.x + 0.5f * width;
			float FormationLeftEdge = this.transform.position.x - 0.5f * width;

			if (FormationRightEdge > xMax) {
				direction = -1;
			}
			if (FormationLeftEdge < xMin) {
				direction = 1;
			}

			this.transform.position += new Vector3 (direction * spawnerSpeed * Time.deltaTime, 0f, 0f);
		}
	}

	void setRandomTime(){
		time = 0;
		spawnTime = Random.Range (minTime, maxTime);
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
