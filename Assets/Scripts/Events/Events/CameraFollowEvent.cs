using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace Game.Events
{
    public class CameraFollowEvent : Event
    {
        // Constants
        private float TRANSFER_TARGET = .5f;

        // Variables
        public Transform target;
        public float moveSpeed =  7.5f;
        private GameObject transitionObject;
        
        // Components & References
        private CinemachineVirtualCamera followCamera; 


        private void Start() {
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }

        // Update is called once per frame
        public override void RunEvent()
        {
            if (target != null) {
                transitionObject = new GameObject();
                transitionObject.transform.position = followCamera.Follow.transform.position;
                followCamera.Follow = transitionObject.transform;
                StartCoroutine(CameraTransfer());
            }
        }

        private IEnumerator CameraTransfer() {
            while (Vector2.Distance(transitionObject.transform.position, target.position) > TRANSFER_TARGET) {
                transitionObject.transform.position = Vector2.MoveTowards(transitionObject.transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            
            followCamera.Follow = target;
            Destroy(transitionObject);
        }
    }
}
