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
	CameraShake _cameraShake;

	[SerializeField]
	Transform _playerGun;

	[SerializeField]
	float gunBackFire = -20f;

	[SerializeField]
	float playerBackFire = -10f;
	IDisposable gunInputDisposable;

	Tween gunTween;
	Tween playerTween;

	[SerializeField]
	Transform playerBody;

	private void Awake()
	{
		_cameraShake = FindObjectOfType<CameraShake>();
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
		playerTween?.Kill();
		gunTween?.Kill();
		gunInputDisposable.Dispose();
	}

	void CheckShoot()
	{
		if (_playerMovementInput.IsAiming())
		{
			if (_gunController.canShoot)
			{
				PlayerBackFire();
				GunBackFire();
			}

			_gunController.Shoot();
		}
	}

	void PlayerBackFire()
	{
		playerTween?.Kill();
		playerTween = playerBody
			.DOLocalRotate(new Vector3(playerBackFire, 0, 0), 0.1f)
			.OnComplete(() =>
			{
				playerBody.DOLocalRotate(Vector3.zero, 0.1f);
			});
	}

	void GunBackFire()
	{
		gunTween?.Kill();
		gunTween = _playerGun
			.DOLocalRotate(new Vector3(gunBackFire, 0, 0), 0.1f)
			.OnComplete(() => _playerGun.DOLocalRotate(Vector3.zero, 0.1f));
	}
}
