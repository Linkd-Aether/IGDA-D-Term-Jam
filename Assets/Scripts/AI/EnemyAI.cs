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

        private float PLAYER_CHASE_SEARCH_CALL_FREQ = .25f;

        // Variables
        private int currentWaypoint = 0;
        private float nextWaypointDistance = 2f;
        private float timeSinceLastSeenPlayer = 0f;
        private Vector2 startChase;

        private State state = State.PATROL;

        [Header("Max Distance To Chase")]
        [Tooltip("Enable a max distance the enemy will Chase a target.")] 
        public bool maxDistEnabled = true;
        [Tooltip("The max distance the enemy will pursue a target from where it begins a Chase.")] 
        public float maxChaseDist = 50f;

        [Header("Other Settings")]
        [Tooltip("The max distance an enemy can see a target from.")] 
        public float maxVisionDist = 10f;
        [Tooltip("The max distance from a target in a Chase before the enemy returns to its Patrol.")] 
        public float maxDistFromTarget = 10f;
        [Tooltip("The max amount of time the player will pursue an enemy that it cannot directly see.")] 
        public float maxSearchTime = 3f;
        [Tooltip("Length of time pausing before return to Patrol.")] 
        public float pauseTime = 2f;

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
                    if (Vector2.Distance(target.transform.position, transform.position) > maxDistFromTarget){
                        StartCoroutine(StateToPause(pauseTime));
                        return;
                    } else if (maxDistEnabled && Vector2.Distance(transform.position, startChase) > maxChaseDist) {
                        StartCoroutine(StateToPause(pauseTime));
                        return;
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
            // Temporary PAUSE before resuming PATROL
            public IEnumerator StateToPause(float delay) 
            {
                CancelInvoke();
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
                InvokeRepeating("PlayerPatrolSearch", 0f, 0.5f);
            }

            // Search for player, start CHASE if found
            private void PlayerPatrolSearch() {
                Lantern lantern = player.GetComponentInChildren<Lantern>();

                if (lantern.isLit) {
                    Vector2 rayDirection = player.position - transform.position;
                    
                    RaycastHit2D hit2D = Physics2D.Raycast(transform.position, rayDirection, maxVisionDist);
                    if (hit2D && hit2D.collider.transform == player) {
                        StateToChase(player);
                    }
                }
            }

            // Search for player, leave CHASE if not found for long enough time
            private void PlayerChaseSearch() {
                Vector2 rayDirection = player.position - transform.position;
                
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, rayDirection, maxVisionDist);
                if (hit2D && hit2D.collider.transform == player) timeSinceLastSeenPlayer = 0;
                else timeSinceLastSeenPlayer += PLAYER_CHASE_SEARCH_CALL_FREQ;

                if (timeSinceLastSeenPlayer > maxSearchTime) {
                    StartCoroutine(StateToPause(pauseTime));
                }
            }

            // Change to pursuit CHASE state
            private void StateToChase(Transform chaseTarget)
            {
                CancelInvoke();
                state = State.CHASE;
                target = chaseTarget;
                startChase = transform.position;
                InvokeRepeating("UpdatePath", 0f, 0.5f);

                timeSinceLastSeenPlayer = 0;
                InvokeRepeating("PlayerChaseSearch", 0f, PLAYER_CHASE_SEARCH_CALL_FREQ);
            }
        #endregion

        #region Gizmos
            private void OnDrawGizmos() {
                Awake();
                // PlayerSearch Ray
                Vector2 rayDirection = (player.position - transform.position).normalized;
                Gizmos.DrawLine(transform.position, (Vector2) transform.position + rayDirection * maxVisionDist);
            }
        #endregion
    }
}
