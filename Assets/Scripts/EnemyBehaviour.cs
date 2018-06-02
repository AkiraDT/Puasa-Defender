using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {
	//attach ke enemy atau enemyS

	public GameObject Laser;			//bullet
	public float fireSpeed = 10.0f;
	public float health = 150.0f;
	public float enemySpeed;
	public GameObject[] dropItem;
	public Image healthBarBG;
	public Image healthBar;

	private PlayerControler Player;
	private float maxHealth;
	private System.Random rand;
	private int dropIndex;
	private float initialSpeed;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("player").GetComponent<PlayerControler> ();
		if (Player == null)
			return;
		healthBarBG.enabled = false;
		healthBar.enabled = false;
		enemySpeed = GameObject.Find ("WaveManager").GetComponent<WaveManagerScript> ().n_enemySpeed;
		initialSpeed = enemySpeed;
		health += GameObject.Find ("WaveManager").GetComponent<WaveManagerScript> ().n_enemyHealth;
		maxHealth = health;
	}

	void Awake(){
		rand = new System.Random ();
	}
	
	// Update is called once per frame
	void Update () {
		//for enemy movement
		Rigidbody2D rb = this.GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector3 (0, enemySpeed, 0);

		//jika health habis
		if (health <= 0) {		
			Destroy (gameObject);

			//mengacak dropItem
			if (dropItem.Length > 1)
				dropIndex = rand.Next (dropItem.Length);
			
			Instantiate (dropItem[dropIndex], this.transform.position, this.transform.rotation);	//dropitem
		}

		//Jika player dapat powerUp lupa
		if (Player.Boost) {
			initialSpeed = enemySpeed;
			enemySpeed = -30f;		//enemy jadi cepat
		}
		else
			enemySpeed = initialSpeed;		//kecepatan normal

		//dan jika player baru selesai dari efek lupa semua musuh langsung hancur
		if (Player.BoostTime <= 0.5)
			health = 0;
	}

	void Fire(){
		GameObject beam = Instantiate (Laser, this.transform.position, Quaternion.identity);
		Rigidbody2D RenderBuffer = beam.GetComponent<Rigidbody2D>();
		RenderBuffer.velocity = new Vector3 (0, -fireSpeed,0);
	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.CompareTag("Bullet")){
			ProjectTile beam = col.gameObject.GetComponent<ProjectTile> ();
			if (beam) {
				health -= beam.GetDamage ();		//darah berkurang
				healthBarBG.enabled = true;
				healthBar.enabled = true;
				beam.Hit ();
				healthBar.fillAmount =  health/maxHealth;
			}
		}
		//jika menabrak player yang sedang dalam keadaan powerUp lupa
		if(col.CompareTag("Player")){
			if (Player.Boost) {
				health -= health;	//enemy hancur
			}
		}
	}
}
