using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Lean.Gui;
using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LeanJoystick moveJoystick;
    public Vector2 moveDirection { get; private set; }
    public Vector2 aimDirection { get; private set; }
    [SerializeField] LeanJoystick aimJoystick;
    // Start is called before the first frame update
    void Start()
    {

        Observable.EveryUpdate().Subscribe(
            _ => SetDirections()
        ).AddTo(this);

    }



    void SetDirections()
    {
        moveDirection = moveJoystick.ScaledValue;
        aimDirection = aimJoystick.ScaledValue;
    }
    bool IsAiming()
    {
        return aimJoystick.ScaledValue != Vector2.zero;
    }

}
