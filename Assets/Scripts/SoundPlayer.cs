using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class SoundPlayer : Event
    {
        public AudioClip audio;
        AudioSource source;

        // Start is called before the first frame update
        void Start()
        {
            source = GetComponent<AudioSource>();
        }

        public override void RunEvent()
        {
            source.PlayOneShot(audio, 1.0f);
        }
    }
}
