using System;
using UniRx;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	PlayerInput _playerInput;
	Rigidbody rb;

	[Header("Movement")]
	[SerializeField]
	private float moveSpeed = 4;
	Vector3 movementVector;

	[Header("Aim")]
	[SerializeField]
	float aimSmoothTime = 0.1f;

	Vector3 aimVector;

	IDisposable movementDisposable;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		_playerInput = GetComponent<PlayerInput>();
	}

	void Start()
	{
		movementDisposable = Observable
			.EveryFixedUpdate()
			.Where(
				_ =>
					_playerInput.movementInput != Vector2.zero
					|| _playerInput.aimInput != Vector2.zero
			)
			.Subscribe(_ =>
			{
				MovePlayerOnXZ();
				RotatePlayer();
			});
	}

	private void OnDisable()
	{
		movementDisposable.Dispose();
	}

	void MovePlayerOnXZ()
	{
		movementVector = new Vector3(
			_playerInput.movementInput.x,
			0,
			_playerInput.movementInput.y
		);

		transform.Translate(
			movementVector * moveSpeed * Time.deltaTime,
			Space.World
		);
	}

	void RotatePlayer()
	{
		if (_playerInput.IsAiming())
		{
			aimVector = new Vector3(
				_playerInput.aimInput.x,
				0,
				_playerInput.aimInput.y
			);
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(aimVector, Vector3.up),
				aimSmoothTime * Time.deltaTime
			);
		}
		else
		{
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(movementVector, Vector3.up),
				aimSmoothTime * Time.deltaTime
			);
		}
	}
}
