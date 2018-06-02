using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemScript : MonoBehaviour {
	//Untuk semua DropItem

	private float dropItemSpeed = -5f;	//kecepatan DropItem jatuh
	public int id = 0;					//id untuk dropItem
	private GameObject Player;			//untuk efek magnet harus mencari posisi player

	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("player");
		rb = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {		
		rb.velocity = new Vector3 (0, dropItemSpeed, 0);		//untuk menggerakkan dropItem 

		if (Player != null) {
			if (Player.GetComponent<PlayerControler> ().Magnet) {		//jika magnet aktif, drop item mendekati player
				this.transform.position = Vector2.MoveTowards (this.transform.position, Player.transform.position, 2f * Time.deltaTime);
			}
		}

	}
		
		
}
