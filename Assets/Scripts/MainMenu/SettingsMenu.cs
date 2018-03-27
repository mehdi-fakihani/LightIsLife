using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LIL.Inputs;

public class SettingsMenu : MonoBehaviour {

    public GameObject toolTipsText;
    public GameObject musicSlider, sfxSlider;
    public GameObject ControllerLine1, QwertyLine1, AzertyLine1, ControllerLine2, QwertyLine2, AzertyLine2;
    public GameObject LinePlayer1, LinePlayer2, lineMovement, lineCombat, lineGeneral;
    public GameObject forwardKey, backwardsKey, leftKey, rightKey, sowrdAttackKey, skill1Key, skill2Key, skill3Key, skill4Key, pauseKey,
        interactKey, changeTorchKey, submitKey, cameraRightKey, cameraLeftKey;
    public GameObject PanelMovement, PanelCombat, PanelGeneral;

    private static int playerNum;

    private float sliderValue = 0f;
    private float sliderValueSFX = 0f;

    // Use this for initialization
    void Start ()
    {
        // check slider values
        musicSlider.GetComponent< Slider > ().value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.GetComponent< Slider > ().value = PlayerPrefs.GetFloat("SFXVolume");

        // check tool tip value
        if (PlayerPrefs.GetInt("ToolTips") == 0)
        {
            toolTipsText.GetComponent<Text>().text = "off";
        }
        else
        {
            toolTipsText.GetComponent<Text>().text = "on";
        }

        // check Input system of the 1st player
        if (PlayerPrefs.GetInt("Input1") == 0)
        {
            GeneralData.inputPlayer1 = ProfilsID.KeyboardAZERTY;
            Profile.Models[1] = GeneralData.azertyProfileModel;
            AzertyLine1.gameObject.SetActive(true);
            QwertyLine1.gameObject.SetActive(false);
            ControllerLine1.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Input1") == 1)
        {
            GeneralData.inputPlayer1 = ProfilsID.KeyboardQWERTY;
            Profile.Models[1] = GeneralData.qwertyProfileModel;
            AzertyLine1.gameObject.SetActive(false);
            QwertyLine1.gameObject.SetActive(true);
            ControllerLine1.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Input1") == 2)
        {
            GeneralData.inputPlayer1 = ProfilsID.XBoxGamepad;
            Profile.Models[1] = GeneralData.controllerProfileModel;
            AzertyLine1.gameObject.SetActive(false);
            QwertyLine1.gameObject.SetActive(false);
            ControllerLine1.gameObject.SetActive(true);
        }

        // check Input system of the 2nd player
        if (PlayerPrefs.GetInt("Input2") == 0)
        {
            GeneralData.inputPlayer2 = ProfilsID.KeyboardAZERTY;
            Profile.Models[2] = GeneralData.azertyProfileModel;
            AzertyLine2.gameObject.SetActive(true);
            QwertyLine2.gameObject.SetActive(false);
            ControllerLine2.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Input2") == 1)
        {
            GeneralData.inputPlayer2 = ProfilsID.KeyboardQWERTY;
            Profile.Models[2] = GeneralData.qwertyProfileModel;
            AzertyLine2.gameObject.SetActive(false);
            QwertyLine2.gameObject.SetActive(true);
            ControllerLine2.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Input2") == 2)
        {
            GeneralData.inputPlayer2 = ProfilsID.XBoxGamepad;
            Profile.Models[2] = GeneralData.controllerProfileModel;
            AzertyLine2.gameObject.SetActive(false);
            QwertyLine2.gameObject.SetActive(false);
            ControllerLine2.gameObject.SetActive(true);
        }


    }
	
	// Update is called once per frame
	void Update ()
    {
        sliderValue = musicSlider.GetComponent< Slider > ().value;
        sliderValueSFX = sfxSlider.GetComponent< Slider > ().value;
    }

    public void MusicSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SFXSlider()
    {
        PlayerPrefs.SetFloat("SFXVolume", sliderValueSFX);
    }

    // show tool tips like: 'How to Play' control pop ups
    public void ToolTips()
    {
        if (PlayerPrefs.GetInt("ToolTips") == 0)
        {
            PlayerPrefs.SetInt("ToolTips", 1);
            toolTipsText.GetComponent<Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("ToolTips") == 1)
        {
            PlayerPrefs.SetInt("ToolTips", 0);
            toolTipsText.GetComponent<Text>().text = "off";
        }
    }

    public void ResetGame()
    {
        if (Directory.Exists(GeneralData.path))
        {
            string[] filesName = Directory.GetFiles(GeneralData.path, "*.sav");
            foreach(string fileName in filesName)
            {
                File.Delete(fileName);
            }
        }
    }

    public void Azerty(int playerNum)
    {
        PlayerPrefs.SetInt("Input" + playerNum, 0);
        if (playerNum == 1)
        {
            GeneralData.inputPlayer1 = ProfilsID.KeyboardAZERTY;
            Profile.Models[1] = GeneralData.azertyProfileModel;
            AzertyLine1.gameObject.SetActive(true);
            QwertyLine1.gameObject.SetActive(false);
            ControllerLine1.gameObject.SetActive(false);
        }
        if (playerNum == 2)
        {
            GeneralData.inputPlayer2 = ProfilsID.KeyboardAZERTY;
            Profile.Models[2] = GeneralData.azertyProfileModel;
            AzertyLine2.gameObject.SetActive(true);
            QwertyLine2.gameObject.SetActive(false);
            ControllerLine2.gameObject.SetActive(false);
        }
    }

    public void Qwerty(int playerNum)
    {

        PlayerPrefs.SetInt("Input" + playerNum, 1);
        if (playerNum == 1)
        {
            GeneralData.inputPlayer1 = ProfilsID.KeyboardQWERTY;
            Profile.Models[1] = GeneralData.qwertyProfileModel;
            AzertyLine1.gameObject.SetActive(false);
            QwertyLine1.gameObject.SetActive(true);
            ControllerLine1.gameObject.SetActive(false);
        }
        if (playerNum == 2)
        {
            GeneralData.inputPlayer2 = ProfilsID.KeyboardQWERTY;
            Profile.Models[2] = GeneralData.qwertyProfileModel;
            AzertyLine2.gameObject.SetActive(false);
            QwertyLine2.gameObject.SetActive(true);
            ControllerLine2.gameObject.SetActive(false);
        }
    }
    

    public void Controller(int playerNum)
    {
        PlayerPrefs.SetInt("Input" + playerNum, 2);
        if (playerNum == 1)
        {
            GeneralData.inputPlayer1 = ProfilsID.XBoxGamepad;
            Profile.Models[1] = GeneralData.controllerProfileModel;
            AzertyLine1.gameObject.SetActive(false);
            QwertyLine1.gameObject.SetActive(false);
            ControllerLine1.gameObject.SetActive(true);
        }
        if (playerNum == 2)
        {
            GeneralData.inputPlayer2 = ProfilsID.XBoxGamepad;
            Profile.Models[2] = GeneralData.controllerProfileModel;
            AzertyLine2.gameObject.SetActive(false);
            QwertyLine2.gameObject.SetActive(false);
            ControllerLine2.gameObject.SetActive(true);
        }
    }

    public void player1()
    {
        playerNum = 1;
        LinePlayer1.gameObject.SetActive(true);
        LinePlayer2.gameObject.SetActive(false);
    }

    public void player2()
    {
        playerNum = 2;
        LinePlayer1.gameObject.SetActive(false);
        LinePlayer2.gameObject.SetActive(true);
    }

    public void MouvementPanel()
    {
        PanelMovement.gameObject.SetActive(true);
        PanelCombat.gameObject.SetActive(false);
        PanelGeneral.gameObject.SetActive(false);

        lineMovement.gameObject.SetActive(true);
        lineCombat.gameObject.SetActive(false);
        lineGeneral.gameObject.SetActive(false);

        int inputSystem = PlayerPrefs.GetInt("Input"+playerNum);

        if (inputSystem <= 1)
        {
            forwardKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Up].ToString().Substring(8);
            backwardsKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Down].ToString().Substring(8);
            leftKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Left].ToString().Substring(8);
            rightKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Right].ToString().Substring(8);
            cameraLeftKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.CameraLeft].ToString().Substring(8);
            cameraRightKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.CameraRight].ToString().Substring(8);
        }
        else if(inputSystem == 2)
        {
            forwardKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Up].ToString().Substring(7);
            backwardsKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Down].ToString().Substring(7);
            leftKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Left].ToString().Substring(7);
            rightKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Right].ToString().Substring(7);
            cameraLeftKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.CameraLeft].ToString().Substring(7);
            cameraRightKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.CameraRight].ToString().Substring(7);
        }
    }

    public void CombatPanel()
    {
        PanelMovement.gameObject.SetActive(false);
        PanelCombat.gameObject.SetActive(true);
        PanelGeneral.gameObject.SetActive(false);

        lineMovement.gameObject.SetActive(false);
        lineCombat.gameObject.SetActive(true);
        lineGeneral.gameObject.SetActive(false);

        int inputSystem = PlayerPrefs.GetInt("Input" + playerNum);

        if (inputSystem <= 1)
        {
            sowrdAttackKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Attack].ToString().Substring(8);
            skill1Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill1].ToString().Substring(8);
            skill2Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill2].ToString().Substring(8);
            skill3Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill3].ToString().Substring(8);
            skill4Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill4].ToString().Substring(8);
        }
        else if (inputSystem == 2)
        {
            sowrdAttackKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Attack].ToString().Substring(7);
            skill1Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill1].ToString().Substring(7);
            skill2Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill2].ToString().Substring(7);
            skill3Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill3].ToString().Substring(7);
            skill4Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill4].ToString().Substring(7);
        }
    }

    public void GeneralPanel()
    {
        PanelMovement.gameObject.SetActive(false);
        PanelCombat.gameObject.SetActive(false);
        PanelGeneral.gameObject.SetActive(true);

        lineMovement.gameObject.SetActive(false);
        lineCombat.gameObject.SetActive(false);
        lineGeneral.gameObject.SetActive(true);

        int inputSystem = PlayerPrefs.GetInt("Input" + playerNum);

        if (inputSystem <= 1)
        {
            pauseKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Pause].ToString().Substring(8);
            changeTorchKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.ChangeTorch].ToString().Substring(8);
            interactKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Interaction].ToString().Substring(8);
            submitKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Submit].ToString().Substring(8);
        }
        else if (inputSystem == 2)
        {
            pauseKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Pause].ToString().Substring(7) ;
            changeTorchKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.ChangeTorch].ToString().Substring(7);
            interactKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Interaction].ToString().Substring(7);
            submitKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Submit].ToString().Substring(7);
        }
    }

    public static int getPlayerNum()
    {
        return playerNum;
    }
}
