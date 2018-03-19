using UnityEngine;

namespace LIL
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject warrior;                // The enemy prefab to be spawned.
        public GameObject mage;
        public GameObject support;
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
        public AmbientMusic soundController;

        private int enemyCount;

        void Start()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating("Spawn", spawnTime, spawnTime);

            // Start game with 0 enemies
            enemyCount = 0;
        }

        void Spawn()
        {
            // Start fight music if not already started
            soundController.StartFightMusic();

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(ChooseEnemy(), spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            enemyCount++;
        }

        private GameObject ChooseEnemy()
        {
            int enemyType = Random.Range(0, 3);
            switch (enemyType)
            {
                case 0:
                    return support;
                case 1:
                    return mage;
                default:
                    return warrior;
            }
        }

        public void CountDeath()
        {
            if(--enemyCount == 0)
            {
                soundController.EndFightMusic();
            }
        }
    }
}
