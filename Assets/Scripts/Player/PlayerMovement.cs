using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _playerInput;
    [SerializeField] private float speed = 10;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();


        Observable.EveryUpdate().Subscribe(_ =>
        {
            Move();
        });

    }

    void Move()
    {
        transform.Translate(_playerInput.moveDirection * speed * Time.deltaTime, Space.World);
    }


}
