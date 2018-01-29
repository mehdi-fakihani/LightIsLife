using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Internal class of the input system.
    /// It is used to track axis status by the recorder.
    /// </summary>
    public class AxisStatus
    {
        public bool isPressed;
        public bool isUp;
        public bool isDown;

        public AxisStatus()
        {
            isPressed = false;
            isUp = false;
            isDown = false;
        }
    }
}
