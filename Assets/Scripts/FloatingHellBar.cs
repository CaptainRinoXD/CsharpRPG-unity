using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHellBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    public Slider healthSlider;

    private float currentHealth;
    private float maxHealth;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
        if (playerHealth != null)
        {
            currentHealth = playerHealth.currentHealth;
            maxHealth = playerHealth.StartingtHealth;

            currentHealth = maxHealth;

            UpdateHealthSlider();
        }
        else
        {
            Debug.LogError("No Health component found on the object.");
        }
    }

    private void Update()
    {
        if (Mathf.Abs(healthSlider.value - currentHealth) > 0.01f)
        {
            UpdateHealthSlider();
        }
        //for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
    //for testing
    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = currentHealth;
    }
}
