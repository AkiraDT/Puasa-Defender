using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour {
	public Sprite musicOnSprite;
	public Sprite musicOffSprite;
	public GameObject MusicToggleButton;

	private MusicPlayer m_MusicPlayer;
	// Use this for initialization
	void Start () {
		m_MusicPlayer = GameObject.FindObjectOfType<MusicPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleButton(){
		m_MusicPlayer.ToggleSound ();
	}

	public void UpdateIcon(){
		if (PlayerPrefs.GetInt ("Muted") == 0) {
			AudioListener.volume = 1;
			MusicToggleButton.GetComponent<Image> ().sprite = musicOnSprite;
		} else {
			AudioListener.volume = 0;
			MusicToggleButton.GetComponent<Image> ().sprite = musicOffSprite;
		}
	}
}
