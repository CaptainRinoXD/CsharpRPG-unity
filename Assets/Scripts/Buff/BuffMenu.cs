using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMenu : MonoBehaviour
{
	public Buff buffeffect;
	
	private void OnTriggerEnter2D(Collider2D collision) 
	{
		Destroy(gameObject);
		buffeffect.Apply(collision.gameObject);
	}
}
