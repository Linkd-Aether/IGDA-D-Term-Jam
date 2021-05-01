using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Game.Movement;
using Game.Lighting;

namespace Game.AI 
{
    [RequireComponent(typeof(Seeker)), RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Mover))]
    public class EnemyAI : MonoBehaviour
    {
        // Constants
        private enum State { PATROL, CHASE, PAUSE };
        private float MAX_CHASE_DIST = 10f;
        private float MAX_VISION_DIST = 10f;

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

        private static Transform player;


        private void Awake() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

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
            if (state != State.PAUSE) {
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
            // Player has been caught, temporary PAUSE before resuming PATROl
            public IEnumerator KilledPlayer(float delay) 
            {
                mover.UpdateMovement(Vector2.zero);
                state = State.PAUSE;
                yield return new WaitForSeconds(delay);
                StateToPatrol();
            }

            // Change to default PATROL state
            private void StateToPatrol() 
            {
                CancelInvoke();
                state = State.PATROL;
                PatrolNextTarget();
                InvokeRepeating("PlayerSearch", 0f, 0.5f);
            }

            // Search for player, start CHASE if found
            private void PlayerSearch() {
                Lantern lantern = player.GetComponentInChildren<Lantern>();

                if (lantern.isLit) {
                    Vector2 rayDirection = player.position - transform.position;
                    
                    RaycastHit2D hit2D = Physics2D.Raycast(transform.position, rayDirection, MAX_VISION_DIST);
                    if (hit2D && hit2D.collider.transform == player) {
                        StateToChase(player);
                    }
                }
            }

            // Change to pursuit CHASE state
            private void StateToChase(Transform chaseTarget)
            {
                CancelInvoke();
                state = State.CHASE;
                target = chaseTarget;
                InvokeRepeating("UpdatePath", 0f, 0.5f);
            }
        #endregion

        #region Gizmos
        private void OnDrawGizmos() {
            Awake();
            // PlayerSearch Ray
            Vector2 rayDirection = (player.position - transform.position).normalized;
            Gizmos.DrawLine(transform.position, (Vector2) transform.position + rayDirection * MAX_VISION_DIST);
        }
        #endregion
    }
}
