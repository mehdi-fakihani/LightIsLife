using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Sandbox
{
    public class SceneScript : MonoBehaviour
    {
        void Start()
        {
            // Create player

            Actor.Models[ActorsID.Hero].create(
                new Vector3(0, 2, 0),
                Quaternion.identity,
                new SimplePlayerController()
            );

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
