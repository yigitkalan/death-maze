using DG.Tweening;
using UnityEngine;

public class BotHealthManager : MonoBehaviour, ICanTakeDamage
{
	[SerializeField]
	MeshRenderer _hoodieMesh;

	[SerializeField]
	CameraShake _cameraShake;

	[SerializeField]
	int maxHealth = 2;
	int currentHealth;

	Tween colorTween;

	private void Start()
	{
		_cameraShake = FindObjectOfType<CameraShake>();
		currentHealth = maxHealth;
	}

	private void OnDisable()
	{
		colorTween?.Kill();
	}

	public void Die()
	{
		GetComponent<CapsuleCollider>().enabled = false;
		GameManager.Instance.AddPoints(100);
		GameManager.Instance.SetRemainingEnemies(GameManager.Instance.remainingEnemiesCount - 1);
		transform.DORotate(new Vector3(-90, transform.rotation.eulerAngles.y, 0), 1f);
		transform.DOMoveY(-1, 1f).OnComplete(() => Destroy(gameObject));
	}

	public void TakeDamage(int damage)
	{
		ApplyColorChangeToEnemy();

		_cameraShake.ShakeCamera();
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void ApplyColorChangeToEnemy()
	{
		colorTween?.Kill();
		Color oldColor = _hoodieMesh.material.color;
		colorTween = _hoodieMesh.material
			.DOColor(Color.red, 0.15f)
			.OnComplete(() => _hoodieMesh.material.DOColor(oldColor, 0.15f));
	}
}
