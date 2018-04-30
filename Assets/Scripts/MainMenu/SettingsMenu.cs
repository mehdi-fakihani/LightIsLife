using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LIL.Inputs;

public class SettingsMenu : MonoBehaviour {

    public GameObject toolTipsText;
    public GameObject musicSlider, sfxSlider;
    public GameObject Controller1Line, Keyboard1Line, Controller2Line, Keyboard2Line, QWERTYLine, AZERTYLine;
    public GameObject LinePlayer1, LinePlayer2, lineMovement, lineCombat, lineGeneral;
    public  GameObject forwardKey, backwardsKey, leftKey, rightKey, swordAttackKey, skill1Key, skill2Key, skill3Key, skill4Key, pauseKey,
        interactKey, changeTorchKey, submitKey, cameraRightKey, cameraLeftKey;
    public GameObject PanelMovement, PanelCombat, PanelGeneral, areYouSurePanel, PanelGame;
    public GameObject gameBtn, controlsBtn, keyBtn, returnBtn;
    public GameObject Camera;
    

    private static int playerNum;

    private float sliderValue = 50f;
    private float sliderValueSFX = 30f;
    private string[] controllers;
    private List<int> controllerCount;

    private void Awake()
    {
        //  If already initialized then
        if(PlayerPrefs.HasKey("Keyboard"))
        {
            Debug.Log("already init");
            Debug.Log("up : " + ((Key)PlayerPrefs.GetInt("Keyboard1_Up")).ToString());

            // Setting up the Keyboard1 Model
            GeneralData.Keyboard1ProfileModel = new ProfileModel(ProfilsID.Keyboard1, Device.Keyboard, (Key)PlayerPrefs.GetInt("Keyboard1_Up"),
                (Key)PlayerPrefs.GetInt("Keyboard1_Down"), (Key)PlayerPrefs.GetInt("Keyboard1_Left"), (Key)PlayerPrefs.GetInt("Keyboard1_Right"),
                (Key)PlayerPrefs.GetInt("Keyboard1_Attack"), (Key)PlayerPrefs.GetInt("Keyboard1_Skill1"), (Key)PlayerPrefs.GetInt("Keyboard1_Skill2"),
                (Key)PlayerPrefs.GetInt("Keyboard1_Skill3"), (Key)PlayerPrefs.GetInt("Keyboard1_Skill4"), (Key)PlayerPrefs.GetInt("Keyboard1_Interaction"),
                (Key)PlayerPrefs.GetInt("Keyboard1_ChangeTorch"), (Key)PlayerPrefs.GetInt("Keyboard1_Submit"), (Key)PlayerPrefs.GetInt("Keyboard1_Pause"),
                (Key)PlayerPrefs.GetInt("Keyboard1_CameraRight"), (Key)PlayerPrefs.GetInt("Keyboard1_CameraLeft"));

            // Setting up the Keyboard2 Model
            GeneralData.Keyboard2ProfileModel = new ProfileModel(ProfilsID.Keyboard2, Device.Keyboard, (Key)PlayerPrefs.GetInt("Keyboard2_Up"),
                (Key)PlayerPrefs.GetInt("Keyboard2_Down"), (Key)PlayerPrefs.GetInt("Keyboard2_Left"), (Key)PlayerPrefs.GetInt("Keyboard2_Right"),
                (Key)PlayerPrefs.GetInt("Keyboard2_Attack"), (Key)PlayerPrefs.GetInt("Keyboard2_Skill1"), (Key)PlayerPrefs.GetInt("Keyboard2_Skill2"),
                (Key)PlayerPrefs.GetInt("Keyboard2_Skill3"), (Key)PlayerPrefs.GetInt("Keyboard2_Skill4"), (Key)PlayerPrefs.GetInt("Keyboard2_Interaction"),
                (Key)PlayerPrefs.GetInt("Keyboard2_ChangeTorch"), (Key)PlayerPrefs.GetInt("Keyboard2_Submit"), (Key)PlayerPrefs.GetInt("Keyboard2_Pause"),
                (Key)PlayerPrefs.GetInt("Keyboard2_CameraRight"), (Key)PlayerPrefs.GetInt("Keyboard2_CameraLeft"));

            // Setting up the Controller1 Model
            GeneralData.controller1ProfileModel = new ProfileModel(ProfilsID.XBoxGamepad, Device.XBoxGamepad, (Key)PlayerPrefs.GetInt("Gamepad1_Up"),
                (Key)PlayerPrefs.GetInt("Gamepad1_Down"), (Key)PlayerPrefs.GetInt("Gamepad1_Left"), (Key)PlayerPrefs.GetInt("Gamepad1_Right"),
                (Key)PlayerPrefs.GetInt("Gamepad1_Attack"), (Key)PlayerPrefs.GetInt("Gamepad1_Skill1"), (Key)PlayerPrefs.GetInt("Gamepad1_Skill2"),
                (Key)PlayerPrefs.GetInt("Gamepad1_Skill3"), (Key)PlayerPrefs.GetInt("Gamepad1_Skill4"), (Key)PlayerPrefs.GetInt("Gamepad1_Interaction"),
                (Key)PlayerPrefs.GetInt("Gamepad1_ChangeTorch"), (Key)PlayerPrefs.GetInt("Gamepad1_Submit"), (Key)PlayerPrefs.GetInt("Gamepad1_Pause"),
                (Key)PlayerPrefs.GetInt("Gamepad1_CameraRight"), (Key)PlayerPrefs.GetInt("Gamepad1_CameraLeft"));

            // Setting up the Controller2 Model
            GeneralData.controller2ProfileModel = new ProfileModel(ProfilsID.XBoxGamepad, Device.XBoxGamepad, (Key)PlayerPrefs.GetInt("Gamepad2_Up"),
                (Key)PlayerPrefs.GetInt("Gamepad2_Down"), (Key)PlayerPrefs.GetInt("Gamepad2_Left"), (Key)PlayerPrefs.GetInt("Gamepad2_Right"),
                (Key)PlayerPrefs.GetInt("Gamepad2_Attack"), (Key)PlayerPrefs.GetInt("Gamepad2_Skill1"), (Key)PlayerPrefs.GetInt("Gamepad2_Skill2"),
                (Key)PlayerPrefs.GetInt("Gamepad2_Skill3"), (Key)PlayerPrefs.GetInt("Gamepad2_Skill4"), (Key)PlayerPrefs.GetInt("Gamepad2_Interaction"),
                (Key)PlayerPrefs.GetInt("Gamepad2_ChangeTorch"), (Key)PlayerPrefs.GetInt("Gamepad2_Submit"), (Key)PlayerPrefs.GetInt("Gamepad2_Pause"),
                (Key)PlayerPrefs.GetInt("Gamepad2_CameraRight"), (Key)PlayerPrefs.GetInt("Gamepad2_CameraLeft"));
        }
        else
        {
            Debug.Log("init");
            initKeys();
        }

        controllers = Input.GetJoystickNames();
        controllerCount = new List<int>();
        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i] != "")
                controllerCount.Add(i);
        }

        if (PlayerPrefs.GetInt("Keyboard") == -1)
        {
            QWERTYLine.SetActive(true);
            AZERTYLine.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Keyboard") == -2)
        {
            QWERTYLine.SetActive(false);
            AZERTYLine.SetActive(true);
        }

        // check slider values
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");

        // check tool tip value
        if (PlayerPrefs.GetInt("ToolTips") == 0)
        {
            toolTipsText.GetComponent<Text>().text = "off";
        }
        else
        {
            toolTipsText.GetComponent<Text>().text = "on";
        }

        // Set the default input system for the player 1
        if (PlayerPrefs.GetInt("Input1") >= 0 && controllerCount.Contains(PlayerPrefs.GetInt("Input1")))
        {
            GeneralData.inputPlayer1 = ProfilsID.XBoxGamepad;
            Profile.Models[1] = GeneralData.controller1ProfileModel;
            Keyboard1Line.gameObject.SetActive(false);
            Controller1Line.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Input1") == -1 || controllerCount.Contains(PlayerPrefs.GetInt("Input1")))
        {
            GeneralData.inputPlayer1 = ProfilsID.Keyboard1;
            Profile.Models[1] = GeneralData.Keyboard1ProfileModel;
            Keyboard1Line.gameObject.SetActive(true);
            Controller1Line.gameObject.SetActive(false);
        }

        // Set the default input system for the player 2
        if (PlayerPrefs.GetInt("Input2") >= 0 && controllerCount.Contains(PlayerPrefs.GetInt("Input2")))
        {
            GeneralData.inputPlayer2 = ProfilsID.XBoxGamepad;
            Profile.Models[2] = GeneralData.controller2ProfileModel;
            Keyboard2Line.gameObject.SetActive(false);
            Controller2Line.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Input2") == -1 || controllerCount.Contains(PlayerPrefs.GetInt("Input2")))
        {
            GeneralData.inputPlayer2 = ProfilsID.Keyboard2;
            Profile.Models[2] = GeneralData.Keyboard2ProfileModel;
            Keyboard2Line.gameObject.SetActive(true);
            Controller2Line.gameObject.SetActive(false);
        }

        ChangeKeys.InitKeys(new GameObject[] {forwardKey, backwardsKey, leftKey, rightKey, swordAttackKey, skill1Key, skill2Key, skill3Key, skill4Key, pauseKey,
        interactKey, changeTorchKey, submitKey, cameraRightKey, cameraLeftKey });
    }

    // Use this for initialization
    void Start ()
    {
       /* controllers = Input.GetJoystickNames();
        controllerCount = new List<int>();
        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i] != "")
                controllerCount.Add(i);
        }

        if (PlayerPrefs.GetInt("Keyboard") == -1)
        {
            QWERTYLine.SetActive(true);
            AZERTYLine.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Keyboard") == -2)
        {
            QWERTYLine.SetActive(false);
            AZERTYLine.SetActive(true);
        }

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

        // Set the default input system for the player 1
        if (PlayerPrefs.GetInt("Input1") >= 0 && controllerCount.Contains(PlayerPrefs.GetInt("Input1")))
        {
            GeneralData.inputPlayer1 = ProfilsID.XBoxGamepad;
            Profile.Models[1] = GeneralData.controller1ProfileModel;
            Keyboard1Line.gameObject.SetActive(false);
            Controller1Line.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Input1") == -1 || controllerCount.Contains(PlayerPrefs.GetInt("Input1")))
        {
            GeneralData.inputPlayer1 = ProfilsID.Keyboard1;
            Profile.Models[1] = GeneralData.Keyboard1ProfileModel;
            Keyboard1Line.gameObject.SetActive(true);
            Controller1Line.gameObject.SetActive(false);
        }

        // Set the default input system for the player 2
        if (PlayerPrefs.GetInt("Input2") >= 0 && controllerCount.Contains(PlayerPrefs.GetInt("Input2")))
        {
            GeneralData.inputPlayer2 = ProfilsID.XBoxGamepad;
            Profile.Models[2] = GeneralData.controller2ProfileModel;
            Keyboard2Line.gameObject.SetActive(false);
            Controller2Line.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Input2") == -1 || controllerCount.Contains(PlayerPrefs.GetInt("Input2")))
        {
            GeneralData.inputPlayer2 = ProfilsID.Keyboard2;
            Profile.Models[2] = GeneralData.Keyboard2ProfileModel;
            Keyboard2Line.gameObject.SetActive(true);
            Controller2Line.gameObject.SetActive(false);
        }

        ChangeKeys.InitKeys(new GameObject[] {forwardKey, backwardsKey, leftKey, rightKey, swordAttackKey, skill1Key, skill2Key, skill3Key, skill4Key, pauseKey,
        interactKey, changeTorchKey, submitKey, cameraRightKey, cameraLeftKey });*/
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

        PlayerPrefs.DeleteAll();
        initKeys();
    }

    public void QWERTY()
    {
        PlayerPrefs.SetInt("Keyboard", -1);
        PlayerPrefs.SetInt("Keyboard1_Up", (int)Key.KeyboardW);                        // "W" for up
        Profile.Models[1].keys[LIL.PlayerAction.Up] = Key.KeyboardW;
        PlayerPrefs.SetInt("Keyboard1_Left", (int)Key.KeyboardA);                      // "A" for left
        Profile.Models[1].keys[LIL.PlayerAction.Left] = Key.KeyboardA;

        GeneralData.Keyboard1ProfileModel = Profile.Models[1];
        QWERTYLine.gameObject.SetActive(true);
        AZERTYLine.gameObject.SetActive(false);
    }

    public void AZERTY()
    {
        PlayerPrefs.SetInt("Keyboard", -2);
        PlayerPrefs.SetInt("Keyboard1_Up", (int)Key.KeyboardZ);                        // "Z" for up
        Profile.Models[1].keys[LIL.PlayerAction.Up] = Key.KeyboardZ;
        PlayerPrefs.SetInt("Keyboard1_Left", (int)Key.KeyboardQ);                      // "Q" for left
        Profile.Models[1].keys[LIL.PlayerAction.Left] = Key.KeyboardQ;

        GeneralData.Keyboard1ProfileModel = Profile.Models[1];
        QWERTYLine.gameObject.SetActive(false);
        AZERTYLine.gameObject.SetActive(true);
    }

    public void Keayboard(int playerNum)
    {
        PlayerPrefs.SetInt("Input" + playerNum, -1);
        if (playerNum == 1)
        {
            GeneralData.inputPlayer1 = ProfilsID.Keyboard1;
            Profile.Models[1] = GeneralData.Keyboard1ProfileModel;
            Keyboard1Line.gameObject.SetActive(true);
            Controller1Line.gameObject.SetActive(false);
        }
        if (playerNum == 2)
        {
            GeneralData.inputPlayer2 = ProfilsID.Keyboard2;
            Profile.Models[2] = GeneralData.Keyboard2ProfileModel;
            Keyboard2Line.gameObject.SetActive(true);
            Controller2Line.gameObject.SetActive(false);
        }
    }


    public void Controller(int playerNum)
    {
        if (playerNum == 1 && controllerCount.Count >= 1)
        {
            if (PlayerPrefs.GetInt("Input2") == -1 || (PlayerPrefs.GetInt("Input2") >= 0 && controllerCount.Count >= 2))
            {
                if(PlayerPrefs.GetInt("Input2") == -1)
                    PlayerPrefs.SetInt("Input" + playerNum, controllerCount.ToArray()[0]);
                else if(PlayerPrefs.GetInt("Input2") >=0 && controllerCount.Count >= 2)
                    PlayerPrefs.SetInt("Input" + playerNum, controllerCount.ToArray()[1]);


                GeneralData.inputPlayer1 = ProfilsID.XBoxGamepad;
                Profile.Models[1] = GeneralData.controller1ProfileModel;
                Keyboard1Line.gameObject.SetActive(false);
                Controller1Line.gameObject.SetActive(true);
            }
        }
        else if (playerNum == 2 && controllerCount.Count >= 1)
        {
            if (PlayerPrefs.GetInt("Input1")== -1 || (PlayerPrefs.GetInt("Input1") >= 0 && controllerCount.Count >= 2))
            {
                if(PlayerPrefs.GetInt("Input1") == -1)
                    PlayerPrefs.SetInt("Input" + playerNum, controllerCount.ToArray()[0]);
                else if(PlayerPrefs.GetInt("Input1") >= 0 && controllerCount.Count >= 2)
                    PlayerPrefs.SetInt("Input" + playerNum, controllerCount.ToArray()[1]);

                GeneralData.inputPlayer2 = ProfilsID.XBoxGamepad;
                Profile.Models[2] = GeneralData.controller2ProfileModel;
                Keyboard2Line.gameObject.SetActive(false);
                Controller2Line.gameObject.SetActive(true);
            }
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

        if (inputSystem == -1)
        {
            forwardKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Up].ToString().Substring(8);
            backwardsKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Down].ToString().Substring(8);
            leftKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Left].ToString().Substring(8);
            rightKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Right].ToString().Substring(8);
            cameraLeftKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.CameraLeft].ToString().Substring(8);
            cameraRightKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.CameraRight].ToString().Substring(8);
        }
        else if(inputSystem >= 0)
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

        if (inputSystem == -1)
        {
            swordAttackKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Attack].ToString().Substring(8);
            skill1Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill1].ToString().Substring(8);
            skill2Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill2].ToString().Substring(8);
            skill3Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill3].ToString().Substring(8);
            skill4Key.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Skill4].ToString().Substring(8);
        }
        else if (inputSystem >= 0)
        {
            swordAttackKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Attack].ToString().Substring(7);
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

        if (inputSystem == -1)
        {
            pauseKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Pause].ToString().Substring(8);
            changeTorchKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.ChangeTorch].ToString().Substring(8);
            interactKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Interaction].ToString().Substring(8);
            submitKey.GetComponent<Text>().text = Profile.Models[playerNum].keys[LIL.PlayerAction.Submit].ToString().Substring(8);
        }
        else if (inputSystem >= 0)
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

    public void DisableButtons()
    {
        gameBtn.SetActive(false);
        controlsBtn.SetActive(false);
        keyBtn.SetActive(false);
        returnBtn.SetActive(false);
    }

    public void ActiveButtons()
    {
        gameBtn.SetActive(true);
        controlsBtn.SetActive(true);
        keyBtn.SetActive(true);
        returnBtn.SetActive(true);
    }

    public void AreYouSure()
    {
        PanelGame.SetActive(false);
        areYouSurePanel.gameObject.SetActive(true);
        DisableButtons();
    }

    public void No()
    {
        areYouSurePanel.gameObject.SetActive(false);
        PanelGame.SetActive(true);
        ActiveButtons();
    }

    public void Yes()
    {
        ResetGame();
        areYouSurePanel.gameObject.SetActive(false);
        PanelGame.SetActive(true);
        ActiveButtons();
    }

    // Initialize input keys
    public void initKeys()
    {
        //  If not already initialized then
        if (!PlayerPrefs.HasKey("Keyboard"))
        {
            // Set QWERTY as keyboard system
            PlayerPrefs.SetInt("Keyboard", -1);
            QWERTYLine.SetActive(true);
            AZERTYLine.SetActive(false);

            // Keyboard 1 
            PlayerPrefs.SetInt("Keyboard1_Up", (int)Key.KeyboardW);                       // "W" for up
            PlayerPrefs.SetInt("Keyboard1_Down", (int)Key.KeyboardS);                      // "S" for down
            PlayerPrefs.SetInt("Keyboard1_Left", (int)Key.KeyboardA);                      // "A" for left
            PlayerPrefs.SetInt("Keyboard1_Right", (int)Key.KeyboardD);                     // "D" for right
            PlayerPrefs.SetInt("Keyboard1_Attack", (int)Key.KeyboardSpace);                // "space" for sword attack
            PlayerPrefs.SetInt("Keyboard1_Skill1", (int)Key.KeyboardG);                    // "G" for skill 1
            PlayerPrefs.SetInt("Keyboard1_Skill2", (int)Key.KeyboardH);                    // "H" for skill 2
            PlayerPrefs.SetInt("Keyboard1_Skill3", (int)Key.KeyboardJ);                    // "J" for skill 3
            PlayerPrefs.SetInt("Keyboard1_Skill4", (int)Key.KeyboardK);                    // "K" for skill 4
            PlayerPrefs.SetInt("Keyboard1_Interaction", (int)Key.KeyboardE);               // "E" for interaction
            PlayerPrefs.SetInt("Keyboard1_ChangeTorch", (int)Key.KeyboardE);               // "E" for Changing torch (interaction and changing troch could not be called together)
            PlayerPrefs.SetInt("Keyboard1_Submit", (int)Key.KeyboardSpace);                // "space" for submit (sword attack and submit could not be called together)
            PlayerPrefs.SetInt("Keyboard1_Pause", (int)Key.KeyboardESC);                   // "ESC" for pause
            PlayerPrefs.SetInt("Keyboard1_CameraRight", (int)Key.KeyboardL);               // "L" for camera right
            PlayerPrefs.SetInt("Keyboard1_CameraLeft", (int)Key.KeyboardLeftShift);        // "left Shift" for camera left

            // Keyboard 2
            PlayerPrefs.SetInt("Keyboard2_Up", (int)Key.KeyboardUp);                       // "up arrow" for up
            PlayerPrefs.SetInt("Keyboard2_Down", (int)Key.KeyboardDown);                   // "down arrow" for down
            PlayerPrefs.SetInt("Keyboard2_Left", (int)Key.KeyboardLeft);                   // "left arrow" for left
            PlayerPrefs.SetInt("Keyboard2_Right", (int)Key.KeyboardRight);                 // "right arrow" for right
            PlayerPrefs.SetInt("Keyboard2_Attack", (int)Key.KeyboardNUM0);                 // "numeric 0" for sword attack
            PlayerPrefs.SetInt("Keyboard2_Skill1", (int)Key.KeyboardNUM8);                 // "numeric 8" for skill 1
            PlayerPrefs.SetInt("Keyboard2_Skill2", (int)Key.KeyboardNUM4);                 // "numeric 4" for skill 2
            PlayerPrefs.SetInt("Keyboard2_Skill3", (int)Key.KeyboardNUM6);                 // "numeric 6" for skill 3
            PlayerPrefs.SetInt("Keyboard2_Skill4", (int)Key.KeyboardNUM5);                 // "numeric 5" for skill 4
            PlayerPrefs.SetInt("Keyboard2_Interaction", (int)Key.KeyboardNUM1);            // "numeric 1" for interaction
            PlayerPrefs.SetInt("Keyboard2_ChangeTorch", (int)Key.KeyboardNUM1);            // "numeric 1" for Changing torch (interaction and changing troch could not be called together)
            PlayerPrefs.SetInt("Keyboard2_Submit", (int)Key.KeyboardNUM0);                 // "numeric 0" for submit (sword attack and submit could not be called together)
            PlayerPrefs.SetInt("Keyboard2_Pause", (int)Key.KeyboardRightShift);            // "right shift" for pause
            PlayerPrefs.SetInt("Keyboard2_CameraRight", (int)Key.KeyboardNUMEnter);        // "numeric enter" for camera right
            PlayerPrefs.SetInt("Keyboard2_CameraLeft", (int)Key.KeyboardRightCtrl);        // "right ctrl" for camera left

            // Gamepad 1
            PlayerPrefs.SetInt("Gamepad1_Up", (int)Key.GamepadLeftJoystickUp);                      // "left joystick up" for up
            PlayerPrefs.SetInt("Gamepad1_Down", (int)Key.GamepadLeftJoystickDown);                  // "left joystick down" for down
            PlayerPrefs.SetInt("Gamepad1_Left", (int)Key.GamepadLeftJoystickLeft);                  // "left joystick left" for left
            PlayerPrefs.SetInt("Gamepad1_Right", (int)Key.GamepadLeftJoystickRight);                // "left joystick right" for right
            PlayerPrefs.SetInt("Gamepad1_Attack", (int)Key.GamepadR1);                              // "R1" for sword attack
            PlayerPrefs.SetInt("Gamepad1_Skill1", (int)Key.GamepadY);                               // "Y" for skill 1
            PlayerPrefs.SetInt("Gamepad1_Skill2", (int)Key.GamepadX);                               // "X" for skill 2
            PlayerPrefs.SetInt("Gamepad1_Skill3", (int)Key.GamepadB);                               // "B" for skill 3
            PlayerPrefs.SetInt("Gamepad1_Skill4", (int)Key.GamepadA);                               // "A" for skill 4
            PlayerPrefs.SetInt("Gamepad1_Interaction", (int)Key.GamepadL1);                         // "L1" for interaction
            PlayerPrefs.SetInt("Gamepad1_ChangeTorch", (int)Key.GamepadL1);                         // "L1" for Changing torch (interaction and changing troch could not be called together)
            PlayerPrefs.SetInt("Gamepad1_Submit", (int)Key.GamepadR1);                              // "R1" for submit (sword attack and submit could not be called together)
            PlayerPrefs.SetInt("Gamepad1_Pause", (int)Key.GamepadStart);                            // "start" for pause
            PlayerPrefs.SetInt("Gamepad1_CameraRight", (int)Key.GamepadRightJoystickRight);         // "right joystick right" for camera right
            PlayerPrefs.SetInt("Gamepad1_CameraLeft", (int)Key.GamepadRightJoystickLeft);           // "right joystick left" for camera left

            // Gamepad 2
            PlayerPrefs.SetInt("Gamepad2_Up", (int)Key.GamepadLeftJoystickUp);                      // "left joystick up" for up
            PlayerPrefs.SetInt("Gamepad2_Down", (int)Key.GamepadLeftJoystickDown);                  // "left joystick down" for down
            PlayerPrefs.SetInt("Gamepad2_Left", (int)Key.GamepadLeftJoystickLeft);                  // "left joystick left" for left
            PlayerPrefs.SetInt("Gamepad2_Right", (int)Key.GamepadLeftJoystickRight);                // "left joystick right" for right
            PlayerPrefs.SetInt("Gamepad2_Attack", (int)Key.GamepadR1);                              // "R1" for sword attack
            PlayerPrefs.SetInt("Gamepad2_Skill1", (int)Key.GamepadY);                               // "Y" for skill 1
            PlayerPrefs.SetInt("Gamepad2_Skill2", (int)Key.GamepadX);                               // "X" for skill 2
            PlayerPrefs.SetInt("Gamepad2_Skill3", (int)Key.GamepadB);                               // "B" for skill 3
            PlayerPrefs.SetInt("Gamepad2_Skill4", (int)Key.GamepadA);                               // "A" for skill 4
            PlayerPrefs.SetInt("Gamepad2_Interaction", (int)Key.GamepadL1);                         // "L1" for interaction
            PlayerPrefs.SetInt("Gamepad2_ChangeTorch", (int)Key.GamepadL1);                         // "L1" for Changing torch (interaction and changing troch could not be called together)
            PlayerPrefs.SetInt("Gamepad2_Submit", (int)Key.GamepadR1);                              // "R1" for submit (sword attack and submit could not be called together)
            PlayerPrefs.SetInt("Gamepad2_Pause", (int)Key.GamepadStart);                            // "start" for pause
            PlayerPrefs.SetInt("Gamepad2_CameraRight", (int)Key.GamepadRightJoystickRight);         // "right joystick right" for camera right
            PlayerPrefs.SetInt("Gamepad2_CameraLeft", (int)Key.GamepadRightJoystickLeft);           // "right joystick left" for camera left

            // Setting up the Keyboard1 Model
            GeneralData.Keyboard1ProfileModel = new ProfileModel(ProfilsID.Keyboard1, Device.Keyboard, (Key)PlayerPrefs.GetInt("Keyboard1_Up"),
                (Key)PlayerPrefs.GetInt("Keyboard1_Down"), (Key)PlayerPrefs.GetInt("Keyboard1_Left"), (Key)PlayerPrefs.GetInt("Keyboard1_Right"),
                (Key)PlayerPrefs.GetInt("Keyboard1_Attack"), (Key)PlayerPrefs.GetInt("Keyboard1_Skill1"), (Key)PlayerPrefs.GetInt("Keyboard1_Skill2"),
                (Key)PlayerPrefs.GetInt("Keyboard1_Skill3"), (Key)PlayerPrefs.GetInt("Keyboard1_Skill4"), (Key)PlayerPrefs.GetInt("Keyboard1_Interaction"),
                (Key)PlayerPrefs.GetInt("Keyboard1_ChangeTorch"), (Key)PlayerPrefs.GetInt("Keyboard1_Submit"), (Key)PlayerPrefs.GetInt("Keyboard1_Pause"),
                (Key)PlayerPrefs.GetInt("Keyboard1_CameraRight"), (Key)PlayerPrefs.GetInt("Keyboard1_CameraLeft"));

            // Setting up the Keyboard2 Model
            GeneralData.Keyboard2ProfileModel = new ProfileModel(ProfilsID.Keyboard2, Device.Keyboard, (Key)PlayerPrefs.GetInt("Keyboard2_Up"),
                (Key)PlayerPrefs.GetInt("Keyboard2_Down"), (Key)PlayerPrefs.GetInt("Keyboard2_Left"), (Key)PlayerPrefs.GetInt("Keyboard2_Right"),
                (Key)PlayerPrefs.GetInt("Keyboard2_Attack"), (Key)PlayerPrefs.GetInt("Keyboard2_Skill1"), (Key)PlayerPrefs.GetInt("Keyboard2_Skill2"),
                (Key)PlayerPrefs.GetInt("Keyboard2_Skill3"), (Key)PlayerPrefs.GetInt("Keyboard2_Skill4"), (Key)PlayerPrefs.GetInt("Keyboard2_Interaction"),
                (Key)PlayerPrefs.GetInt("Keyboard2_ChangeTorch"), (Key)PlayerPrefs.GetInt("Keyboard2_Submit"), (Key)PlayerPrefs.GetInt("Keyboard2_Pause"),
                (Key)PlayerPrefs.GetInt("Keyboard2_CameraRight"), (Key)PlayerPrefs.GetInt("Keyboard2_CameraLeft"));

            // Setting up the Controller1 Model
            GeneralData.controller1ProfileModel = new ProfileModel(ProfilsID.XBoxGamepad, Device.XBoxGamepad, (Key)PlayerPrefs.GetInt("Gamepad1_Up"),
                (Key)PlayerPrefs.GetInt("Gamepad1_Down"), (Key)PlayerPrefs.GetInt("Gamepad1_Left"), (Key)PlayerPrefs.GetInt("Gamepad1_Right"),
                (Key)PlayerPrefs.GetInt("Gamepad1_Attack"), (Key)PlayerPrefs.GetInt("Gamepad1_Skill1"), (Key)PlayerPrefs.GetInt("Gamepad1_Skill2"),
                (Key)PlayerPrefs.GetInt("Gamepad1_Skill3"), (Key)PlayerPrefs.GetInt("Gamepad1_Skill4"), (Key)PlayerPrefs.GetInt("Gamepad1_Interaction"),
                (Key)PlayerPrefs.GetInt("Gamepad1_ChangeTorch"), (Key)PlayerPrefs.GetInt("Gamepad1_Submit"), (Key)PlayerPrefs.GetInt("Gamepad1_Pause"),
                (Key)PlayerPrefs.GetInt("Gamepad1_CameraRight"), (Key)PlayerPrefs.GetInt("Gamepad1_CameraLeft"));

            // Setting up the Controller2 Model
            GeneralData.controller2ProfileModel = new ProfileModel(ProfilsID.XBoxGamepad, Device.XBoxGamepad, (Key)PlayerPrefs.GetInt("Gamepad2_Up"),
                (Key)PlayerPrefs.GetInt("Gamepad2_Down"), (Key)PlayerPrefs.GetInt("Gamepad2_Left"), (Key)PlayerPrefs.GetInt("Gamepad2_Right"),
                (Key)PlayerPrefs.GetInt("Gamepad2_Attack"), (Key)PlayerPrefs.GetInt("Gamepad2_Skill1"), (Key)PlayerPrefs.GetInt("Gamepad2_Skill2"),
                (Key)PlayerPrefs.GetInt("Gamepad2_Skill3"), (Key)PlayerPrefs.GetInt("Gamepad2_Skill4"), (Key)PlayerPrefs.GetInt("Gamepad2_Interaction"),
                (Key)PlayerPrefs.GetInt("Gamepad2_ChangeTorch"), (Key)PlayerPrefs.GetInt("Gamepad2_Submit"), (Key)PlayerPrefs.GetInt("Gamepad2_Pause"),
                (Key)PlayerPrefs.GetInt("Gamepad2_CameraRight"), (Key)PlayerPrefs.GetInt("Gamepad2_CameraLeft"));

            // Set music default value
            if (!PlayerPrefs.HasKey("MusicVolume")) PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().maxValue / 2);
            musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
            Camera.GetComponent<ConfigureMusicVolume>().UpdateMusicVolume();

            if (!PlayerPrefs.HasKey("SFXVolume")) PlayerPrefs.SetFloat("SFXVolume", sfxSlider.GetComponent<Slider>().maxValue / 3);
            sfxSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");
            Camera.GetComponent<ConfigureMusicVolume>().UpdateSFXVolume();


            // Set the default input system for the player 1
            if (!PlayerPrefs.HasKey("Input1")) PlayerPrefs.SetInt("Input1", -1);
            GeneralData.inputPlayer1 = ProfilsID.Keyboard1;
            Profile.Models[1] = GeneralData.Keyboard1ProfileModel;
            Keyboard1Line.gameObject.SetActive(true);
            Controller1Line.gameObject.SetActive(false);

            // Set the default input system for the player 2
            if (!PlayerPrefs.HasKey("Input2")) PlayerPrefs.SetInt("Input2", -1);
            GeneralData.inputPlayer2 = ProfilsID.Keyboard2;
            Profile.Models[2] = GeneralData.Keyboard2ProfileModel;
            Keyboard2Line.gameObject.SetActive(true);
            Controller2Line.gameObject.SetActive(false);

        }
    }
}
