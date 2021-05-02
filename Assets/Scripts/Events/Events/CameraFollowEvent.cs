using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace Game.Events
{
    public class CameraFollowEvent : Event
    {
        // Variables
        public Transform target;
        
        // Components & References
        private CinemachineVirtualCamera followCamera; 


        private void Start() {
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }

        // Update is called once per frame
        public override void RunEvent()
        {
            if (target != null) {
                followCamera.Follow = target;
            }
        }
    }
}
