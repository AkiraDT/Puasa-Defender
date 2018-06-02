using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	//Untuk menyimpan score

	public static int score = 0;

	public GameObject mytext;
	public GameObject highScoreText;

	public int coin{get{ return PlayerPrefs.GetInt ("coin");}}

	// Use this for initialization
	void Start () {
		Reset ();
		mytext.GetComponent<Text>().text = score.ToString ();
		highScoreText.GetComponent<Text> ().text = PlayerPrefs.GetInt ("highScore").ToString();
	}

	public void ScoreCount(int points){		//menghitung score
		score += points;
		mytext.GetComponent<Text>().text = score.ToString ();
		if (score > PlayerPrefs.GetInt ("highScore"))		//jika mencapai highscore baru
			HighScoreReached ();
	}

	public int getScore(){
		return score;
	}


	public static void Reset(){
		score = 0;
	}

	public void HighScoreReached(){			//jika highscore baru tercapai, teks berubah menjadi "HIGHSCORE"
		highScoreText.GetComponent<Text>().text = "HIGHSCORE";
	}


	public void StoreHighScore(int newHighScore){		//Menyimpan highscore ke PlayerPrefs
		int oldHighScore = PlayerPrefs.GetInt ("highScore", 0);
		if (newHighScore > oldHighScore) {
			PlayerPrefs.SetInt ("highScore", newHighScore);
			highScoreText.GetComponent<Text> ().text = newHighScore.ToString();
		}

		int oldCoin = PlayerPrefs.GetInt ("coin");
		PlayerPrefs.SetInt ("coin", oldCoin + newHighScore);
	}
}
