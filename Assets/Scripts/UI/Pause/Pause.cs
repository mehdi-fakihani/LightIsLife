using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LIL.Inputs;

public class Pause : MonoBehaviour {

    public GameObject pausePanel;
    private int playerNum;
    private Profile profile;

    public void pauseGame(int _playerNum, Profile _profile)
    {
        playerNum = _playerNum;
        profile = _profile;
        pausePanel.SetActive(true);
        this.transform.GetChild(1).GetChild(1).GetComponent<Selectable>().Select();
        GeneralData.gamePaused = true;
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        GeneralData.gamePaused = false;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene(GeneralData.mainMenuID);
    }

    public int getPlayerNum()
    {
        return playerNum;
    }

    public Profile getProfile()
    {
        return profile;
    }
}
