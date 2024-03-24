using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgePlayer : MonoBehaviour
{
    [SerializeField] float damageGiven;
    private bool isActive = true;
    private void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isActive)
            collision.gameObject.GetComponent<Health>().TakeDamage(damageGiven);
    }

    public void setActive(bool stage)
    {
        isActive = stage;
    }
}