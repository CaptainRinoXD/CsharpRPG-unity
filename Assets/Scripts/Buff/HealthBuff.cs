using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "Buff/HealthBuff")]
public class HealthBuff : Buff
{
    [SerializeField] private float PlusHealth;
    public override void Apply(GameObject targetColsion)
    {
        if(targetColsion.GetComponent<Health>().currentHealth < targetColsion.GetComponent<Health>().StartingtHealth)
        {
            targetColsion.GetComponent<Health>().TakeDamage(-PlusHealth);
        } else if (targetColsion.GetComponent<Health>().currentHealth == targetColsion.GetComponent<Health>().StartingtHealth)
        {
            targetColsion.GetComponent<Health>().StartingtHealth = targetColsion.GetComponent<Health>().currentHealth + PlusHealth;
            targetColsion.GetComponent<Health>().currentHealth = targetColsion.GetComponent<Health>().StartingtHealth;
            System.Console.WriteLine("Check");
        }
    }
}
