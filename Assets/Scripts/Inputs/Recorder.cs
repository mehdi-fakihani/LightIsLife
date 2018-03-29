using System;
using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Internal class of the input system.
    /// Used to simulate buttons with axis by tracking their values.
    /// </summary>
    public class Recorder : MonoBehaviour
    {
        private static Recorder instance;

        private readonly Dictionary<string, Status> recordedAxis
            = new Dictionary<string, Status>();

        /// <summary>
        /// Returns the status of the axis button.
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static AxisStatus StatusOf(AxisInfo axis)
        {
            var status = instance.recordedAxis[axis.name];
            return axis.positiveValue ? status.posStatus : status.negStatus;
        }

        /// <summary>
        /// Register an axis to be then tracked by the recorder.
        /// </summary>
        /// <param name="axis"></param>
        public static void RecordAxis(AxisInfo axis)
        {
            if (instance.recordedAxis.ContainsKey(axis.name)) return;
            
            instance.recordedAxis.Add(axis.name, new Status
            {
                posStatus = new AxisStatus(),
                negStatus = new AxisStatus()
            });
        }

        private static void pressAxis(AxisStatus axis)
        {
            axis.isUp = false;
            axis.isDown = !axis.isPressed;
            axis.isPressed = true;
        }
        private static void releaseAxis(AxisStatus axis)
        {
            axis.isUp = axis.isPressed;
            axis.isDown = false;
            axis.isPressed = false;
        }

        void Update()
        {
            foreach (var axis in recordedAxis)
            {
                var posStatus = axis.Value.posStatus;
                var negStatus = axis.Value.negStatus;
                float val = Input.GetAxis(axis.Key);

                if (val > 0)
                {
                    pressAxis(posStatus);
                    releaseAxis(negStatus);
                }
                else if (val < 0)
                {
                    pressAxis(negStatus);
                    releaseAxis(posStatus);
                }
                else // val == 0
                {
                    releaseAxis(posStatus);
                    releaseAxis(negStatus);
                }
            }
        }

        void Awake()
        {
            if (instance == null) instance = this;
        }

        private class Status
        {
            public AxisStatus posStatus;
            public AxisStatus negStatus;
        }
    }
}
