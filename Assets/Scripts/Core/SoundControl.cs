using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;

namespace Game.Core
{
    public class SoundControl : MonoBehaviour
    {
        // Constants
        private float FADE_IN_TIME = 5f;
        
        // Variables
        private float mainMaxAudio = .5f;
        private float crossMaxAudio = 0f;

        private float mainTrueMax;

        private AudioSource audioSource;
        private AudioSource crossAudio;
        
        
        private void Awake() {
            DontDestroyOnLoad(this.gameObject);    
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0;
            audioSource.loop = true;

            crossAudio = this.gameObject.AddComponent<AudioSource>();
            crossAudio.loop = true;

            StartCoroutine(UtilFunctions.LerpCoroutine(FadeTheme, 0, 1, FADE_IN_TIME));
        }

        private void FadeTheme(float percent) {
            audioSource.volume = mainMaxAudio * percent;
        }

        private void CrossFade(float percent) {
            audioSource.volume = mainMaxAudio * percent;
            crossAudio.volume = crossMaxAudio * (1 - percent);
        }

        public IEnumerator SwitchMusic(AudioClip audioClip, float fadeTime, float newAudioMaxVolume) {
            crossAudio.volume = 0;
            crossAudio.clip = audioClip;
            crossAudio.Play();

            crossMaxAudio = audioSource.volume;
            mainMaxAudio = newAudioMaxVolume;
            
            AudioSource temp = audioSource;
            audioSource = crossAudio;
            crossAudio = temp;

            yield return StartCoroutine(UtilFunctions.LerpCoroutine(CrossFade, 0, 1, fadeTime));
        }

        public IEnumerator CrossFadeInMix(AudioClip audioClip, float fadeTime, float newAudioMaxVolume) {
            crossAudio.volume = 0;
            crossAudio.clip = audioClip;
            crossAudio.Play();

            mainTrueMax = mainMaxAudio;
            mainMaxAudio = audioSource.volume;
            crossMaxAudio = newAudioMaxVolume;

            yield return StartCoroutine(UtilFunctions.LerpCoroutine(CrossFade, 1, .5f, fadeTime));
        }

        public IEnumerator CrossFadeOutMix(float fadeTime) {
            yield return StartCoroutine(UtilFunctions.LerpCoroutine(CrossFade, .5f, 1f, fadeTime));
            mainMaxAudio = mainTrueMax;
            if (mainMaxAudio != audioSource.volume) {
                float start = audioSource.volume / mainMaxAudio;
                float fade = FADE_IN_TIME * (1 - start);
                yield return StartCoroutine(UtilFunctions.LerpCoroutine(FadeTheme, start, 1, fade));
            }
        }
    }
}
