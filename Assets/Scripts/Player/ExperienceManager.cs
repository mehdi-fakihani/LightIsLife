using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LIL;

public class ExperienceManager : MonoBehaviour {

    public AudioClip itemCollect;

    private AudioSource source;
    private int experience = 0;
    private int level;
    private int playerNum;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        playerNum = (int)gameObject.name[gameObject.name.Length - 1] - 48;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exp"))
        {
            source.PlayOneShot(itemCollect);
            GeneralData.IncrExperience(1, playerNum);
            Destroy(other.gameObject);
        }
    }

    public int GetExperience()
    {
        return experience;
    }

    public void setLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }
}
