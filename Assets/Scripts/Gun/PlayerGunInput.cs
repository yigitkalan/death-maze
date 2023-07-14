using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class PlayerGunInput : MonoBehaviour
{
	[SerializeField]
	PlayerMovementInput _playerMovementInput;

	[SerializeField]
	GunController _gunController;

	[SerializeField]
	Transform _playerGun;

	[SerializeField]
	float gunBackFire = -20f;

	IDisposable gunInputDisposable;

	Tween gunTween;

	private void Awake()
	{
		_playerGun = GameObject.FindGameObjectWithTag("PlayerGun").transform;
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
		gunTween?.Kill();
		gunInputDisposable.Dispose();
	}

	void CheckShoot()
	{
		if (_playerMovementInput.IsAiming())
		{
			if (_gunController.canShoot)
			{
				gunTween?.Kill();
				gunTween = _playerGun
					.DOLocalRotate(new Vector3(gunBackFire, 0, 0), 0.1f)
					.OnComplete(() => _playerGun.DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
			}

			_gunController.Shoot();
		}
	}
}
