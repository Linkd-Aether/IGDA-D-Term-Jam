using Game.Control;
using Game.Gameplay;
using Game.Lighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Game.Lighting;


namespace Game.Events
{
    public class OutElevatorEvent : Event
    {
        Player player;
        Animator spriteAnimator;
        Lantern lantern;

        private void Start()
        {
            player = FindObjectOfType<Player>();
            spriteAnimator = GetComponentInChildren<Animator>();
            lantern = player.GetComponentInChildren<Lantern>();
        }

        public override void RunEvent()
        {
            player.Freeze();

            foreach (SpriteRenderer sprite in player.GetComponentsInChildren<SpriteRenderer>()) {
                sprite.sortingOrder += 5;
            }
            GetComponentInChildren<SpriteRenderer>().sortingOrder += 5;

            Lantern lantern = player.GetComponentInChildren<Lantern>();
            lantern.StopAllCoroutines();
            StartCoroutine(Utils.UtilFunctions.LerpCoroutine(lantern.SetLightFraction, 1, 3f, 3f));

            StartCoroutine(Utils.UtilFunctions.LerpCoroutine(player.SizeChange, 1f, 2f, 3f));
            StartCoroutine(Utils.UtilFunctions.LerpCoroutine(lantern.LightSetting, 1f, 2f, 3f));
            spriteAnimator.SetBool("Leaving", true);
        }
    }
}
