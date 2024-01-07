using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int startingHealth;
    private int currentHealth;

    public Health(int startingHealth)
    {
        this.startingHealth = startingHealth;
        Reset();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Reset()
    {
        this.currentHealth = this.startingHealth;
    }

    public void Decrease(int amount)
    {
        this.currentHealth -= Mathf.Abs(amount);
    }
    public void Increase(int amount)
    {
        this.currentHealth += Mathf.Abs(amount);
    }
}
