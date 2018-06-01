using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour {
	
	public Image healthBarBG;
	public Image healthBar;
	public float bossSpeed = -2f;
	public float fireSpeed = 2;
	public bool canShot;
	public GameObject[] Bullet;
	public GameObject burstDrop;

	private float health;
	private Animator anim;
	private PlayerControler Player;
	private float maxHealth;
	private float prob;
	private float initialSpeed;
	private System.Random rand;
	private int dropIndex;
	private float timer = 6;
	private int counter = 0;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("player").GetComponent<PlayerControler> ();
		if (Player == null)
			return;
		healthBarBG.enabled = false;
		healthBar.enabled = false;
		initialSpeed = bossSpeed;
		anim = this.transform.GetComponentInParent<Animator> ();
		anim.Play ("BossEnterAnimation");
	}

	void Awake(){
		health = PlayerPrefs.GetFloat ("BossHealth");
		rand = new System.Random ();
		maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {

		if(canShot){
			timer -= Time.deltaTime;
			if (timer <= 0) {
				Fire ();
				timer = 6;
			}
		}
	}

	void Fire(){
		GameObject beam;
		if (counter % 5 == 0) {
			beam = Instantiate (Bullet [1], this.transform.position, Quaternion.identity);
		} else {
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
				health -= beam.GetDamage ();
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

	public void BurstDrop(){
		burstDrop.GetComponent<DropBurstScript> ().BurstDrop ();
	}
}
