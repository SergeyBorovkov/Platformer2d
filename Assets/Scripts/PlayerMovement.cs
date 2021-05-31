using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public Vector2 Velocity;
    public LayerMask LayerMask;

    private bool isRightSide = true;

    protected Vector2 targetVelocity;
    protected bool isGrounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    public bool IsGrounded => isGrounded;
    public float Speed => targetVelocity.x;    

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        targetVelocity = new Vector2(x, 0);

        if ((x < 0 && isRightSide) || (x > 0 && !isRightSide))        
            Flip();        

        if (Input.GetKey(KeyCode.Space) && isGrounded)
            Velocity.y = 5;
    }

    void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = targetVelocity.x;

        isGrounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    isGrounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity -= projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position += move.normalized * distance;
    }

    private void Flip()
    {
        isRightSide = !isRightSide;
        transform.Rotate(0f, 180, 0f);
    }
}
