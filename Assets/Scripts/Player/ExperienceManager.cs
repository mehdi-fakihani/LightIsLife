using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour {

    public AudioClip itemCollect;

    private AudioSource source;
    private int experience = 0;
    private int level;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exp"))
        {
            source.PlayOneShot(itemCollect);
            experience++;
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
