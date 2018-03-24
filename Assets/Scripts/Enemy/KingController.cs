using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace LIL
{
    public class KingController : MonoBehaviour
    {
        
        public GameObject poisonPrefab;
        public float poisonAoeRange = 10f;
        public float poisonDamage = 10f;
        public float poisonActivationTime = 1.5f;
        public float poisonFrequence = 2f;           // make poison spawn every x seconds

        public GameObject invocation;
        public float invocationRange = 2f;
        public int nbInvocations = 4;


        private void ActivatePoisonAoe()
        {
            InvokeRepeating("SpawnPoisonAoe", 0, poisonFrequence);
        }

        private void InvocationSpell()
        {
            for (int i = 0; i < nbInvocations; i++)
            {
                Instantiate(invocation, GetRandomPosInRange(invocationRange), Quaternion.Euler(0, 0, 0));
            }
        }

        // Use this for initialization
        void Start()
        {
            ActivatePoisonAoe();
            InvocationSpell();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void SpawnPoisonAoe()
        {
            KingPoisonController.CreatePoisonAoe(poisonPrefab, GetRandomPosInRange(poisonAoeRange),
                                    Quaternion.Euler(0, 0, 0), poisonDamage, poisonActivationTime);
        }

        private Vector3 GetRandomPosInRange(float range)
        {
            float dx = Random.Range(-range, range + 1);
            float dz = Random.Range(-range, range + 1);
            Vector3 position = new Vector3(transform.position.x + dx,
                                           transform.position.y + 0.5f,
                                           transform.position.z + dz);
            return position;
        }
    }
}

