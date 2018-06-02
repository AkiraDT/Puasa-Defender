using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour {
	//Attach ke boss


	public Image healthBarBG;
	public Image healthBar;
	public float bossSpeed = -2f;		//kecepatan boss
	public float fireSpeed = 2;			//kecepatan peluru boss
	public bool canShot;				//apakah boss bisa menembak atau tidak
	public GameObject[] Bullet;
	public GameObject burstDrop;		//DropItem banyak

	private float health;
	private Animator anim;
	private float maxHealth;
	private float initialSpeed;
	private System.Random rand;
	private float timer = 6;
	private int counter = 0;			//sebagai parameter pola tembakan

	// Use this for initialization
	void Start () {
		healthBarBG.enabled = false;
		healthBar.enabled = false;
		initialSpeed = bossSpeed;
		anim = this.transform.GetComponentInParent<Animator> ();
		anim.Play ("BossEnterAnimation");		//boss pertama datang
	}

	void Awake(){
		health = PlayerPrefs.GetFloat ("BossHealth");		//health akan bertambah seiring dengan stage
		rand = new System.Random ();
		maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		if(canShot){
			timer -= Time.deltaTime;		//setiap 6 detik
			if (timer <= 0) {
				Fire ();
				timer = 6;
			}
		}
	}

	void Fire(){
		GameObject beam;
		if (counter % 5 == 0) {		//pola peluru 1
			beam = Instantiate (Bullet [1], this.transform.position, Quaternion.identity);
		} else {					//pola peluru 2
			beam = Instantiate (Bullet[0], this.transform.position, Quaternion.identity);
		}
		Rigidbody2D RenderBuffer = beam.GetComponent<Rigidbody2D>();
		RenderBuffer.velocity = new Vector3 (0, -fireSpeed,0);
		counter++;
	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.CompareTag("Bullet")){
			ProjectTile beam = col.gameObject.GetComponent<ProjectTile> ();
			if (beam) {
				health -= beam.GetDamage ();		//health berkurang
				healthBarBG.enabled = true;
				healthBar.enabled = true;
				beam.Hit ();
				healthBar.fillAmount =  health/maxHealth;
			}
		}
	}

	public float Health{
		get{ 
			return health;
		}
	}

	public void BurstDrop(){		//dipanggil di spawner
		burstDrop.GetComponent<DropBurstScript> ().BurstDrop ();
	}
}
