using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "DeBuff/JumpDeBuff")]

public class JumpDeBuff : Debuff
{
	public int debuff;
	
	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerMovement>().jumpPower -= debuff;
	}
}
