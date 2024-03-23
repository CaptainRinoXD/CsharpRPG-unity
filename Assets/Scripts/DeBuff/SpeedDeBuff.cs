using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="DeBuff/SpdDeBuff")]
public class SpeedDeBuff : Debuff
{
	public float debuff;
	
	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerMovement>().moveSpeed -= debuff;
	}
}
