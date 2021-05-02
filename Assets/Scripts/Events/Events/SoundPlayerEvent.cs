using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class SoundPlayerEvent : Event
    {
        public AudioClip audioClip;
        private AudioSource source;

        // Start is called before the first frame update
        void Start()
        {
            source = GetComponent<AudioSource>();
        }

        public override void RunEvent()
        {
            source.PlayOneShot(audioClip, 1.0f);
        }
    }
}
