using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Components & References
    private ParticleSystem spawnParticles;
    private Animation spawnAnimation;


    private void Start()
    {
        spawnAnimation = GetComponent<Animation>();
        spawnParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) spawnAnimation.Play();
    }

    public void PlayParticleSystem() 
    {
        spawnParticles.Play();
    }

    public void DetachSpawn() 
    {
        Transform enemy = null;
        foreach (Transform child in transform) {
            if (child.gameObject.tag == "Enemy") {
                enemy = child;
            }
        }
        if (enemy != null) {
            enemy.parent = transform.parent;
            enemy.GetComponentInChildren<Animator>().enabled = true;
        } 

        Destroy(this.gameObject);
    }
}
