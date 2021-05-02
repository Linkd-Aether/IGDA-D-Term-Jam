using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Utils;


namespace Game.Events
{
    public class DynamicTextEvent : Event
    {
        // Variables
        private bool triggered = false;
        private Text currentMessage;

        public string[] messages;
        public float fadeInTime = 1.5f;
        public float fadeOutTime = 1.5f;
        public float targetSize = 10f;
        
        // Components & References
        public Canvas canvas;
        

        public override void RunEvent() {
            if (!triggered) {
                triggered = true;
                StartCoroutine(TextChanges());
            }
        }

        private IEnumerator TextChanges() {
            foreach (string message in messages) {
                currentMessage = GenerateText(message);

                StartCoroutine(UtilFunctions.LerpCoroutine(SizeText, 0, 1, fadeInTime + fadeOutTime));
                yield return StartCoroutine(UtilFunctions.LerpCoroutine(FadeText, 0, 1, fadeInTime));
                yield return StartCoroutine(UtilFunctions.LerpCoroutine(FadeText, 1, 0, fadeInTime));
                Destroy(currentMessage.gameObject);
            }
        }

        private Text GenerateText(string message) {
            GameObject textObj = new GameObject("Dynamic Text");
            textObj.transform.parent = canvas.transform;

            Text text = textObj.AddComponent<Text>();
            text.text = message;

            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            text.font = ArialFont;
            text.material = ArialFont.material;

            Color color = text.color;
            color.a = 0;
            text.color = color;

            RectTransform canvasSize = canvas.transform.GetComponent<RectTransform>();
            float x = Random.Range(canvasSize.rect.width / 4, canvasSize.rect.width * 3 / 4);
            float y = Random.Range(canvasSize.rect.height / 4, canvasSize.rect.height * 3 / 4);
            text.transform.position = new Vector2(x, y);

            text.alignment = TextAnchor.MiddleCenter;

            return text;
        }

        private void SizeText(float lerpValue) {
            currentMessage.transform.localScale = targetSize * Vector3.one * lerpValue;
        }

        private void FadeText(float lerpValue) {
            Color color = currentMessage.color;
            color.a = lerpValue;
            currentMessage.color = color;
        }
    }
}