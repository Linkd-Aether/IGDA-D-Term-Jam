using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Game.Utils;


namespace Game.Events
{
    public class ScreenShakeEvent : Event
    {
        // Variables
        public float shakeDelay = 0f;
        public float shakeDuration = 0f;
        public float shakeIntensity = 0f;
        public float shakeFrequency = 0f;
        
        // Components & References
        private CinemachineVirtualCamera shakeCamera; 
        private CinemachineBasicMultiChannelPerlin shakeEffect;

        private void Start() {
            shakeCamera = FindObjectOfType<CinemachineVirtualCamera>();
            shakeEffect = shakeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public override void RunEvent() {
            BeginCameraShake();
        }

        public void BeginCameraShake() {
            StartCoroutine(CameraShake());
        }

        public void StopCameraShake() {
            UpdateIntensity(0);
        }

        private IEnumerator CameraShake() {
            yield return new WaitForSeconds(shakeDelay);
            yield return StartCoroutine(UtilFunctions.LerpCoroutine(UpdateIntensity, 1, 0, shakeDuration));
        }

        private void UpdateIntensity(float perecent) {
            shakeEffect.m_AmplitudeGain = perecent * shakeIntensity;
            shakeEffect.m_FrequencyGain = perecent * shakeFrequency;
        }
    }
}
