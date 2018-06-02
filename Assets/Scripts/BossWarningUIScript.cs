using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarningUIScript : MonoBehaviour {
	//Untuk warning boss

	public void PlayAnimationWarning(){
		this.GetComponent<Animator>().Play("BossWarningAnimation");
	}

}
