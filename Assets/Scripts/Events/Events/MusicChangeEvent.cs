using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;


namespace Game.Events
{
    public class MusicChangeEvent : Event
    {
        private bool triggered = false;

        public bool singleTriggerEvent = true;
        public float fadeTime = 2.5f;
        public SoundControl soundControl;
        public AudioClip bgm;


        public override void RunEvent() {
            if (!triggered || !singleTriggerEvent) {
                triggered = true;
                StartCoroutine(soundControl.CrossFade(bgm, fadeTime));
                Destroy(this);
            }
        }
    }
}
