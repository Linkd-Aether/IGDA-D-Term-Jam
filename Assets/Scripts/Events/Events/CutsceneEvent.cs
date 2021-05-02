using System.Collections;
using System.Collections.Generic;
using Game.Events;
using Game.Gameplay;
using Game.Lighting;
using Game.Movement;
using UnityEngine;

namespace Game.Events
{
    public class CutsceneEvent : Event
    {
        Player player;
        public override void RunEvent()
        {
            player.Freeze();
            player.GetComponentInChildren<Mover>().UpdateMovement(new Vector2(1, 0));
            //player.GetComponentInChildren<Mover>().moveSpd = 20f;
            player.GetComponentInChildren<Lantern>().LightOn();
        }

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}