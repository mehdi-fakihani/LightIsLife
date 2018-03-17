using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SpiritController : MonoBehaviour {
    
    public Transform player;
    public float speed = 2.0f;
    public float distanceToPlayer = 1;          // Distance to keep from player
    public float refreshPathTime = 2.0f;        // Time interval between 2 path calculations
    public float verticalOndulation = 1;        // vertical ondulation movement of the spirit
    
    private NavMeshPath currentPath;
    private int currentPathIndex;
    private float timer;

	// Use this for initialization
	void Start () {
        currentPath = new NavMeshPath();
        UpdatePath();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance > distanceToPlayer)
        {
            if(currentPathIndex < currentPath.corners.Length)
            {
                MoveAlongPath();
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
        Vector3 playerToSpirit = transform.position - player.position;
        Vector3 target = player.position + playerToSpirit.normalized * distanceToPlayer;
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, currentPath);
    }

    private void MoveAlongPath()
    {
        float distanceInOneUpdate = Time.deltaTime * speed;
        float distanceToNextCorner;
        Vector3 target;
        // find next target in path
        do
        {
            target = currentPath.corners[currentPathIndex];
            distanceToNextCorner = Vector3.Distance(target, transform.position);
        } while (distanceToNextCorner < distanceInOneUpdate
              && ++currentPathIndex < currentPath.corners.Length);


        Vector3 direction = target - transform.position;
        if (currentPathIndex < currentPath.corners.Length)
        {
            transform.position += direction.normalized * distanceInOneUpdate;
        }
        else // if last point is too close, slow down to reach destination exactly
        {
            transform.position += direction.normalized * distanceToNextCorner;
        }
    }

    public Transform GetTarget()
    {
        return player;
    }

    public void SetTarget(Transform p)
    {
        player = p;
    }
}
