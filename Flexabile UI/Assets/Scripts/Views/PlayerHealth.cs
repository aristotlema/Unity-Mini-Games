using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int startingHealth = 100;

    public float debuffCooldown = 1f;

    // public ParticleSystem damageVFX;

    [Header("Events")]
    public GameEvent onPlayerHealthChanged;

    private int currentHealth;

    private WaitForSeconds waitForDebuff;

    private void Awake()
    {
        currentHealth = startingHealth;
        waitForDebuff = new WaitForSeconds(debuffCooldown);

        StartCoroutine(TakeDamage(1));
    }

    private IEnumerator TakeDamage(int amount)
    {
        while(currentHealth > 0)
        {
            currentHealth -= amount;

            // damageVFX.Play();

            onPlayerHealthChanged.Raise(this, currentHealth);

            yield return waitForDebuff;
        }
    }
}
