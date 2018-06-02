using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
	//untuk membantu Display di editor

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (this.transform.position, 0.5f);
	}
}
