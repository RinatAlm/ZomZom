using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] AudioManager _audioManager;

    private void Start()
    {
        _audioManager.Play("28DaysLater");
    }
    void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * _moveSpeed * Time.deltaTime,_joystick.Vertical * _moveSpeed * Time.deltaTime);
        if(_joystick.Vertical > 0.2f || _joystick.Vertical < -0.2f)
        {
            _animator.SetBool("isVertical", true);
        }
        else
        {
            _animator.SetBool("isVertical", false);
        }

        if( _joystick.Horizontal>0.15f)
        {
            _animator.SetBool("isPositiveHorizontal", true);
            _animator.SetBool("isNegativeHorizontal", false);
        }
        else if( _joystick.Horizontal<-0.15f)
        {
            _animator.SetBool("isNegativeHorizontal", true);
            _animator.SetBool("isPositiveHorizontal", false);
        }
        else
        {
            _animator.SetBool("isNegativeHorizontal", false);
            _animator.SetBool("isPositiveHorizontal", false);
            _animator.SetBool("isStill", true);
        }

       
    }
}
