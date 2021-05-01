using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Game.Lighting
{

    public class DevLight : MonoBehaviour
    {
        public Light2D lightObj;
    
    // Start is called before the first frame update
        void Start()
        {
            lightObj = GetComponent<Light2D>();
            lightObj.intensity = 0;
        }
    }
}