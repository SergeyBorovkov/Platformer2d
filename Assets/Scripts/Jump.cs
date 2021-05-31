using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class Jump : MonoBehaviour
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
        if (Input.GetKey(KeyCode.Space) && _movement.IsGrounded)        
            _animator.SetTrigger("Jump");        
    }
}