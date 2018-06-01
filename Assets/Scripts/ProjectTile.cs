using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour {
	//Untuk semua object peluru
	// Use this for initialization
	public float Damage = 100f;		//jumlah damage yang diberikan

	public float GetDamage(){		//untuk memberikan damage(dipanggil di objek sasaran)
		return Damage;
	}

	public void Hit(){
		Destroy (gameObject);
	}
}