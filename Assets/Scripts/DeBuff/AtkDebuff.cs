using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DeBuff/AttackDeBuff")]
public class AtkDebuff : Debuff
{
	public float debuff;
	
	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerAttack>().attackCoolDown -= debuff;
	}
}
