using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Buff/SpdBuff")]
public class SpeedBuff : Buff
{
	public float buff;
	
	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerMovement>().moveSpeed += buff;
	}
}
