using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusic : MonoBehaviour {

    public AudioClip ambientMusic;
    public AudioClip fightStart;
    public AudioClip fightLoop;
    [SerializeField] private float fadingTime = 0.5f;

    private AudioSource[] sources; // needs 2 sources for now
    private bool isFighting;

    private readonly int ambientId = 0;
    private readonly int fightId = 1;
    

	// Use this for initialization
	void Start () {
        sources = GetComponentsInChildren<AudioSource>();

        foreach(AudioSource source in sources)  source.volume = PlayerPrefs.GetFloat("MusicVolume");

        sources[ambientId].loop = true;
        sources[fightId].loop = false;
        sources[ambientId].clip = ambientMusic;
        sources[fightId].clip = fightStart;
        sources[ambientId].Play();
        isFighting = false;
     }

    public void StartFightMusic()
    {
        if (!isFighting)
        {
            if(sources == null || sources.Length == 0) sources = GetComponentsInChildren<AudioSource>();
            StartCoroutine(AmbientToFight(sources[ambientId], sources[fightId], fadingTime));
        }
    }

    public bool IsFighting()
    {
        return isFighting;
    }

    public void EndFightMusic()
    {
        if (isFighting)
        {
            StartCoroutine(FightToAmbient(sources[fightId], sources[ambientId], fadingTime));
        }
    }

    private void LoopFightMusic()
    {
        sources[fightId].loop = true;
        sources[fightId].clip = fightLoop;
        sources[fightId].Play();
    }

    private IEnumerator FightToAmbient(AudioSource fadeOut, AudioSource fadeIn, float FadeTime)
    {
        float startVolumeOut = fadeOut.volume;
        float startVolumeIn = fadeIn.volume;
        fadeIn.volume = 0;
        fadeIn.Play();

        while (fadeOut.volume > 0)
        {
            fadeOut.volume -= startVolumeOut * Time.deltaTime / FadeTime;
            fadeIn.volume += startVolumeIn * Time.deltaTime / FadeTime;
            yield return null;
        }
        
        fadeOut.Stop();
        fadeOut.volume = startVolumeOut;
        sources[fightId].clip = fightStart;
        sources[fightId].loop = false;
        isFighting = false;
    }

    private IEnumerator AmbientToFight(AudioSource fadeOut, AudioSource fadeIn, float FadeTime)
    {
        float startVolumeOut = fadeOut.volume;
        float startVolumeIn = fadeIn.volume;
        fadeIn.volume = 0;
        Invoke("LoopFightMusic", fightStart.length - 0.5f);
        fadeIn.Play();

        while (fadeOut.volume > 0)
        {
            fadeOut.volume -= startVolumeOut * Time.deltaTime / FadeTime;
            fadeIn.volume += startVolumeIn * Time.deltaTime / FadeTime;
            yield return null;
        }

        fadeOut.Stop();
        fadeOut.volume = startVolumeOut;
        isFighting = true;
    }
}
