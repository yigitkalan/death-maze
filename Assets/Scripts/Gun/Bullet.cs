using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private int defaultBulletDamage = 1;

	private void OnCollisionEnter(Collision other)
	{
		ICanTakeDamage canTakeDamage =
			other.gameObject.GetComponent<ICanTakeDamage>();
		// if collided object has ICanTakeDamage interface
		if (canTakeDamage != null)
		{
			canTakeDamage.TakeDamage(defaultBulletDamage);
		}
	}

	public void SetBulletDamage(int _bulletDamage)
	{
		defaultBulletDamage = _bulletDamage;
	}
}
