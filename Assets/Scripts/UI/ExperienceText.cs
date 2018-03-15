using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceText : MonoBehaviour {

    // Use this for initialization
    private UnityEngine.UI.Text text;
    [SerializeField] private ExperienceManager player;

    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }
    void Update()
    {
        text.text = " level : " + player.GetLevel();
    }
}
