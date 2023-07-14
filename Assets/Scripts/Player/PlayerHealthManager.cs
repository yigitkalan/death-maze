using DG.Tweening;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, ICanTakeDamage
{
	[SerializeField]
	float maxHealth = 5;
	float currentHealth;

	InGameUIController _inGameUIController;
	private Tween colorTween;

	[SerializeField]
	MeshRenderer _headMesh;

	private void Start()
	{
		_inGameUIController = FindObjectOfType<InGameUIController>();
		currentHealth = maxHealth;
	}

	private void OnDisable()
	{
		colorTween?.Kill();
	}

	public void Die()
	{
		GameManager.Instance.isPlayerDead = true;

		transform.DOLocalRotate(new Vector3(-90, 0, 0), 1f);
		transform
			.DOMoveY(-1, 1f)
			.OnComplete(() =>
			{
				Destroy(gameObject);
				_inGameUIController.SetDeathUI();
			});
	}

	public void TakeDamage(int damage)
	{
		ApplyColorChangeToPlayer();
		currentHealth -= damage;
		_inGameUIController.SetHealthBar((currentHealth / maxHealth) * 100);
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void ApplyColorChangeToPlayer()
	{
		colorTween?.Kill();
		Color oldColor = _headMesh.material.color;
		colorTween = _headMesh.material
			.DOColor(Color.red, 0.15f)
			.OnComplete(() => _headMesh.material.DOColor(oldColor, 0.25f));
	}
}
