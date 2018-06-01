using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBurstScript : MonoBehaviour {
	public GameObject[] itemList;

	private int itemIndex;	//for powerUp placing
	private System.Random rand;
	private int maxCount = 12;

	// Use this for initialization
	void Start () {
		rand = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BurstDrop(){
		foreach (Transform child in this.transform) {
			itemIndex = rand.Next(0, maxCount);
			GameObject spawn;
			if (itemIndex == 6 || itemIndex == 7 || itemIndex == 8 || itemIndex == 9 || itemIndex == 10 || itemIndex == 11) {
				spawn = Instantiate (itemList [0], child.transform.position, Quaternion.identity);
			} else {
				spawn = Instantiate (itemList[itemIndex], child.transform.position, Quaternion.identity);
			}
			spawn.transform.position = child.transform.position;
		}
	}
}
