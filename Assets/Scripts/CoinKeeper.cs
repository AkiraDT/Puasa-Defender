using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinKeeper : MonoBehaviour {
	//Untuk memperlihatkan jumlah coin yang terkumpul

	// Use this for initialization
	void Start () {
		this.GetComponent<Text> ().text = PlayerPrefs.GetInt ("coin").ToString();
	}
}
