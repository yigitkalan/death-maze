using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int bulletDamage = 1;

	private void OnCollisionEnter(Collision other)
	{
		// if collided object has ICanTakeDamage interface
		if (other.gameObject.GetComponent<ICanTakeDamage>() != null)
		{
			other.gameObject
				.GetComponent<ICanTakeDamage>()
				.TakeDamage(bulletDamage);
		}
	}
}
