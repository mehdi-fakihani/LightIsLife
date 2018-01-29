using System.Collections;
using System.Collections.Generic;
using LIL.Inputs;
using UnityEngine;

namespace LIL.Sandbox
{
    public class SceneScript : MonoBehaviour
    {
        void Start()
        {
            // Create player profile

            var keyboardProfile = Profile.Models[ProfilsID.KeyboardLeft].create(0);
            //var gamepadProfile1 = Profile.Models[ProfilsID.XBoxGamepad].create(0);
            //var gamepadProfile2 = Profile.Models[ProfilsID.XBoxGamepad].create(1);

            // Create players

            Actor.Models[ActorsID.Hero].create(
                new Vector3(0, 2, 0),
                Quaternion.identity,
                new SimplePlayerController(keyboardProfile)
            );/*
            Actor.Models[ActorsID.Hero].create(
                new Vector3(0, 2,-2),
                Quaternion.identity,
                new SimplePlayerController(gamepadProfile1)
            );
            Actor.Models[ActorsID.Hero].create(
                new Vector3(0, 2, 2),
                Quaternion.identity,
                new SimplePlayerController(gamepadProfile2)
            );*/

            // Create slimes

            Actor.Models[ActorsID.Slime].create(
                new Vector3(10, 2, 0),
                Quaternion.identity,
                new StupidSlimeController()
            );
            Actor.Models[ActorsID.Slime].create(
                new Vector3(-6, 2, 7),
                Quaternion.identity,
                new StupidSlimeController()
            );
            Actor.Models[ActorsID.Slime].create(
                new Vector3(-6, 2, -7),
                Quaternion.identity,
                new StupidSlimeController()
            );
        }
    }
}
