using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpModifier;
    
    private Animator _animator;
    private bool isRightSide = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if (!Input.anyKeyDown)
        {
            Idle();
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (isRightSide)
                Flip();

            Run();            
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!isRightSide)
                Flip();

            Run();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Flip()
    {
        isRightSide = !isRightSide;
        transform.Rotate(0f, 180, 0f);
    }

    private void Run()
    {
        _animator.SetBool("Run", true);
        transform.Translate(_speed * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        transform.Translate(0, _jumpModifier, 0);
    }

    private void Idle()
    {
        _animator.SetBool("Run", false);
        _animator.ResetTrigger("Jump");
    }
}
