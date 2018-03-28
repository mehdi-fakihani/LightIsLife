using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    [SerializeField] private GameObject player;
    private Animator playerAnimation;
    private float timer;
	// Use this for initialization
	void Start () {
        Debug.Log("OK");
        playerAnimation = player.GetComponent<Animator>();
        //playerAnimation.SetTrigger("death");
        playerAnimation.Play("Death");
    }

    public void GoMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuDorian");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
