using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
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
        _animator.SetFloat("Speed", Mathf.Abs(_movement.Speed));

        if (Input.GetKeyDown(KeyCode.Space))
            _animator.SetTrigger("Jump"); 
    }
}