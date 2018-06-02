using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	//Untuk berpindah scene

	public void LoadLevel(string name){
		//Jika pertama main, ketika mulai akan masuk ke scene cerita dulu
		if(name.Contains("GameScene")){
			if(PlayerPrefs.GetInt("IsFirstPlayed") != 1){
				name = "StoryScene";
			}
		}

		SceneManager.LoadScene (name);	//Berpindah Scene
	}

	public void QuitRequest(){
		Application.Quit ();	//untuk keluar Aplikasi
	}

}
