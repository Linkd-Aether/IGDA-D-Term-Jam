using System.Collections;
using System.Collections.Generic;
using Game.Events;
using Game.Gameplay;
using Game.Lighting;
using Game.Movement;
using UnityEngine;

namespace Game.Events
{
    public class Cutscene2Event : Event
    {
        Player player;
        Mover mover;
        Animator animator;
        public override void RunEvent()
        {
            StartCoroutine(coolGuys());

        }

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
            mover = player.GetComponentInChildren<Mover>();
            animator = player.GetComponentInChildren<Animator>();
        }

        IEnumerator coolGuys()
        {
            // Get ahead of the trigger
            yield return new WaitForSeconds(0.1f);

            // Stop and turn around
            player.Freeze();
            mover.UpdateMovement(new Vector2(1, 0));
            mover.moveSpd = -1f;
            animator.SetBool("Moving", false);

            // Wait for explosion
            yield return new WaitForSeconds(5);

            // Run away
            mover.UpdateMovement(new Vector2(1, 0));
            mover.moveSpd = 100f;
            animator.SetBool("Moving", true);
        }
    }
}