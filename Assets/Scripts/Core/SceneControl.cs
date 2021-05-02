using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    public int nextIndex;
    public string levelName;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Update is called once per frame
    public IEnumerator SceneTransition()
    {
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => spriteRenderer.color.a == 1);
        SceneManager.LoadScene(nextIndex);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Title") && Input.anyKeyDown)
        {
            StartCoroutine(Object.FindObjectOfType<SceneControl>().SceneTransition());
        }
    }
}
