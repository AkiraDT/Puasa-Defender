using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTombolScript : MonoBehaviour {
	//Untuk menyembunyikan tombol Cerita di awal permainan

	public GameObject CeritaButton;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("IsFirstPlayed") != 1) {		//jika pertama install, sembunyikan tombol cerita
			CeritaButton.SetActive (false);
		} else {
			CeritaButton.SetActive (true);
		}
	}
}
