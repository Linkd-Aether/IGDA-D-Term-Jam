using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Components & References
    private ParticleSystem spawnParticles;
    private Animation spawnAnimation;

    private Transform enemy;

    private void Start()
    {
        spawnAnimation = GetComponent<Animation>();
        spawnParticles = GetComponentInChildren<ParticleSystem>();

        foreach (Transform child in transform) {
            if (child.gameObject.tag == "Enemy") {
                enemy = child;
            }
        }
        enemy.gameObject.SetActive(false);
    }

    public void SpawnEnemy()
    {
        enemy.gameObject.SetActive(true);
        spawnAnimation.Play();
    }

    public void PlayParticleSystem() 
    {
        spawnParticles.Play();
    }

    public void DetachSpawn() 
    {

        if (enemy != null) {
            enemy.parent = transform.parent;
            enemy.GetComponentInChildren<Animator>().enabled = true;
        } 

        Destroy(this.gameObject);
    }
}
