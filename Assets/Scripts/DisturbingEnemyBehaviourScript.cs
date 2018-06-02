using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisturbingEnemyBehaviourScript : MonoBehaviour {
	//untuk kotak amal
	//Kurang lebih sama dengan EnemyBehaviour tetpai berbeda di gerakan

	public float minTime =20f;
	public float maxTime =30f;
	public float health = 150.0f;
	public float enemySpeed = -2f;
	public Image healthBarBG;
	public Image healthBar;
	public GameObject BurstDrop;

	private GameObject Player;
	private GameObject Spawner;
	private int direction = 1;
	private float width = 0.5f;
	private float disturberSpeed = 1f;
	private static float xMin;
	private static float xMax;
	private float maxHealth;
	private int dropIndex;
	private float padding = 0.2f;
	private System.Random rand;
	private float timerCharge = 2f;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("player");
		Spawner = GameObject.Find ("DisturbingEnemySpawner");
		Camera camera = Camera.main;

		//batas gerakkan kiri kanan
		xMin = camera.ViewportToWorldPoint (new Vector3 (0, 0)).x +padding;		
		xMax = camera.ViewportToWorldPoint (new Vector3 (1, 0)).x -padding;

		if (Player == null)
			return;
		healthBarBG.enabled = false;
		healthBar.enabled = false;
		rb = this.GetComponent<Rigidbody2D> ();
	}

	void Update(){
		
		//if the enemy reach certain distance from the player, it will charge
		if (this.transform.position.y <= (Player.transform.position.y + 2.5f)) {
			timerCharge -= Time.deltaTime;
			rb.velocity = Vector3.zero;
			if (timerCharge <= 0) {
				rb.velocity = new Vector3 (0f, -5f,0f);
			}
		} else {
			//for always moving
			float FormationRightEdge = this.transform.position.x + 0.5f * width;
			float FormationLeftEdge = this.transform.position.x - 0.5f * width;

			if (FormationRightEdge > xMax) {
				direction = -1;
			}
			if (FormationLeftEdge < xMin) {
				direction = 1;
			}

			this.transform.position += new Vector3 (direction * disturberSpeed * Time.deltaTime, 0f, 0f);

			rb.velocity = new Vector3 (0, enemySpeed, 0);
		}

		if (health <= 0) {
			BurstDrop.GetComponent<DropBurstScript> ().BurstDrop ();
			Destroy (gameObject);
			Spawner.GetComponent<SweeperSpawnerScript> ().CanSpawn = true;
		}
	}

	void Awake(){
		rand = new System.Random ();
		maxHealth = health;
	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.CompareTag("Bullet")){
			ProjectTile beam = col.gameObject.GetComponent<ProjectTile> ();
			if (beam) {
				health -= beam.GetDamage ();
				healthBarBG.enabled = true;
				healthBar.enabled = true;
				beam.Hit ();
				healthBar.fillAmount =  health/maxHealth;
			}
		}
		if (col.name == "ObjectDestroyer") {		//jika kotak amal hancur, spawner aktif kembali
			Spawner.GetComponent<SweeperSpawnerScript> ().CanSpawn = true;
		}
	}

}
