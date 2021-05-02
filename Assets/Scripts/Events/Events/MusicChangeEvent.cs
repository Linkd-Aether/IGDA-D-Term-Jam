using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;


namespace Game.Events
{
    public class MusicChangeEvent : Event
    {
        private bool triggered = false;

        public float fadeTime = 2.5f;
        public float maxVolume = .5f;

        public SoundControl soundControl;
        public AudioClip bgm;


        private void Start() {
            if (soundControl == null) {
                soundControl = FindObjectOfType<SoundControl>();
            }            
        }

        public override void RunEvent() {
            if (!triggered) {
                triggered = true;
                StartCoroutine(soundControl.SwitchMusic(bgm, fadeTime, maxVolume));
            }
        }
    }
}
