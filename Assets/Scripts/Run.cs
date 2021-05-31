using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class Run : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _movement;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _animator.SetBool("Grounded", _movement.IsGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(_movement.Speed));
    }
}
