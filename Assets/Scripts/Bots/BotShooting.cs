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

	BotHealthManager myBotHealthManager;

	private void Start()
	{
		myBotHealthManager = GetComponent<BotHealthManager>();
		_botSight = GetComponent<BotSight>();
		_botMovement = GetComponent<BotMovement>();

		shootDisposable = Observable
			.EveryUpdate()
			.Where(
				_ =>
					!GameManager.Instance.isPlayerDead
					&& PlayerInRange()
					&& !_botMovement.onPatrol
					&& !(myBotHealthManager.currentHealth == 0)
			)
			.Subscribe(_ => ShootPlayer());
	}

	private void OnDisable()
	{
		shootDisposable.Dispose();
	}

	void ShootPlayer()
	{
		//create a backkick tween up there and set autostart to false then play it here
		_gunController.Shoot();
	}

	bool PlayerInRange() => _botSight.CanSeePlayer();
}
