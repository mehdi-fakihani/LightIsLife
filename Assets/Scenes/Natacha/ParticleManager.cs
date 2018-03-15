using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    private Dictionary<GameObject, GameObject> particles = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
        particles = new Dictionary<GameObject, GameObject>();
    }
    // Update is called once per frame
    void Update () {
        foreach(KeyValuePair<GameObject, GameObject> particle in particles)
        {
            if (particle.Key == null) particles.Remove(particle.Key);
            else particle.Key.transform.position = particle.Value.transform.position;
        }
	}

    public void addEffect(GameObject particleAnimation, GameObject target)
    {
        GameObject particle = spawnParticle(particleAnimation);
        particle.transform.position = target.transform.position + particle.transform.position;
        particles.Add(particle, target);
    }

    private GameObject spawnParticle(GameObject particleAnimation)
    {
        GameObject particles = (GameObject)Instantiate(particleAnimation);
        particles.transform.position = new Vector3(0, particles.transform.position.y, 0);
#if UNITY_3_5
			particles.SetActiveRecursively(true);
#else
        particles.SetActive(true);
        //			for(int i = 0; i < particles.transform.childCount; i++)
        //				particles.transform.GetChild(i).gameObject.SetActive(true);
#endif

        ParticleSystem ps = particles.GetComponent<ParticleSystem>();

#if UNITY_5_5_OR_NEWER
        if (ps != null)
        {
            var main = ps.main;
            if (main.loop)
            {
                ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
                ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
            }
        }
#else
		if(ps != null && ps.loop)
		{
			ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
			ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
#endif

        return particles;
    }
}
