using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	public AudioClip menuAudio;
	public AudioClip gameAudio;
	public AudioClip bossAudio;

	private AudioSource audioSource;
	public string[] sceneName;		//Nama Scene untuk dijadikan referensi music mana yang akan diputar
	private bool[] changeMusicHelper = new bool[3];		//agar music hanya ada 1 yang aktif (tidak overlap)
	private BossSpawner BS;

	
	void Start () {
		audioSource = GetComponent<AudioSource> ();

		for(int i=0; i<3; i++){
			changeMusicHelper [i] = true;
		}
			

		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	void Update(){

		if ((SceneManager.GetActiveScene ().name == sceneName [0] || SceneManager.GetActiveScene ().name == sceneName [2]) 
			&& changeMusicHelper[0]) {
			audioSource.clip = menuAudio;
			audioSource.Play ();
			changeMusicHelper [0] = false;
			changeMusicHelper [1] = true;
			changeMusicHelper [2] = true;
		} else if (SceneManager.GetActiveScene ().name == sceneName [1] && changeMusicHelper[1] && changeMusicHelper[2]) {
			audioSource.clip = gameAudio;
			audioSource.Play ();
			changeMusicHelper [0] = true;
			changeMusicHelper [1] = false;
			changeMusicHelper [2] = true;
		}

		if(SceneManager.GetActiveScene ().name == sceneName [1]){
			BS = GameObject.Find("BossSpawner").GetComponent<BossSpawner>();
			if (BS != null) {
				if (BS.IsBossAlreadySpawned && changeMusicHelper[2] ) {
					audioSource.clip = bossAudio;
					audioSource.Play ();
					changeMusicHelper [0] = true;
					changeMusicHelper [1] = true;
					changeMusicHelper [2] = false;
				} else if(!BS.IsBossAlreadySpawned && changeMusicHelper[1]){
					audioSource.clip = gameAudio;
					audioSource.Play ();
					changeMusicHelper [0] = true;
					changeMusicHelper [1] = false;
					changeMusicHelper [2] = true;
				}
			}
		}
	}

	public void ToggleSound(){
		if (PlayerPrefs.GetInt ("Muted") == 0) {
			PlayerPrefs.SetInt ("Muted", 1);
			//dijadiin mute
		}else{
			PlayerPrefs.SetInt ("Muted", 0);
			//unmute
		}
	}
}
