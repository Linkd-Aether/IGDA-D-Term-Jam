using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils 
{
    public static class UtilFunctions
    {
        public delegate void LerpDelegate(float value);

        public static IEnumerator LerpCoroutine(LerpDelegate method, float startValue, float endValue, float lerpDuration) 
        {
            float lerpT = 0;

            while (lerpT < 1) {
                Debug.LogError("LERPING!");
                lerpT += Time.deltaTime / lerpDuration;
                lerpT = Mathf.Clamp(lerpT, 0, 1);
                float value = Mathf.Lerp(startValue, endValue, lerpT);
                method(value);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
