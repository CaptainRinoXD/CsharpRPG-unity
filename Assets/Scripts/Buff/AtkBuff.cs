using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buff/AttackBuff")]
public class AtkBuff : Buff
{
	public float buff;
	
	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerAttack>().attackCoolDown += buff;
	}
}
