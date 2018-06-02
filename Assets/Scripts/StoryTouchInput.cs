using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoryTouchInput : MonoBehaviour, IPointerClickHandler {
	//Touch ketika di scene cerita

	public Sprite[] DialogList;			//list dialog
	public GameObject DialogHolder;
	public GameObject ButtonMulai;
	public GameObject ButtonKembali;

	private int counter;
	private bool limit = false;			//batas sentuhan

	void Start () {
		ButtonMulai.SetActive (false);
		ButtonKembali.SetActive (false);

		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		DialogHolder.GetComponent<Image> ().sprite= DialogList [counter];
	}


	public virtual void OnPointerClick(PointerEventData ped){
		if (counter < DialogList.Length - 1) {
			counter++;
		} else {
			if (!limit) {
				if (PlayerPrefs.GetInt ("IsFirstPlayed") == 1) {
					ButtonKembali.SetActive (true);
				} else {
					PlayerPrefs.SetInt ("IsFirstPlayed", 1);		//jika pertama kali main
					ButtonMulai.SetActive (true);
				}
				limit = true;
			}
		}
	}
}
