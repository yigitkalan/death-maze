using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, ICanTakeDamage
{
	[SerializeField]
	float maxHealth = 5;
	float currentHealth;

	InGameUIController _inGameUIController;

	private void Start()
	{
		_inGameUIController = FindObjectOfType<InGameUIController>();
		currentHealth = maxHealth;
	}

	public void Die()
	{
		GameManager.Instance.isPlayerDead = true;
		Destroy(gameObject);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		print(currentHealth);
		_inGameUIController.SetHealthBar((currentHealth / maxHealth) * 100);
		if (currentHealth <= 0)
		{
			Die();
		}
	}
}
