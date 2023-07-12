using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, ICanTakeDamage
{
	[SerializeField]
	int maxHealth = 5;

	public void Die()
	{
		Destroy(gameObject);
	}

	public void TakeDamage(int damage)
	{
		maxHealth -= damage;
		if (maxHealth <= 0)
		{
			Die();
		}
	}
}
