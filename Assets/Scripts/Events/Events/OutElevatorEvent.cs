﻿using Game.Control;
using Game.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class OutElevatorEvent : Event
    {
        Player player;
        Animator spriteAnimator;


        private void Start()
        {
            player = FindObjectOfType<Player>();
            spriteAnimator = GetComponentInChildren<Animator>();
        }

        public override void RunEvent()
        {
            player.Freeze();
            StartCoroutine(Utils.UtilFunctions.LerpCoroutine(player.SizeChange, 1f, 2f, 3f));
            spriteAnimator.SetBool("Leaving", true);
        }


    }
}