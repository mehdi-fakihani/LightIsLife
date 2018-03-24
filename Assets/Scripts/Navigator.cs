using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


namespace LIL
{
    // public class to store navigation methods
    public static class Navigator
    {     
        /// <summary>
        /// Moves from position along a NavMeshPath with a given speed.
        /// Returns the next position and actualise pathIndex.
        /// </summary>
        public static Vector3 MoveAlongPath(Vector3 position, float speed, NavMeshPath path, ref int pathIndex)
        {
            float distanceInOneUpdate = Time.deltaTime * speed;
            float travelledDistance = 0;
            Vector3 target = path.corners[pathIndex];

            // find next target in path
            while (travelledDistance + Vector3.Distance(position, target) < distanceInOneUpdate
                && pathIndex + 1 < path.corners.Length)
            {
                travelledDistance += Vector3.Distance(position, target);
                position = target;
                target = path.corners[++pathIndex];
            }
            
            Vector3 direction = target - position;
            return position + direction.normalized * (distanceInOneUpdate - travelledDistance);
        }
    }
}

