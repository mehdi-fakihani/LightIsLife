using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// The class representing a player or an ennemy.
    /// It can be affected by effets and cast spells.
    /// Controllers are used to make them active.
    /// </summary>
    public class Actor : MonoBehaviour
    {
        public static readonly Dictionary<ActorsID, ActorModel> Models
            = new Dictionary<ActorsID, ActorModel>();

        [NonSerialized] public ActorModel model;
        [NonSerialized] public IActorController controller;
        
        [NonSerialized] public List<Skill> skills = new List<Skill>();
        [NonSerialized] public List<IEffect> effects = new List<IEffect>();

        // Caracteristics
        [NonSerialized] public int currentLife;
        [NonSerialized] public float speedRatio;
        [NonSerialized] public int immobilityTokens;
        [NonSerialized] public int silenceTokens;
        [NonSerialized] public bool isDead;

        /// <summary>
        /// Add a new effect to the actor.
        /// </summary>
        /// <param name="effect"></param>
        public void addEffect(IEffect effect)
        {
            effect.initialize(this);
            effects.Add(effect);
        }

        /// <summary>
        /// Returns the current actor's speed.
        /// </summary>
        /// <returns></returns>
        public float speed() { return speedRatio * model.speed; }

        /// <summary>
        /// Indicates if the actor can move.
        /// </summary>
        /// <returns></returns>
        public bool canMove() { return immobilityTokens == 0; }

        /// <summary>
        /// Indicates if the actor can cast a spell.
        /// </summary>
        /// <returns></returns>
        public bool canCast() { return silenceTokens == 0; }

        /// <summary>
        /// Returns the actor's faction.
        /// </summary>
        /// <returns></returns>
        public Faction faction() { return model.faction; }
        
        void Update()
        {
            if (isDead) return;

            float secs = Time.deltaTime;

            // Update skills
            foreach (var skill in skills)
            {
                skill.update(secs);
            }
            // Update effects (remove those which are finished)
            var endedEffects = new List<IEffect>();
            foreach (var effect in effects)
            {
                effect.update(secs);
                if (effect.isFinished()) endedEffects.Add(effect);
            }
            foreach (var effect in endedEffects)
            {
                effect.destroy();
                effects.Remove(effect);
            }
            // Update controller
            controller.update(secs);
        }
    }
}