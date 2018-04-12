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
                                    GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(8);
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
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadA;
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
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadB;
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
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadX;
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
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadY;
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
                            Debug.Log("action : " + pa.ToString());
                            Debug.Log("name : " + GetKeyGameObject(pa.ToString()).name);
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadL1;
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
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadR1;
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
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadBack;
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
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadStart;
                            this.GetComponent<Text>().text = Key.GamepadStart.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button8 + keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadLeftJoystick.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadLeftJoystick))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadLeftJoystick;
                    this.GetComponent<Text>().text = Key.GamepadLeftJoystick.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadLeftJoystick)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadLeftJoystick;
                            this.GetComponent<Text>().text = Key.GamepadLeftJoystick.ToString().Substring(7);
                        }
                    }
                }
            }
            if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button9+keyShift)))
            {
                PlayerAction action = (PlayerAction)Enum.Parse(typeof(PlayerAction), this.name);
                Debug.Log("key pressed : " + Key.GamepadRightJoystick.ToString());
                if (!Profile.Models[playerNum].keys.ContainsValue(Key.GamepadRightJoystick))
                {
                    Profile.Models[playerNum].keys[action] = Key.GamepadRightJoystick;
                    this.GetComponent<Text>().text = Key.GamepadRightJoystick.ToString().Substring(7);
                }
                else
                {
                    List<PlayerAction> playerActions = new List<PlayerAction>(Profile.Models[playerNum].keys.Keys);

                    foreach (PlayerAction pa in playerActions)
                    {
                        if ((int)Profile.Models[playerNum].keys[pa] == (int)Key.GamepadRightJoystick)
                        {
                            Profile.Models[playerNum].keys[pa] = Profile.Models[playerNum].keys[action];
                            GetKeyGameObject(pa.ToString()).GetComponent<Text>().text = Profile.Models[playerNum].keys[pa].ToString().Substring(7);
                            Profile.Models[playerNum].keys[action] = Key.GamepadRightJoystick;
                            this.GetComponent<Text>().text = Key.GamepadRightJoystick.ToString().Substring(7);
                        }
                    }
                }
            }
        }
    }
}
