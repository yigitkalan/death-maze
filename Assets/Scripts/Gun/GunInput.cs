using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GunInput : MonoBehaviour
{
	[SerializeField]
	PlayerMovementInput _playerMovementInput;

	[SerializeField]
	GunController _gunController;

	private void Awake()
	{
		_gunController = GetComponent<GunController>();
		_playerMovementInput = GameObject
			.FindGameObjectWithTag("Player")
			.GetComponent<PlayerMovementInput>();
	}

	// Start is called before the first frame update
	void Start()
	{
		Observable.EveryUpdate().Subscribe(_ => CheckShoot());
	}

	void CheckShoot()
	{
		if (_playerMovementInput.IsAiming())
		{
			_gunController.Shoot();
		}
	}
}
