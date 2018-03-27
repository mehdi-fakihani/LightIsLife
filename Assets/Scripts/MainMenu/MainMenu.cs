using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LIL.Inputs;

public class MainMenu : MonoBehaviour
{

    public Animator animator;
    public GameObject PanelControls, PanelGame, PanelKeyBindings, PanelMovement,
        PanelCombat, PanelGeneral, hoverSound, sfxhoversound, clickSound, areYouSure, PanelCredits, PanelLoad;
    // campaign button sub menu
    public GameObject continueBtn, newGameBtn, loadGameBtn, singlePlayerBtn, multiPlayerBtn;
    // highlights
    public GameObject lineGame, lineControls, lineKeyBindings, lineCombat,
        lineGeneral, lineMovement;
    // Mouse Sprite
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public string sceneName = "sceneJulien";

    public void Awake()
    {
        GeneralData.mainMenuID = SceneManager.GetActiveScene().buildIndex;

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        GeneralData.sceneName = sceneName;

        // Setting up the Azerty Model
        GeneralData.azertyProfileModel = new ProfileModel(ProfilsID.KeyboardAZERTY, Device.Keyboard, Key.KeyboardI, Key.KeyboardK, Key.KeyboardJ,
            Key.KeyboardL, Key.KeyboardSpace, Key.Keyboard1, Key.Keyboard2, Key.Keyboard3, Key.Keyboard4, Key.KeyboardE, Key.KeyboardX,
            Key.KeyboardV, Key.KeyboardESC, Key.KeyboardLeft, Key.KeyboardRight);

        // Setting up the Qwerty Model
        GeneralData.qwertyProfileModel = new ProfileModel(ProfilsID.KeyboardQWERTY, Device.Keyboard, Key.KeyboardW, Key.KeyboardS, Key.KeyboardA,
            Key.KeyboardD, Key.KeyboardR, Key.KeyboardNUM8, Key.KeyboardNUM4, Key.KeyboardNUM6, Key.KeyboardNUM5, Key.KeyboardE, Key.KeyboardX,
            Key.KeyboardV, Key.KeyboardESC, Key.KeyboardLeft, Key.KeyboardRight);

        // Setting up the Controller Model
        GeneralData.controllerProfileModel = new ProfileModel(ProfilsID.XBoxGamepad, Device.XBoxGamepad, Key.GamepadLeftJoystickUp,
            Key.GamepadLeftJoystickDown, Key.GamepadLeftJoystickLeft, Key.GamepadLeftJoystickRight, Key.GamepadR1, Key.GamepadA,
            Key.GamepadB, Key.GamepadX, Key.GamepadY, Key.GamepadL1, Key.GamepadR2, Key.GamepadL2, Key.GamepadStart, Key.GamepadRightJoystickDown, Key.GamepadRightJoystickUp);

    }

    public void PlayCampaign()
    {
        DisableNewGame();
        areYouSure.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(true);
        newGameBtn.gameObject.SetActive(true);
        loadGameBtn.gameObject.SetActive(true);
        PanelLoad.gameObject.SetActive(false);
        PanelCredits.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        areYouSure.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(false);
        newGameBtn.gameObject.SetActive(true);
        loadGameBtn.gameObject.SetActive(false);
        singlePlayerBtn.gameObject.SetActive(true);
        multiPlayerBtn.gameObject.SetActive(true);
        PanelCredits.gameObject.SetActive(false);
    }


    public void DisablePlayCampaign()
    {
        continueBtn.gameObject.SetActive(false);
        newGameBtn.gameObject.SetActive(false);
        loadGameBtn.gameObject.SetActive(false);
        PanelLoad.gameObject.SetActive(false);
        DisableNewGame();
    }

    public void DisableNewGame()
    {
        singlePlayerBtn.gameObject.SetActive(false);
        multiPlayerBtn.gameObject.SetActive(false);
    }

    public void SinglePlayer()
    {
        DisablePlayCampaign();
        GeneralData.multiplayer = false;
        GeneralData.New();
        SceneManager.LoadScene(sceneName);
    }

    public void MultiPlayer()
    {
        DisablePlayCampaign();
        GeneralData.multiplayer = true;
        GeneralData.New();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadPanel()
    {
        areYouSure.gameObject.SetActive(false);
        PanelCredits.gameObject.SetActive(false);
        DisablePlayCampaign();

        PanelLoad.gameObject.SetActive(true);

        List<string> files = GeneralData.FilesToLoad();

        for (int i = 0; i < files.Count; i++)
        {
            PanelLoad.transform.GetChild(i + 2).GetComponentInChildren<Text>().text = files[i].Substring(0, files[i].Length - 4);
        }
        if (files == null || files.Count == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                PanelLoad.transform.GetChild(i + 2).GetComponentInChildren<Text>().text = "EMPTY SLOT";
            }
        }
    }

    public void CloseLoadPanel()
    {
        PanelLoad.gameObject.SetActive(false);
    }

    public void ContinueLastGame()
    {
        GeneralData.fileName = GeneralData.LastGamePlayed();
        if (GeneralData.fileName != "Unkown")
        {
            GeneralData.Load(GeneralData.LastGamePlayed());
            SceneManager.LoadScene(sceneName);
        }
    }

    public void Credits()
    {
        DisablePlayCampaign();
        areYouSure.gameObject.SetActive(false);
        PanelCredits.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        PanelCredits.gameObject.SetActive(false);
    }

    public void Position2()
    {
        DisablePlayCampaign();
        animator.SetFloat("Animate", 1);
    }

    public void Position1()
    {
        DisablePlayCampaign();
        animator.SetFloat("Animate", 0);
    }

    public void GamePanel()
    {
        PanelControls.gameObject.SetActive(false);
        PanelGame.gameObject.SetActive(true);
        PanelKeyBindings.gameObject.SetActive(false);

        lineGame.gameObject.SetActive(true);
        lineControls.gameObject.SetActive(false);
        lineKeyBindings.gameObject.SetActive(false);
    }

    public void ControlsPanel()
    {
        PanelControls.gameObject.SetActive(true);
        PanelGame.gameObject.SetActive(false);
        PanelKeyBindings.gameObject.SetActive(false);

        lineGame.gameObject.SetActive(false);
        lineControls.gameObject.SetActive(true);
        lineKeyBindings.gameObject.SetActive(false);

    }

    public void KeyBindingsPanel()
    {
        PanelControls.gameObject.SetActive(false);
        PanelGame.gameObject.SetActive(false);
        PanelKeyBindings.gameObject.SetActive(true);

        lineGame.gameObject.SetActive(false);
        lineControls.gameObject.SetActive(false);
        lineKeyBindings.gameObject.SetActive(true);
    }



    public void PlayHover()
    {
        hoverSound.GetComponent<AudioSource>().Play();
    }

    public void PlaySFXHover()
    {
        sfxhoversound.GetComponent<AudioSource>().Play();
    }

    public void PlayClick()
    {
        clickSound.GetComponent<AudioSource>().Play();
    }

    public void AreYouSure()
    {
        areYouSure.gameObject.SetActive(true);
        DisablePlayCampaign();
        PanelCredits.gameObject.SetActive(false);
    }

    public void No()
    {
        areYouSure.gameObject.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }

}
