using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LIL.Inputs
{
    /// <summary>
    /// Internal class of the input system.
    /// Translate gamepad axis keys into an axis name and it's direction.
    /// The name can be used to query the axis status with Inputs.Recorder.StatusOf(axisName).
    /// </summary>
    public class AxisInfo // TODO Gamepad axis (here and in Unity)
    {
        public string name;
        public bool positiveValue;

        private AxisInfo() { }
        
        /// <summary>
        /// Retrieves axis info from a key and the device's number used.
        /// This function must be updated when new axes are added in Unity.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="deviceNum"></param>
        /// <returns></returns>
        public static AxisInfo From(Key key, int deviceNum)
        {

            var info = new AxisInfo();
            info.name = "Gamepad" + (deviceNum + 1);

            switch (key)
            {
                case Key.GamepadLeftJoystickUp:
                    info.name += "LeftJoystickUp";
                    info.positiveValue = true;
                    break;
                case Key.GamepadLeftJoystickDown:
                    info.name += "LeftJoystickUp";
                    info.positiveValue = false;
                    break;
                case Key.GamepadLeftJoystickLeft:
                    info.name += "LeftJoystickLeft";
                    info.positiveValue = true;
                    break;
                case Key.GamepadLeftJoystickRight:
                    info.name += "LeftJoystickLeft";
                    info.positiveValue = false;
                    break;

                case Key.GamepadRightJoystickUp:
                    info.name += "RightJoystickUp";
                    info.positiveValue = true;
                    break;
                case Key.GamepadRightJoystickDown:
                    info.name += "RightJoystickUp";
                    info.positiveValue = false;
                    break;
                case Key.GamepadRightJoystickLeft:
                    info.name += "RightJoystickLeft";
                    info.positiveValue = true;
                    break;
                case Key.GamepadRightJoystickRight:
                    info.name += "RightJoystickLeft";
                    info.positiveValue = false;
                    break;

                case Key.GamepadPadUp:
                    info.name += "PadUp";
                    info.positiveValue = true;
                    break;
                case Key.GamepadPadDown:
                    info.name += "PadUp";
                    info.positiveValue = false;
                    break;
                case Key.GamepadPadLeft:
                    info.name += "PadLeft";
                    info.positiveValue = true;
                    break;
                case Key.GamepadPadRight:
                    info.name += "PadLeft";
                    info.positiveValue = false;
                    break;

                case Key.GamepadL2:
                    info.name += "Triggers";
                    info.positiveValue = true;
                    break;
                case Key.GamepadR2:
                    info.name += "Triggers";
                    info.positiveValue = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("key", key, null);
            }
            return info;
        }
    }
}
