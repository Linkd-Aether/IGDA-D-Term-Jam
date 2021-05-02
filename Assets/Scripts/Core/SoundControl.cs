using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;

namespace Game.Core
{
    public class SoundControl : MonoBehaviour
    {
        // Constants
        private float MAX_VOLUME = .5f;
        private float FADE_IN_TIME = 5f;
        
        // Variables
        private AudioSource audioSource;
        private AudioSource crossAudio;
        

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0;
            audioSource.loop = true;

            StartCoroutine(UtilFunctions.LerpCoroutine(FadeTheme, 0, 1, FADE_IN_TIME));
        }

        private void FadeTheme(float percent) {
            audioSource.volume = MAX_VOLUME * percent;
        }

        private void CrossFadeTheme(float percent) {
            audioSource.volume = MAX_VOLUME * (1 - percent);
            crossAudio.volume = MAX_VOLUME * percent;
        }

        public IEnumerator CrossFade(AudioClip audioClip, float fadeTime) {
            crossAudio = this.gameObject.AddComponent<AudioSource>();
            crossAudio.volume = 0;
            crossAudio.clip = audioClip;
            crossAudio.loop = true;
            crossAudio.Play();
            
            yield return StartCoroutine(UtilFunctions.LerpCoroutine(CrossFadeTheme, 0, 1, fadeTime));

            AudioSource temp = audioSource;
            audioSource = crossAudio;
            Destroy(temp);
            print("ran throug");

            yield return null;
        }
    }
}
