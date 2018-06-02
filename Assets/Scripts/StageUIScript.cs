using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIScript : MonoBehaviour {
	//Untuk menghitung dan menampilkan notifikasi stage

	private int count;			//menghitung stage
	public GameObject TextHolder;

	// Use this for initialization
	void Start () {
		count = 1;
		TextHolder.GetComponent<Text> ().text = count.ToString ();
		this.GetComponent<Animator>().Play("StageClearAnimation");
	}
		
	public void increaseCount(){
		count++;
		TextHolder.GetComponent<Text> ().text = count.ToString ();
		this.GetComponent<Animator>().Play("StageClearAnimation");
	}
}
