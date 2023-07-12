using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, ICanTakeDamage
{
	[SerializeField]
	int maxHealth = 5;
	int currentHealth;

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void Die()
	{
		Destroy(gameObject);
	}

	public void TakeDamage(int damage)
	{
		print("taking damage");
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}
}
