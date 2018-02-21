using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Model for the blades dance skill.
    /// It moves instantanly the caster off a short distance, causing damages to the ennemies
    /// traversed.
    /// </summary>
    public class BladesDanceSkill : ISkillModel
    {
        [SerializeField] private int damages;
        [SerializeField] private float range;
        [SerializeField] private float castTime;
        [SerializeField] private float impactWidth;
        [SerializeField] private AudioClip castSound;

        public override void cast(SkillManager manager)
        {
            var player = manager.gameObject;

            if (castSound != null)
            {
                var audioSource = player.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound, 0.3f);
            }
            
            player.GetComponent<EffectManager>().addEffect(new Effects.Slow(castTime, 0.9f));
            player.GetComponent<EffectManager>().addEffect(new Effects.Silence(castTime));
            player.GetComponent<EffectManager>().addEffect(new Effects.Delayed(castTime, () =>
            {
                var destination = player.transform.position + player.transform.forward * range;

                var hits = Physics.SphereCastAll(
                    player.transform.position + new Vector3(0, 1.5f, 0),
                    impactWidth,
                    player.transform.forward,
                    range);
                
                var decors = new List<RaycastHit>();
                var actors = new List<GameObject>();
                foreach (var hit in hits)
                {
                    var obj = hit.transform.gameObject;

                    if (obj == player) continue;
                    if (obj.transform.IsChildOf(player.transform)) continue;

                    if (obj.CompareTag("Player") || obj.CompareTag("Enemy"))
                         actors.Add(obj);
                    else decors.Add(hit);
                }

                // Player's destination is at the spell max range
                if (decors.Count == 0 && actors.Count == 0) 
                {
                    player.transform.position = destination;
                    return;
                }
                // Players's destination is before touching the closest decor
                if (decors.Count != 0) 
                {
                    var impact = destination;
                    float distance = (player.transform.position - impact).magnitude;
                    Debug.Log("hit " + decors.Count + " decors");
                    foreach (var decor in decors)
                    {
                        float newDistance = (player.transform.position - decor.point).magnitude;
                        if (newDistance < distance)
                        {
                            distance = newDistance;
                            impact = decor.point;
                        }
                    }
                    destination = impact - player.transform.forward * 1.5f;
                }
                // Player's destination is behind the farthest actor
                else
                {
                    var impact = player.transform.position;
                    float distance = (player.transform.position - impact).magnitude;
                    foreach (var actor in actors)
                    {
                        float newDistance = (player.transform.position - actor.transform.position).magnitude;
                        if (newDistance > distance)
                        {
                            distance = newDistance;
                            impact = actor.transform.position;
                        }
                    }
                    var newDestination = impact + player.transform.forward * 1.5f;
                    destination = newDestination.magnitude > destination.magnitude
                        ? newDestination
                        : destination;
                }

                destination.y = player.transform.position.y;
                player.transform.position = destination;

                foreach (var actor in actors.Where(actor => actor.CompareTag("Enemy")))
                {
                    var health = actor.GetComponent<HealthEnemy>();
                    if (health) health.takeDammage(damages);
                }
            }));
        }
    }
}
