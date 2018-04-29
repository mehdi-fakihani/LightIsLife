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

        private void Awake()
        {
            trigger = GetComponent<Collider>();
            
        }

        private void Start()
        {
            if (GeneralData.game.deblockedEnemyZones.Contains(gameObject.name))
            {
                trigger.enabled = false;
                soundController.EndFightMusic();

                if (campfire != null)
                    campfire.GetComponent<LightActivator>().ActivateFire();

                Destroy(gameObject);
            }
            else
            {
                if(campfire != null)
                    campfire.GetComponent<LightActivator>().DesactivateFire();

                InitializeSpawner();
            }

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
            if (++nbDead == nbEnemies)
            {
                // if all enemies are spawned and dead, stop fight music
                soundController.EndFightMusic();
                //Debug.Log(campfire.name);
                if (campfire != null)
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    foreach(GameObject player in players)
                    {
                        Debug.Log("Zone " + gameObject.name + " deblocked + 30 current life");
                        GeneralData.game.deblockedEnemyZones.Add(gameObject.name);
                        GeneralData.UpdateCurrentLife(30, player.GetComponent<PlayerController>().getPlayerNum());
                    }
                    
                    campfire.GetComponent<LightActivator>().ActivateFire();
                }
                Destroy(gameObject);
            }
        }

        public bool IsAllSpawned()
        {
            return nbSpawned == nbEnemies;
        }
    }
}
