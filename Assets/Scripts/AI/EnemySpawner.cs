using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Constants
    private static AudioClip SPAWN_SOUND;

    // Components & References
    private ParticleSystem spawnParticles;
    private Animation spawnAnimation;

    private Transform enemy;

    private void Start()
    {
        SPAWN_SOUND = Resources.Load<AudioClip>("Audio/SFX/Eldritch Scream 1");

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

    public void PlayEntrySound() {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.PlayOneShot(SPAWN_SOUND);
    }
}
