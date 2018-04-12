using UnityEngine;

namespace LIL
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject warrior;                // The enemy prefab to be spawned.
        public GameObject mage;                   // optional in case we want to spawn mage
        public GameObject support;                // optional in case we want to spawn support
        public GameObject spawnAnimation;
        public float spawnTime = 3f;            // How long between each spawn.
        public int nbEnemies;
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
        public AmbientMusic soundController;

        public GameObject campfire;

        private Collider trigger;
        private int nbSpawned;
        private int nbDead;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Start fight music if not already started
                soundController.StartFightMusic();

                // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
                InvokeRepeating("Spawn", 0, spawnTime);
                trigger.enabled = false;
            }
        }

        private void Start()
        {
            //campfire = transform.parent.gameObject;
            trigger = GetComponent<Collider>();
            InitializeSpawner();
        }

        private void InitializeSpawner()
        {
            // Start game with 0 enemies and spawning inactive
            nbSpawned = 0;
            nbDead = 0;
            trigger.enabled = true;
        }

        private void Spawn()
        {
            ++nbSpawned;

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            ChooseEnemy(spawnPointIndex);
            // Display an animation when an enemy spawn
            Instantiate(spawnAnimation, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            
            // if all enemies have been spawned, stop spawn invoke
            if (IsAllSpawned())
            {
                CancelInvoke();
            }
        }

        private void ChooseEnemy(int spawnPointIndex)
        {
            int enemyType = Random.Range(0, 3);
            switch (enemyType)
            {
                case 0:
                    if(support != null)
                    {
                        SupportController.Create(this, support, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                        return;
                    }
                    break;
                case 1:
                    if(mage != null)
                    {
                        MageController.Create(this, mage, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                        return;
                    }
                    break;
            }
            WarriorController.Create(this, warrior, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }

        public void CountDeath()
        {
            Debug.Log("dead: " + (nbDead + 1) + "; ennemies: " + nbEnemies + "; this" + gameObject);
            if (++nbDead == nbEnemies)
            {
                // if all enemies are spawned and dead, stop fight music
                soundController.EndFightMusic();
                //Debug.Log(campfire.name);
                if (campfire != null)
                {
                    Debug.Log("not null: " + campfire);
                    Debug.Log(gameObject.name);
                    campfire.GetComponent<LightActivator>().ActivateFire();
                }
                Destroy(gameObject); ;
            }
        }

        public bool IsAllSpawned()
        {
            return nbSpawned == nbEnemies;
        }
    }
}
