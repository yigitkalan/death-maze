using System;
using UniRx;
using UnityEngine;

public class BotShooting : MonoBehaviour
{
	[SerializeField]
	BotSight _botSight;

	[SerializeField]
	BotMovement _botMovement;

	[SerializeField]
	GunController _gunController;

	IDisposable shootDisposable;

	private void Start()
	{
		_botSight = GetComponent<BotSight>();
		_botMovement = GetComponent<BotMovement>();

		shootDisposable = Observable
			.EveryUpdate()
			.Where(
				_ => !GameManager.Instance.isPlayerDead && PlayerInRange() && !_botMovement.onPatrol
			)
			.Subscribe(_ => ShootPlayer());
	}

	private void OnDisable()
	{
		shootDisposable.Dispose();
	}

	void ShootPlayer()
	{
		_gunController.Shoot();
	}

	bool PlayerInRange() => _botSight.CanSeePlayer();
}
