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
		_inGameUIController.SetDeathUI();
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		_inGameUIController.SetHealthBar((currentHealth / maxHealth) * 100);
		if (currentHealth <= 0)
		{
			Die();
		}
	}
}
