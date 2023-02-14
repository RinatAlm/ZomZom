using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;

    void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * _moveSpeed * Time.deltaTime,_joystick.Vertical * _moveSpeed * Time.deltaTime);
        if(_joystick.Horizontal>_joystick.Vertical)
        {

        }
    }
}
