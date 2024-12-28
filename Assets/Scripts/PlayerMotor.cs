using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public Rigidbody rb;

    public bool grounded = false, jumpCooldown = true;
    public float friction = 0.1f;
    public float acceleration = 10.0f, frictionEpsilon = 0.1f; 
    public float maxSpeed = 5.0f, jumpForce = 5.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        Vector3 newForce = Vector3.zero;
        Vector3 frictionForce = -rb.velocity.normalized * friction;
        if(frictionForce.magnitude < frictionEpsilon)
        {
            frictionForce = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.W))
        {
            newForce += transform.forward * acceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newForce -= transform.forward * acceleration;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newForce -= acceleration * transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newForce += acceleration * transform.right;
        }

        frictionForce.y = 0;

        if(Input.GetKey(KeyCode.Space) && grounded && jumpCooldown)
        {
            rb.velocity += Vector3.up * jumpForce;
            Invoke(nameof(ResetJump), 0.75f);
            jumpCooldown = false;
            SoundManager.PlaySound(SoundType.JUMP, 0.5f);
        }

        rb.AddForce(newForce + frictionForce);

        float xyMag = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        if (xyMag > maxSpeed)
        {
            rb.velocity = new Vector3(
                rb.velocity.x/xyMag * maxSpeed, 
                rb.velocity.y, 
                rb.velocity.z/xyMag * maxSpeed);
        }
    }

    void ResetJump()
    {
        jumpCooldown = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 6) grounded = true;
    }
    
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.layer == 6) grounded = false;
    }
}
