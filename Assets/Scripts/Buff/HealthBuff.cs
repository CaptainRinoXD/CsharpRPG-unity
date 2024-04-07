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
            //Xử lý khi máu hiện tại ít hơn máu khởi đầu thì sẽ tăng máu bằng cách thêm dấu ngược
        } else if (targetColsion.GetComponent<Health>().currentHealth == targetColsion.GetComponent<Health>().StartingtHealth)
        {
            targetColsion.GetComponent<Health>().StartingtHealth = targetColsion.GetComponent<Health>().currentHealth + PlusHealth;
            targetColsion.GetComponent<Health>().currentHealth = targetColsion.GetComponent<Health>().StartingtHealth;
            System.Console.WriteLine("Check");
            //Xử lý khi máu hiện tại bằng màu khởi đầu thì sẽ thêm máu cho khởi đầu và set máu hiện tại bằng máu khởi đầu
        }
    }
}
