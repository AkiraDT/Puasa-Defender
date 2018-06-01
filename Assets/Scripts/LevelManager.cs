using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
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
