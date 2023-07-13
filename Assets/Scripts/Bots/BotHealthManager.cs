using UnityEngine;

public class BotHealthManager : MonoBehaviour, ICanTakeDamage
{
	[SerializeField]
	int maxHealth = 2;
	int currentHealth;

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void Die()
	{
		GameManager.Instance.SetRemainingEnemies(GameManager.Instance.remainingEnemiesCount - 1);
		Destroy(gameObject);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}
}
