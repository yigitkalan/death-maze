using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanTakeDamage
{
	void TakeDamage(int damage);

	void Die();
}
