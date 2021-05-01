using Game.Control;
using Game.Gameplay;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class InElevator : MonoBehaviour
    {
        Player player;

        private void Start()
        {
            player = FindObjectOfType<Player>();
            StartCoroutine(Utils.UtilFunctions.LerpCoroutine(player.SizeChange, .5f, 1f, 3f));
        }

        void StopControls()
        {
            player.Freeze();
        }

        void RestoreControls()
        {
            player.Unfreeze();
        }
    }
}