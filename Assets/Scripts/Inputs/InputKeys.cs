﻿using System;
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

        KeyboardA = KeyCode.A,
        KeyboardB = KeyCode.B,
        KeyboardC = KeyCode.C,
        KeyboardD = KeyCode.D,
        KeyboardE = KeyCode.E,
        KeyboardF = KeyCode.F,
        KeyboardG = KeyCode.G,
        KeyboardH = KeyCode.H,
        KeyboardI = KeyCode.I,
        KeyboardJ = KeyCode.J,
        KeyboardK = KeyCode.K,
        KeyboardL = KeyCode.L,
        KeyboardM = KeyCode.M,
        KeyboardN = KeyCode.N,
        KeyboardO = KeyCode.O,
        KeyboardP = KeyCode.P,
        KeyboardQ = KeyCode.Q,
        KeyboardR = KeyCode.R,
        KeyboardS = KeyCode.S,
        KeyboardT = KeyCode.T,
        KeyboardU = KeyCode.U,
        KeyboardV = KeyCode.V,
        KeyboardW = KeyCode.W,
        KeyboardX = KeyCode.X,
        KeyboardY = KeyCode.Y,
        KeyboardZ = KeyCode.Z,

        Keyboard0 = KeyCode.Alpha0,
        Keyboard1 = KeyCode.Alpha1,
        Keyboard2 = KeyCode.Alpha2,
        Keyboard3 = KeyCode.Alpha3,
        Keyboard4 = KeyCode.Alpha4,
        Keyboard5 = KeyCode.Alpha5,
        Keyboard6 = KeyCode.Alpha6,
        Keyboard7 = KeyCode.Alpha7,
        Keyboard8 = KeyCode.Alpha8,
        Keyboard9 = KeyCode.Alpha9,

        KeyboardNUM0 = KeyCode.Keypad0,
        KeyboardNUM1 = KeyCode.Keypad1,
        KeyboardNUM2 = KeyCode.Keypad2,
        KeyboardNUM3 = KeyCode.Keypad3,
        KeyboardNUM4 = KeyCode.Keypad4,
        KeyboardNUM5 = KeyCode.Keypad5,
        KeyboardNUM6 = KeyCode.Keypad6,
        KeyboardNUM7 = KeyCode.Keypad7,
        KeyboardNUM8 = KeyCode.Keypad8,
        KeyboardNUM9 = KeyCode.Keypad9,
        KeyboardNUMEnter = KeyCode.KeypadEnter,

        KeyboardESC = KeyCode.Escape,
        

        KeyboardSpace = KeyCode.Space,
        KeyboardUp = KeyCode.UpArrow,
        KeyboardDown = KeyCode.DownArrow,
        KeyboardLeft = KeyCode.LeftArrow,
        KeyboardRight = KeyCode.RightArrow,
        KeyboardReturn = KeyCode.Return,
        KeyboardLeftShift = KeyCode.LeftShift,
        KeyboardRightShift = KeyCode.RightShift,
        KeyboardLeftCtrl = KeyCode.LeftControl,
        KeyboardRightCtrl = KeyCode.RightControl,

        // Gamepad buttons

        GamepadA = KeyCode.Joystick1Button0,
        GamepadB = KeyCode.Joystick1Button1,
        GamepadX = KeyCode.Joystick1Button2,
        GamepadY = KeyCode.Joystick1Button3,

        GamepadL1 = KeyCode.Joystick1Button4,
        GamepadR1 = KeyCode.Joystick1Button5,
        GamepadBack = KeyCode.Joystick1Button6,
        GamepadStart = KeyCode.Joystick1Button7,
        GamepadLeftJoystick = KeyCode.Joystick1Button8,
        GamepadRightJoystick = KeyCode.Joystick1Button9,
       
        // Gamepad axis

        GamepadLeftJoystickUp = 1000, // Threshold used in AxisInfo
        GamepadLeftJoystickDown,
        GamepadLeftJoystickLeft,
        GamepadLeftJoystickRight,

        GamepadRightJoystickUp,
        GamepadRightJoystickDown,
        GamepadRightJoystickLeft,
        GamepadRightJoystickRight,

        GamepadPadUp,
        GamepadPadDown,
        GamepadPadLeft,
        GamepadPadRight,

        GamepadL2,
        GamepadR2,
    }

}
