using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffMenu : MonoBehaviour
{
	public Debuff debuffeffect;
	
	private void OnTriggerEnter2D(Collider2D collision) 
	{
		Destroy(gameObject);
		debuffeffect.Apply(collision.gameObject);
	}
}
