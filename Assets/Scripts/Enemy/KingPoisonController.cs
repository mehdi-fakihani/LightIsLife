using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    public class KingPoisonController : MonoBehaviour
    {
        public float Damage { get;  set; }
        public float DamageActivationTime { get; set; }

        private Collider aoe;

        public static void CreatePoisonAoe(Object prefab, Vector3 pos, Quaternion rot, float damage, float activationTime)
        {
            GameObject obj = Instantiate(prefab, pos, rot) as GameObject;
            KingPoisonController kpc = obj.GetComponent<KingPoisonController>();
            kpc.Damage = damage;
            kpc.DamageActivationTime = activationTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<HealthManager>().harm(Damage);
            }
        }

        // Use this for initialization
        void Start()
        {
            aoe = GetComponent<Collider>();
            aoe.enabled = false;
            StartCoroutine(PoisonLifeCycle());
        }

        private IEnumerator PoisonLifeCycle()
        {
            // let the player see the poison before activation so he can dodge it
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(DamageActivationTime);

            // Activate the damage zone for 1 second
            aoe.enabled = true;
            transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(1);

            // Then destroy poison zone
            Destroy(gameObject);
        }

    }
}

