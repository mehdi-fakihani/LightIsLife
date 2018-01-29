using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Lists all buttons available for the devices supported.
    /// The gamepad axis must be listed after the threshold value and be handled in AxisInfo.
    /// </summary>
    public enum Key
    {
        // Keyboard buttons

        KeyboardZ = KeyCode.Z,
        KeyboardQ = KeyCode.Q,
        KeyboardS = KeyCode.S,
        KeyboardD = KeyCode.D,
        KeyboardT = KeyCode.T,
        KeyboardY = KeyCode.Y,
        KeyboardU = KeyCode.U,
        KeyboardI = KeyCode.I,
        KeyboardSpace = KeyCode.Space,

        // Gamepad buttons

        GamepadA = KeyCode.Joystick1Button0,
        GamepadB = KeyCode.Joystick1Button1,
        GamepadX = KeyCode.Joystick1Button2,
        GamepadY = KeyCode.Joystick1Button3,

        // Gamepad axis

        GamepadLeftJoystickUp = 1000, // Threshold used in AxisInfo
        GamepadLeftJoystickDown,
        GamepadLeftJoystickLeft,
        GamepadLeftJoystickRight,
        GamepadL2,
        GamepadR2,
    }

}
