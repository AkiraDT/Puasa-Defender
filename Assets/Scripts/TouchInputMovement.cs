using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchInputMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	//Untuk mengontrol player

	GameObject Player;
	float offDistance;			//jarak antara posisi sentuhan dan player
	float startPos;
	private Camera m_camera;

	private Animator anim;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("player");
		m_camera = Camera.main;
		anim = Player.GetComponent<Animator> ();
	}

	public virtual void OnPointerDown(PointerEventData ped){		//ketika menyentuh layar
		offDistance = Player.transform.position.x - m_camera.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Player.transform.position.y, Player.transform.position.z)).x ;
	}

	public virtual void OnDrag(PointerEventData ped){			//ketika menggeser layar
		if(Player == null)
			return;
		Vector3 pos = new Vector3 (Input.mousePosition.x, Player.transform.position.y, Player.transform.position.z);

		Player.transform.position = new Vector2 (Mathf.Clamp(offDistance + m_camera.ScreenToWorldPoint(pos).x, m_camera.ViewportToWorldPoint (new Vector3 (0, 0)).x +0.48f, m_camera.ViewportToWorldPoint (new Vector3 (1, 0)).x -0.48f), Player.transform.position.y);
		anim.SetBool ("isMoving", true);		//animasi berjalan

	}

	public virtual void OnPointerUp(PointerEventData ped){		//ketika sentuhan dilepaskan
		anim.SetBool ("isMoving", false);		//animasi berhenti
	}
}
