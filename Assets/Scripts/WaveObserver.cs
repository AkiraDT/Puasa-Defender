using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObserver : MonoBehaviour {
	//Untuk mendeteksi apakah musuh dalam satu baris hancur semua atau tidak (Wave)

	public float enemySpeed;

	private GameObject waveSign;		//animasi yang keluar ketika musuh satu baris hancur (coin +2)
	private float initialSpeed;			//kecepatan awal musuh
	private WaveClearUI WaveUI;			//script dari WaveSign
	private PlayerControler Player;		//untuk mendeteksi status player
	private int counter = 5;			//menghitung jumlah eney yg tersisa dalam satu baris
	private ScoreKeeper SK;
	private WaveManagerScript WMS;		//mengatur wave (kecepatan, health, dan spawnRate enemy)

	void Awake () {
		Player = GameObject.Find("player").GetComponent<PlayerControler> ();
		SK = GameObject.Find ("Panel").GetComponent<ScoreKeeper> ();
		WMS = GameObject.Find ("WaveManager").GetComponent<WaveManagerScript> ();
		enemySpeed = WMS.n_enemySpeed;
		initialSpeed = enemySpeed;			//initial speed dijadikan acuan ketika nanti balik dari keadaan powerUp lupa
		WaveUI = GameObject.Find ("WaveClear").GetComponent<WaveClearUI>();
	}
	
	// Update is called once per frame
	void Update () {
		if (counter <= 0) {			//ketika semua enemy hancur
			SK.ScoreCount (2);		//Score +2
			WaveUI.increaseCount ();		//hitungan wave bertambah (karena bisa saja wave terjadi lebih dari satu kali berturut-turut
			Destroy (this.gameObject);		//selfdestruct
			return;
		}

		Rigidbody2D rb = this.GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector3 (0, enemySpeed, 0);			//agar bergerak bersamaan dengan enemy

		if (Player.Boost) {			//ketika player dapat powerUp lupa
			initialSpeed = enemySpeed;			
			enemySpeed = WMS.n_enemyBosstSpeed;		//kecepatan bertambah
		}
		else
			enemySpeed = initialSpeed;		//balik ke kecepatan normal
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.name == "ObjectDestroyer") {
			Destroy(this.gameObject);			//jika sudah melewati batas area game, selfdestruct
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.CompareTag("Enemy")){		//jika musuh hancur, hitung mundur
			counter--;
		}
	}
}
