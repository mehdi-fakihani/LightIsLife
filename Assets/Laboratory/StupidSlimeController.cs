using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Sandbox
{
    public class StupidSlimeController : IActorController
    {
        private Actor slime;
        private float timeElapsed;

        public void register(Actor actor)
        {
            slime = actor;
            timeElapsed = 0f;
        }

        public void update(float secs)
        {
            timeElapsed += secs;

            var transform = slime.gameObject.transform;

            // Sin and cos used to run in circle

            float forward = (float) Math.Cos(timeElapsed);
            float left =    (float) Math.Sin(timeElapsed);
            transform.position += Vector3.forward * forward * slime.speed() * secs;
            transform.position += Vector3.left    * left    * slime.speed() * secs;
        }
    }
}
