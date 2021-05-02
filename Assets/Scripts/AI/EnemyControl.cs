using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;


namespace Game.AI {
    [RequireComponent(typeof(AudioSource))]
    public class EnemyControl : MonoBehaviour
    {
        // Constants
        private static AudioClip[] SCREAMS = new AudioClip[10];
        private static int[] BEGIN_CHASE = {0,4,5,7};
        private static int[] ABANDON_CHASE = {2,8,6};
        private static int[] KILL_PLAYER = {1,3,9};

        private static AudioClip CHASE_MUSIC;

        private float MIN_TIME_BETWEEN_SCREAMS = 4f;

        // Variables
        private float timeSinceLastScream = 0f;

        // Components & References
        private AudioSource audioSource;
        private SoundControl soundControl;


        // Start is called before the first frame update
        private void Start()
        {
            for (int i = 0; i < SCREAMS.Length; i ++) {
                SCREAMS[i] = Resources.Load<AudioClip>($"Audio/SFX/Eldritch Scream {i+1}");
            }
            CHASE_MUSIC = Resources.Load<AudioClip>("Audio/BGM/Eldritch Attack 5.1.21");
                
            audioSource = GetComponent<AudioSource>();
            soundControl = FindObjectOfType<SoundControl>();
        }

        private void Update() {
            timeSinceLastScream += Time.deltaTime;
        }

        private void PlayRandomScream(int[] screamSet){ 
            if (timeSinceLastScream >= MIN_TIME_BETWEEN_SCREAMS) {
                int chosen = Random.Range(0, screamSet.Length);
                audioSource.PlayOneShot(SCREAMS[screamSet[chosen]]);
                timeSinceLastScream = 0;
            }
        }

        public void BeginChaseScream(){ 
            PlayRandomScream(BEGIN_CHASE);
        }

        public void AbandonChaseScream(){ 
            PlayRandomScream(ABANDON_CHASE);
        }

        public void KillPlayerScream(){ 
            PlayRandomScream(KILL_PLAYER);
        }

        public void BeginChase() {
            StartCoroutine(soundControl.CrossFadeInMix(CHASE_MUSIC, 1.5f, .2f));
        }

        public void EndChase() {
            StartCoroutine(soundControl.CrossFadeOutMix(1.5f));
        }
    }
}
