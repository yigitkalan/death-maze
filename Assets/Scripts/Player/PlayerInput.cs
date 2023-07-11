using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Lean.Gui;
using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	[SerializeField]
	LeanJoystick moveJoystick;
	public Vector2 movementInput { get; private set; }

	[SerializeField]
	LeanJoystick aimJoystick;
	public Vector2 aimInput { get; private set; }

	// Start is called before the first frame update
	void Start()
	{
		Observable.EveryUpdate().Subscribe(_ => SetDirections()).AddTo(this);
	}

	void SetDirections()
	{
		movementInput = moveJoystick.ScaledValue;
		aimInput = aimJoystick.ScaledValue;
	}

	public bool IsAiming()
	{
		return aimJoystick.ScaledValue != Vector2.zero;
	}
}
