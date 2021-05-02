using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;


namespace Game.Events
{
    public class StaticTextEvent : Event
    {
        // Variables
        private bool triggered = false;
        public TextMesh message;
        public float fadeTime = 2f;

        [Header("Mode")]
        [Tooltip("The message will fade out on trigger if set to false.")]
        public bool fadeInText = true;
        
        
        private void Start()
        {
            if (message == null) message = GetComponentInChildren<TextMesh>();
            Color color = message.color;
            color.a = 0;
            message.color = color;
        }

        public override void RunEvent() {
            if (!triggered) {
                triggered = true;
                int[] values = {0, 1};
                if (!fadeInText) Array.Reverse(values);
                StartCoroutine(UtilFunctions.LerpCoroutine(FadeText, values[0], values[1], fadeTime));
            }
        }

        private void FadeText(float lerpValue) {
            Color color = message.color;
            color.a = lerpValue;
            message.color = color;
        }
    }
}