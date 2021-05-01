using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutElevator : MonoBehaviour
{
    public void SceneShift()
    {
        StartCoroutine(Object.FindObjectOfType<SceneControl>().SceneTransition());
    }
}
