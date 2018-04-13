using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LIL.Inputs;
using System;
using LIL;

public class ChangeKeys : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private bool selected;
    private int playerNum;
    private Event keyEvent;
    private Key newKey;
    private Key[] values;
    public static GameObject[] keysGameObject;

    public void OnDeselect(BaseEventData eventData)
    {
        selected = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        selected = true;

    }

    public static void InitKeys(GameObject[] _keysGameObject)
    {
        keysGameObject = _keysGameObject;
    }

    public static void UpdateKeysText(int playerNum)
    {
        for(int i=0; i<keysGameObject.Length; i++)
        {
            int length = 8;

            if (PlayerPrefs.GetInt("Input" + playerNum) == 2)
                length = 7;
        }
    }

    public GameObject GetKeyGameObject(string name)
    {
        for(int i = 0; i<keysGameObject.Length; i++)
        {
            if (keysGameObject[i].name == name)
                return keysGameObject[i];
        }
        return null ;
    }

    // Use this for initialization
    void Start () {
        playerNum = SettingsMenu.getPlayerNum();
        values = (Key[])Enum.GetValues(typeof(Key));
    }

    private void OnGUI()
    {
        if (selected)
        {
            if (playerNum != SettingsMenu.getPlayerNum())
                playerNum = SettingsMenu.getPlayerNum();

            keyEvent = Event.current;
            if (keyEvent.isKey)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if ((int)keyEvent.keyCode == (int)values[i])
                    {
                        newKey = values[i];
                        PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                        if (!Profile.Models[playerNum].keys.ContainsValue(newKey) && newKey.ToString().StartsWith("Keyboard"))
                        {
                            PlayerPrefs.SetInt("Keyboard" + playerNum + "_" + this.name, (int)newKey);
                            Profile.Models[playerNum].keys[action] = newKey;
                            this.GetComponent<Text>().text = newKey.ToString().Substring(8);
                            
                        }
                        else
                        {
                            List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                            foreach (PlayerAction pa in playerActions)
                            {
                                if ((int)Profile.Models[playerNum].keys[pa] == (int)newKey)
                                {
                                    Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                                    PlayerPrefs.SetInt("Keyboard" + playerNum + "_" + pa.ToString(), (int) Profile.Models[playerNum].keys[action]);
                                    GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(8);
                                    PlayerPrefs.SetInt("Keyboard" + playerNum + "_" + this.name, (int)newKey);
                                    Profile.Models[playerNum].keys[action] = newKey;
                                    this.GetComponent<Text>().text = newKey.ToString().Substring(8);
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int keyShift = 0;
        if (PlayerPrefs.GetInt("Input" + playerNum) !=-1)
            keyShift = PlayerPrefs.GetInt("Input" + playerNum) * ((int)KeyCode.Joystick2Button0 - (int)KeyCode.Joystick1Button0);

        if (selected)
        {
            if (playerNum != SettingsMenu.getPlayerNum())
                playerNum = SettingsMenu.getPlayerNum();

            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button0 + keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadA.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadA))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadA;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadA);
                    this.GetComponent<Text>().text = Key.GamepadA.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadA)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadA;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadA);
                            this.GetComponent<Text>().text = Key.GamepadA.ToString().Substring(7);
                        }
                    }
                }
            }

            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button1+keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadB.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadB))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadB;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadB);
                    this.GetComponent<Text>().text = Key.GamepadB.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadB)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadB;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadB);
                            this.GetComponent<Text>().text = Key.GamepadB.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button2+keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadX.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadX))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadX;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadX);
                    this.GetComponent<Text>().text = Key.GamepadX.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);
                    
                    foreach ( PlayerAction pa in playerActions)
                    {
                        if ((int) Profile.Models[playerNum].keys[pa] == (int)Key.GamepadX)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadX;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadX);
                            this.GetComponent<Text>().text = Key.GamepadX.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button3 + keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadY.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadY))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadY;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadY);
                    this.GetComponent<Text>().text = Key.GamepadY.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadY)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadY;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadY);
                            this.GetComponent<Text>().text = Key.GamepadY.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button4 + keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadL1.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadL1))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadL1;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadL1);
                    this.GetComponent<Text>().text = Key.GamepadL1.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadL1)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadL1;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadL1);
                            this.GetComponent<Text>().text = Key.GamepadL1.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button5 + keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadR1.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadR1))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadR1;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadR1);
                    this.GetComponent<Text>().text = Key.GamepadR1.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadR1)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadR1;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadR1);
                            this.GetComponent<Text>().text = Key.GamepadR1.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode) (KeyCode.Joystick1Button6 + keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadBack.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadBack))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadBack;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadBack);
                    this.GetComponent<Text>().text = Key.GamepadBack.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadBack)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadBack;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadBack);
                            this.GetComponent<Text>().text = Key.GamepadBack.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode) (KeyCode.Joystick1Button7 + keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadStart.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadStart))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadStart;
                    PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadStart);
                    this.GetComponent<Text>().text = Key.GamepadStart.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadStart)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + pa.ToString(), (int)Profile.Models[playerNum].keys[action]);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadStart;
                            PlayerPrefs.SetInt("Gamepad" + playerNum + "_" + this.name, (int)Key.GamepadStart);
                            this.GetComponent<Text>().text = Key.GamepadStart.ToString().Substring(7);
                        }
                    }
                }
            }
        }
    }
}
