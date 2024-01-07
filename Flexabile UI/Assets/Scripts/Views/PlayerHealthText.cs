using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthText : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    private void Awake()
    {
        SetHealth(0);
    }

    private void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }

    public void UpdateHealth(Component sender, object data)
    {//e.g. if (sender is PlayerHealth), if (sender is Health)

        if (data is int)
        {
            int amount = (int) data;

            SetHealth(amount);
            Debug.Log($"Updating the health by {amount}");
        }
        
    }
}
