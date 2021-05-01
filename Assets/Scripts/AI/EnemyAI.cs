using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Game.Movement;

namespace Game.AI 
{
    [RequireComponent(typeof(Seeker)), RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Mover))]
    public class EnemyAI : MonoBehaviour
    {
        // Constants
        private enum State { PATROL, CHASE };
        private float MAX_CHASE_DIST = 10f;

        // Variables
        private int currentWaypoint = 0;
        public float nextWaypointDistance = 2f;

        private State state = State.PATROL;

        // Components & References
        private Seeker seeker;
        private Path path;
        private Rigidbody2D rb;
        private Mover mover;

        private EnemyPath patrolPath;
        private Transform target;


        private void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            mover = GetComponent<Mover>();

            patrolPath = transform.parent.GetComponentInChildren<EnemyPath>();

            StateToPatrol();
        }

        private void FixedUpdate()
        {
            if (state == State.CHASE) {
                if (Vector2.Distance(target.transform.position, transform.position) > MAX_CHASE_DIST){
                    StateToPatrol();
                }
            }

            if (path != null) {
                if (currentWaypoint >= path.vectorPath.Count) {
                    if (state == State.PATROL) {
                        PatrolNextTarget();
                    }
                    return;
                }

                Vector2 dir = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
                mover.UpdateMovement(dir);

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                if (distance < nextWaypointDistance) {
                    currentWaypoint++;
                }
            }
        }

        #region Update Path
            // Moves patrolling enemies to next target
            private void PatrolNextTarget() 
            {
                target = patrolPath.NextNode();
                UpdatePath();
            }
            
            // Update the path in case the object has moved in Combat State
            private void UpdatePath() 
            {
                if (seeker.IsDone()) {
                    seeker.StartPath(rb.position, target.position, PathFindComplete);
                }
            }

            // Path has been generated to target
            private void PathFindComplete(Path p) 
            {
                if (!p.error) {
                    path = p;
                    currentWaypoint = 0;
                }
            }
        #endregion

        #region State Change
            // Change to default PATROL state
            private void StateToPatrol() 
            {
                CancelInvoke();
                state = State.PATROL;
                PatrolNextTarget();
            }

            // Change to pursuit CHASE state
            private void StateToChase(Transform chaseTarget)
            {
                state = State.CHASE;
                target = chaseTarget;
                InvokeRepeating("UpdatePath", 0f, 0.5f);
            }
        #endregion

    }
}
