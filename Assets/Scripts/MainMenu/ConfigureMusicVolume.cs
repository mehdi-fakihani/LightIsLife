using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureMusicVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    public void UpdateMusicVolume()
    {
        this.transform.GetChild(1).GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void UpdateSFXVolume()
    {
        this.transform.GetChild(0).GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume") / 10;
        this.transform.GetChild(2).GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume") / 10;
        this.transform.GetChild(3).GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume") / 10;
    }
}

