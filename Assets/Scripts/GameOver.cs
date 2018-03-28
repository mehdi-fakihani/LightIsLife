using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    [SerializeField] private GameObject player;
    private Animator playerAnimation;
    private float timer;
	// Use this for initialization
	void Start () {
        playerAnimation = player.GetComponent<Animator>();
        //playerAnimation.SetTrigger("death");
        playerAnimation.Play("Death");
    }

}
