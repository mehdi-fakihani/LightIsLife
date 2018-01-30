using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Internal class of the input system.
    /// Store static methods to determine a key's category.
    /// </summary>
    public class KeyGroup
    {
        public static Device DeviceOf(Key key)
        {
            return (int) key < (int) KeyCode.Joystick1Button0
                ? Device.Keyboard
                : Device.XBoxGamepad;
        }
        public static bool IsFromAxis(Key key)
        {
            return (int) key >= 1000;
        }

    }
}
