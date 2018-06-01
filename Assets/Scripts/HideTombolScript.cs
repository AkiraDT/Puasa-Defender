using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTombolScript : MonoBehaviour {
	public GameObject CeritaButton;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("IsFirstPlayed") != 1) {
			CeritaButton.SetActive (false);
		} else {
			CeritaButton.SetActive (true);
		}
	}
}
