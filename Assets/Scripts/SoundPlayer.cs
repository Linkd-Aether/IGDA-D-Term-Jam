using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class SoundPlayer : Event
    {
        public AudioClip audio;
        public AudioSource source;

        // Start is called before the first frame update
        void Start()
        {
            if (source == null)
            {
                source = GetComponent<AudioSource>();
            }
        }

        public override void RunEvent()
        {
            source.PlayOneShot(audio, 1.0f);
        }
    }
}
