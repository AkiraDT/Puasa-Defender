using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shreder : MonoBehaviour {
	//Untuk menghancurkan objek yang melewati batas

	public string[] m_tag;		//tag-tag objek yang ingin dihancurkan

	void OnTriggerEnter2D(Collider2D other){
		foreach (string value in m_tag) {
			if (other.CompareTag (value)) {
				Destroy (other.gameObject);
				return;
			}
		}

	}
}
