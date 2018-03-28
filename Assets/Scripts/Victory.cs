using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    [SerializeField] private GameObject player;
    private Animator playerAnimation;
    private float timer;
    // Use this for initialization
    void Start()
    {
        playerAnimation = player.GetComponent<Animator>();
        //playerAnimation.SetTrigger("death");
        playerAnimation.Play("Victory");
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
