using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace LIL
{
    public class KingController : MonoBehaviour
    {
        public float speed = 2.0f;

        public float attackRange = 2f;
        public float chargeRange = 12f;

        public AudioClip hurtSound;
        public AudioClip deathSound;
        public AudioClip appearanceSound;

        public GameObject poisonPrefab;
        public AudioClip poisonSound;
        public float poisonAoeRange = 10f;
        public float poisonDamage = 10f;
        public float poisonActivationTime = 1.5f;
        public float poisonFrequence = 2f;           // make poison spawn every x seconds

        public GameObject invocation;
        public AudioClip invocSound;
        public float invocationRange = 2f;
        public int nbEnemiesToInvoc = 4;
        public int nbInvocations = 4;               // boss will use InvocationSpell [nbInvocations] time 

        private AudioSource source;
        private GameObject[] players;
        private WarriorAttack attacker;
        private Skill charge;
        private ChargeSkill chargeModel;
        private Animator animator;
        private HealthManager health;
        private int healthRatioId;
        private Transform target;
        private NavMeshPath path;
        private int pathIndex;
        

        private void ActivatePoisonAoe()
        {
            InvokeRepeating("SpawnPoisonAoe", 0, poisonFrequence);
        }

        private void InvocationSpell()
        {
            source.PlayOneShot(invocSound);
            for (int i = 0; i < nbEnemiesToInvoc; i++)
            {
                Instantiate(invocation, GetRandomPosInRange(invocationRange), Quaternion.Euler(0, 0, 0));
            }
        }

        // Use this for initialization
        void Start()
        {
            source = GetComponent<AudioSource>();
            path = new NavMeshPath();
            players = GameObject.FindGameObjectsWithTag("Player");
            attacker = GetComponent<WarriorAttack>();
            animator = GetComponent<Animator>();
            GetComponent<SphereCollider>().radius = attackRange;
            charge = GetComponent<SkillManager>().getSkill(SkillsID.Charge);
            chargeModel = charge.model as ChargeSkill;
            chargeModel.range = chargeRange;

            ActivatePoisonAoe();

            // Set hurt and death reactions
            HealthManager health = GetComponent<HealthManager>();
            healthRatioId = nbInvocations;
            health.setHurtCallback(() =>
            {
                animator.SetTrigger("hurt");
                source.PlayOneShot(hurtSound);
                if(health.getLife() <= healthRatioId * health.getInitialLife() / (nbInvocations + 1))
                {
                    InvocationSpell();
                    --healthRatioId;
                }
            });
            health.setDeathCallback(() =>
            {
                animator.SetTrigger("death");
                source.PlayOneShot(deathSound);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
                Time.timeScale = 1f;
                Destroy(gameObject, 1.5f);
            });
        }

        // Update is called once per frame
        void Update()
        {
            //check if not dead
            if (!GetComponent<HealthManager>().isAlive()) return;

            FindTarget();
            transform.LookAt(target.position);
            float distance = Vector3.Distance(transform.position, target.position) - attackRange;
            if(distance > chargeRange * 0.8f)
            {
                if (distance > chargeRange * 0.5f)
                {
                    // try to charge to the target if it is far enough
                    chargeModel.range = distance;
                    if (charge.tryCast())
                    {
                        return;
                    }
                }
            }
            if(!attacker.IsPlayerInRange())
            {
                MoveToTarget();
            }
        }
        
        private void FindTarget()
        {
            float minDistance = float.MaxValue;
            float distance;
            foreach(GameObject p in players)
            {
                distance = Vector3.Distance(transform.position, p.transform.position);
                if (distance < minDistance)
                {
                    target = p.transform;
                    minDistance = distance;
                }
            }
        }

        private void MoveToTarget()
        {
            pathIndex = 0;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            transform.position = Navigator.MoveAlongPath(transform.position, speed, path, ref pathIndex);
        }

        private void SpawnPoisonAoe()
        {
            source.PlayOneShot(poisonSound);
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

        public void SetTarget(Transform t)
        {
            target = t;
        }
    }
}

