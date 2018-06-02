using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DragonBones;

public class PlayerControler : MonoBehaviour {
	public GameObject[] Bullet;			//Untuk menyimpan sprite peluru/sendok dari level 1-max (karena array jadi dari 0)
	public Sprite normalSprite;			//Untuk menyimpan sprite ketika player dalam keadaan normal
	public Sprite boostSprite;			//untuk menyimpan Sprite ketika dapat powerUp Lupa
	public float bulletSpeed = 15.0f;	//Untuk mengatur kecepatan peluru meluncur
	public float fireRate;				//Untuk menentukan fireRate/seberapa cepat peluru keluar perdetiknya (makin kecil angka makin cepat)
	public string[] LoseTagList;		//Tag apa saja yang membuat player kalah
	public GameObject Holo;				//Untuk efek ketika mendapat powerUp magnet. sebenarnya bisa memakai sprite, cuma ini biar cepat
	public GameObject Flame;			//Untuk efek ketika mendapat powerUp lupa

	private float nextFire;			//Untuk digunakan bersama fireRate
	EnemySpawner ES;				//Reference EnemySpawner script

	//untuk status powerUp
	private bool doubleBullet= false; // id 0
	//untuk level Up menggunakan		 id 1
	private bool boost 		= false;  // id 2
	private bool magnet 	= false;  // id 3

	//Untuk durasi powerUp
	private float doubleBulletTime = 10;
	private float boostTime = 3;
	private float magnetTime = 10;

	private ScoreKeeper SK;				//Reference ke score/coin/pahala
	private int levelIndex = 0;			//untuk mengakses level atau bullet yang akan digunakan
	private float offSetDouble = 0.15f;	//untuk jarak ketika double PowerUp

	/*===Digunakan ketika memakai asset dari DragonBone===*/
	/*
	private UnityArmatureComponent armature;		
	private string idleAnimation = "idle";
	private string boostInAnimation = "first_boost";
	private string boostAnimation = "boost_idle";
	private string boostOutAnimation = "last_boost";

	*code yg bersangkutan akan diberi tanda DB*
	*/

	float normalSpawnTime;			//waktu  normal spawn musuh
	float boostSpawnTime = 0.3f;	//waktu spawn ketika dalam powerUp lupa / boost
	float timerGameStart = 3f;		//waktu jeda sebelum permainan dimulai
	bool isPlaying = false;			//status game mulai atau belum


	// Use this for initialization
	void Start () {
		ES = GameObject.Find("enemyFormation").GetComponent<EnemySpawner> ();
		SK = GameObject.Find ("Panel").GetComponent<ScoreKeeper> ();
		levelIndex = PlayerPrefs.GetInt ("baseLevel");		//mengambil base level dari PlayerPrefs
															//baseLevel (akan lebih berguna jika ingin ada purchase/upgrade level awal)
		normalSpawnTime = ES.spawnTime;
		Holo.SetActive (false);
		this.GetComponent<SpriteRenderer> ().sprite = normalSprite;

		//armature = this.GetComponent<UnityArmatureComponent> ();	//DB
		//armature.animation.FadeIn (boostAnimation, -1f, -1);	//DB
	}

	//==================FIELD================
	public bool Boost{
		get{ 
			return boost; 
		}
	}

	public float BoostTime{
		get{
			return boostTime;
		}
	}

	public bool Magnet{
		get{
			return magnet;
		}
	}
	//=======================================


	// Update is called once per frame
	void Update () {
		
		//jeda sebelum permainan berjalan
		if (!isPlaying) {		
			timerGameStart -= Time.deltaTime;
		}

		//ketika waktu jeda sudah terlewat, game dimulai
		if(timerGameStart <= 0 && !isPlaying){		
			isPlaying = true;
			this.GetComponent<SpriteRenderer> ().sprite = normalSprite;

			//armature.animation.FadeIn (idleAnimation, 0.25f, -1);		//DB
			//armature.animation.timeScale = 1;		//DB

			Flame.SetActive (false);
		}


		//auto fire
		if(isPlaying && !boost )		//player hanya bisa menembak ketika permainan dimulai dan tidak dalam keadaan PowerUp lupa
			Fire();

		//=====================================================
		//hitung mundur waktu powerUp double bullet
		if (doubleBullet) {		
			doubleBulletTime -= Time.deltaTime;
			if (doubleBulletTime <= 0) {
				doubleBullet = false;
				doubleBulletTime = 10;
			}
		}
		//=====================================================
		//hitung mundur waktu PowerUp lupa
		if (boost) {		
			boostTime -= Time.deltaTime;
			//this.GetComponent<SpriteRenderer> ().sprite = boostSprite;
			if (boostTime <= 0) {
				boost = false;
				boostTime = 3;
				ES.spawnTime = normalSpawnTime;		//set waktu spawn musuh menjadi normal
				ES.WaveChanged ();		//untuk mengatur ulang waktu spawnnya
				this.GetComponent<SpriteRenderer> ().sprite = normalSprite;		//sprite menjadi sprite normal

				//armature.animation.FadeIn(idleAnimation, 0.25f, -1);		//DB
				Flame.SetActive (false);	//mematikan efek lupa
			}
		}
		//=====================================================
		//hitung mundur powerUp magnet
		if(magnet){
			magnetTime -= Time.deltaTime;
			if(magnetTime<= 0){
				magnetTime = 10;
				magnet = false;
				Holo.SetActive (false);		//efek magnet dinonaktifkan
			}
		}
			
	}

