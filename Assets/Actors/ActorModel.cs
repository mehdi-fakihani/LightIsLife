using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Store caracteristics to represents any actor of the game.
    /// </summary>
    [System.Serializable]
    public class ActorModel : MonoBehaviour
    {
        [SerializeField] private ActorsID id;
        [SerializeField] private GameObject prefab;
        [SerializeField] public Faction faction = Faction.Neutral;
        [SerializeField] public int life = 100;
        [SerializeField] public float speed = 100f;
        [SerializeField] public SkillsID[] skills = new SkillsID[0];

        /// <summary>
        /// Creates an actor. TODO GameObject
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public GameObject create(Vector3 position, Quaternion rotation, IActorController controller)
        {
            var obj = Instantiate(prefab, position, rotation);
            var actor = obj.GetComponent<Actor>();
            Assert.IsNotNull(actor, "The prefab created from an actor model" +
                                    " must have an 'Actor' script component");
            // Initialize the actor
            actor.model = this;
            actor.controller = controller;
            actor.currentLife = life;
            actor.speedRatio = 1f;
            actor.immobilityTokens = 0;
            actor.silenceTokens = 0;
            actor.isDead = false;
            foreach (var skill in skills)
            {
                var model = Skill.Models[skill];
                actor.skills.Add(model.create(actor));
            }
            controller.register(actor);
            return obj;
        }

        void Awake()
        {
            Actor.Models.Add(id, this);
        }
    }
}
