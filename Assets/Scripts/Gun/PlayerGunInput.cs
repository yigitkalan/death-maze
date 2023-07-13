using System;
using UniRx;
using UnityEngine;

public class PlayerGunInput : MonoBehaviour
{
	[SerializeField]
	PlayerMovementInput _playerMovementInput;

	[SerializeField]
	GunController _gunController;

	IDisposable gunInputDisposable;

	private void Awake()
	{
		_gunController = GetComponent<GunController>();
		_playerMovementInput = GameObject
			.FindGameObjectWithTag("Player")
			.GetComponent<PlayerMovementInput>();
	}

	void Start()
	{
		gunInputDisposable = Observable
			.EveryUpdate()
			.Where(_ => !GameManager.Instance.isPlayerDead)
			.Subscribe(_ =>
			{
				CheckShoot();
			});
	}

	private void OnDisable()
	{
		gunInputDisposable.Dispose();
	}

	void CheckShoot()
	{
		if (_playerMovementInput.IsAiming())
		{
			_gunController.Shoot();
		}
	}
}
