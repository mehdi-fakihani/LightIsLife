using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace LIL
{
    public class SpiritController : MonoBehaviour
    {

        public GameObject player;
        public float speed = 2.0f;
        public float distanceToPlayer = 1;          // Distance to keep from player
        public float refreshPathTime = 2.0f;        // Time interval between 2 path calculations

        private NavMeshPath currentPath;
        private int currentPathIndex;
        private float timer;

        // Use this for initialization
        void Start()
        {
            float[] pos = GeneralData.getPlayerbyNum(player.GetComponent<PlayerController>().getPlayerNum()).pos;
            this.transform.position = new Vector3(pos[0]+2, pos[1], pos[2]);
            currentPath = new NavMeshPath();
            UpdatePath();
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance > distanceToPlayer)
            {
                if (currentPathIndex < currentPath.corners.Length)
                {
                    transform.position = Navigator.MoveAlongPath(transform.position, speed, currentPath, ref currentPathIndex);
                }
                if (timer > refreshPathTime)
                {
                    UpdatePath();
                }
            }
        }

        private void UpdatePath()
        {
            timer = 0;
            currentPathIndex = 0;
            Vector3 playerToSpirit = transform.position - player.transform.position;
            Vector3 target = player.transform.position + playerToSpirit.normalized * distanceToPlayer;
            NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, currentPath);
        }

        public Transform GetTarget()
        {
            return player.transform;
        }

        public void SetTarget(GameObject p)
        {
            player = p;
        }
    }
}

