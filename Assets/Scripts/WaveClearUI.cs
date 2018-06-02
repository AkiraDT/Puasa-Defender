using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveClearUI : MonoBehaviour {
	//untuk mengatur notifikasi wave bonus

	private int count;				//menampung jumlah wave
	private float delay = 0.5F;		//delay per animasinya


	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (count > 0) {
			delay -= Time.deltaTime;
			this.GetComponent<Animator>().Play("WaveClearAnimation");

			if (delay <= 0 && count > 1) {		//agar ada jeda tiap animasi
				delay = 0.5f;
				count--;
				this.GetComponent<Animator>().Play("New State");
			}

		}
	}
		
	public void increaseCount(){
		count++;
	}
}
