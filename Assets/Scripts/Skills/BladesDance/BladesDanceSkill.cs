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
        [SerializeField] private float damages;
        [SerializeField] private float range;
        [SerializeField] private float castTime;
        [SerializeField] private float impactWidth;
        [SerializeField] private AudioClip castSound;

        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;

            if (castSound != null)
            {
                var audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound, 0.3f);
            }
            
            caster.GetComponent<EffectManager>().addEffect(new Effects.Slow(castTime, 0.9f));
            caster.GetComponent<EffectManager>().addEffect(new Effects.Silence(castTime));
            caster.GetComponent<EffectManager>().addEffect(new Effects.Delayed(castTime, () =>
            {
                var destination = caster.transform.position + caster.transform.forward * range;

                var hits = Physics.SphereCastAll(
                    caster.transform.position + new Vector3(0, 1.5f, 0),
                    impactWidth,
                    caster.transform.forward,
                    range);
                
                var decors = new List<RaycastHit>();
                var actors = new List<GameObject>();
                foreach (var hit in hits)
                {
                    var obj = hit.transform.gameObject;

                    if (obj == caster) continue;
                    if (obj.transform.IsChildOf(caster.transform)) continue;

                    if (obj.isNeutral())
                        decors.Add(hit);
                    else
                        actors.Add(obj);
                }

                // Caster's destination is at the spell max range
                if (decors.Count == 0 && actors.Count == 0) 
                {
                    caster.transform.position = destination;
                    return;
                }
                // Caster's destination is before touching the closest decor
                if (decors.Count != 0) 
                {
                    var impact = destination;
                    float distance = (caster.transform.position - impact).magnitude;
                    Debug.Log("hit " + decors.Count + " decors");
                    foreach (var decor in decors)
                    {
                        float newDistance = (caster.transform.position - decor.point).magnitude;
                        if (newDistance < distance)
                        {
                            distance = newDistance;
                            impact = decor.point;
                        }
                    }
                    destination = impact - caster.transform.forward * 1.5f;
                }
                // Caster's destination is behind the farthest actor
                else
                {
                    var impact = caster.transform.position;
                    float distance = (caster.transform.position - impact).magnitude;
                    foreach (var actor in actors)
                    {
                        float newDistance = (caster.transform.position - actor.transform.position).magnitude;
                        if (newDistance > distance)
                        {
                            distance = newDistance;
                            impact = actor.transform.position;
                        }
                    }
                    var newDestination = impact + caster.transform.forward * 1.5f;
                    destination = newDestination.magnitude > destination.magnitude
                        ? newDestination
                        : destination;
                }

                destination.y = caster.transform.position.y;
                caster.transform.position = destination;

                foreach (var actor in actors.Where(actor => actor.isEnemyWith(caster)))
                {
                    var health = actor.GetComponent<HealthManager>();
                    if (health) health.harm(damages);
                }
            }));
        }
    }
}
