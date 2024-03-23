using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/JumpBuff")]

public class JumpBuff : Buff 
{
	public int buff;
	
	public override void Apply(GameObject target)
	{
		target.GetComponent<PlayerMovement>().jumpPower += buff;
	}
}
