using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour {

    private int experience = 0;
    private int level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exp"))
        {
            Destroy(other.gameObject);
            experience++;
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