	void Fire(){
		if (Time.time > nextFire) {
			nextFire = Time.time +fireRate;
			GameObject beam;	//peluru1
			GameObject beam2;	//peluru2 (jika powerUp doubleBullet aktif)

			//posisi peluru normal
			Vector2 normalPos = new Vector2 (this.transform.position.x, this.transform.position.y+1.2f);

			//for double bullet
			Vector2 leftSide = new Vector2(this.transform.position.x+offSetDouble, this.transform.position.y+1.2f);		//posisi peluru kiri
			Vector2 rightSide = new Vector2(this.transform.position.x-offSetDouble, this.transform.position.y+1.2f);	//posisi peluru kanan

			//Jika dapat double PowerUp
			if (doubleBullet) {
				beam = Instantiate (Bullet [levelIndex], leftSide, Quaternion.identity);
				beam2 = Instantiate (Bullet [levelIndex], rightSide, Quaternion.identity);

				Rigidbody2D rb2 = beam2.GetComponent<Rigidbody2D> ();		
				rb2.velocity = new Vector3 (0, bulletSpeed, 0);		//untuk meluncurkan peluru2
			} 
			//keadaan normal
			else {
				beam = Instantiate (Bullet [levelIndex], normalPos, Quaternion.identity);
			}

			Rigidbody2D rb = beam.GetComponent<Rigidbody2D> ();
			rb.velocity = new Vector3 (0, bulletSpeed, 0);		//untuk meluncurkan peluru1
		}
	}


	void OnTriggerEnter2D (Collider2D col){
		/*==jika tidak dalam keadaan lupa, player akan kalah ketika
		 	menabrak objek dengan tag tertentu==*/
		if (!boost) {		
			foreach (string value in LoseTagList) {
				if (col.CompareTag (value)) {
					Destroy (gameObject);		//Menghancurkan player
					SK.StoreHighScore (SK.getScore ());		//membandingkan score sekarang dengan highscore dan menyimpan score jika score > highScore
					SceneManager.LoadScene ("ScoreScene");		//ketika kalah akan dialihan ke scene score
					return;
				}
			}
		}

		//==================================================
		//jika dropItemnya coin / diamond
		if(col.CompareTag("Coin")){		
			int value = 0;
			switch(col.GetComponent<DropItemScript>().id){
			case 0:		//jika coin biasa
				value = 1;
				break;
			case 1:		//jika diamond
				value = 10;
				break;
			}
			Destroy (col.gameObject);		//hancurkan coin
			SK.ScoreCount (value);			//simpan ke score
		}

		//===================================================
		//jika dropItemnya powerUp
		if (col.CompareTag ("PowerUp")) {		
			Destroy (col.gameObject);		//hancurkan itemnya

			//-----------------------------
			//jika powerUp double bullet
			if (col.GetComponent<DropItemScript> ().id == 0){	
				doubleBulletTime = 10;
				doubleBullet = true;
			}
			//-----------------------------
			//jika powerUp LevelUp
			else if(col.GetComponent<DropItemScript> ().id == 1){	
				if(levelIndex < Bullet.Length-1)
					levelIndex++;
				if (levelIndex < 5 || levelIndex > 5 && levelIndex < 14){
					if (levelIndex == 11)
						offSetDouble += 0.3f;
					else
						offSetDouble += 0.15f;
					}
				if (levelIndex == 5 || levelIndex == 10) {
					offSetDouble = 0.15f;
				}
			}
			//-----------------------------
			//jika powerUp Lupa
			else if(col.GetComponent<DropItemScript> ().id == 2){
				//armature.animation.FadeIn(boostAnimation, 0.25f, -1);		//DB

				ES.spawnTime = boostSpawnTime;		//buat musuh agar spawn lebih cepat
				boost = true;
				ES.WaveChanged ();			//untuk mengatur spawn musuh
				Flame.SetActive (true);		//menyalakan efek lupa
			}
			//-----------------------------
			//Jika powerUp magnet
			else if(col.GetComponent<DropItemScript> ().id == 3){
				magnetTime = 10;
				magnet = true;
				Holo.SetActive (true);		//mengaktifkan efek magnet
			}
		}
	}

}
