using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMotor : NetworkBehaviour
{
    public Rigidbody rb;
    public Transform glider;
    public bool grounded = false, jumpCooldown = true, gliding = true;
    public float friction = 0.1f;
    public float acceleration = 10.0f, frictionEpsilon = 0.1f; 
    public float maxSpeed = 5.0f, jumpForce = 10.0f, dashForce = 10.0f;
    public float dashCooldownTime = 5f, curDashCooldownTime = 0f, dashDuration = 0.2f;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody>();

        float rad = 40f, theta = Random.Range(0f, 360f);
        Vector3 pos = new Vector3(rad * Mathf.Cos(theta), 150f, rad * Mathf.Sin(theta));
        transform.position = pos;

        gliding = true;
    }

    void FixedUpdate()
    {
        glider.gameObject.SetActive(gliding);
        if(!IsLocalPlayer) return;
        
        Vector3 newForce = Vector3.zero;
        Vector3 frictionForce = -rb.velocity.normalized * friction;
        if(frictionForce.magnitude < frictionEpsilon)
        {
            frictionForce = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.W))
        {
            newForce += transform.forward * acceleration;
            //slightly rotate towards forward
            //cameraParent.localEulerAngles /= 1.5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newForce -= transform.forward * acceleration * 0.5f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newForce -= acceleration * transform.right * 0.5f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newForce += acceleration * transform.right * 0.5f;
        }

        frictionForce.y = 0;

        if(Input.GetKey(KeyCode.Space) && grounded && jumpCooldown)
        {
            rb.velocity += Vector3.up * jumpForce;
            Invoke(nameof(ResetJump), 0.75f);
            jumpCooldown = false;
            SoundManager.PlaySound(SoundType.JUMP, 0.5f);
        }

        curDashCooldownTime = Mathf.Max(0f, curDashCooldownTime - Time.deltaTime);
        if(Input.GetKey(KeyCode.E) && curDashCooldownTime <= 0)
        {
            rb.velocity = new Vector3(rb.velocity.x*dashForce, rb.velocity.y, rb.velocity.z*dashForce);
            curDashCooldownTime = dashCooldownTime;
        }

        rb.AddForce(newForce + frictionForce);

        bool dashing = curDashCooldownTime > dashCooldownTime - dashDuration;
        float xyMag = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        if (xyMag > maxSpeed && !dashing)
        {
            rb.velocity = new Vector3(
                rb.velocity.x/xyMag * maxSpeed, 
                rb.velocity.y, 
                rb.velocity.z/xyMag * maxSpeed);
        }

        if(gliding)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Max(-10f, rb.velocity.y), rb.velocity.z);
        }
    }

    void ResetJump()
    {
        jumpCooldown = true;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void StopGlidingRpc(RpcParams rpcParams = default)
    {
        gliding = false;
    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.layer == 6) 
        {
            if(gliding)
            {
                StopGlidingRpc();
            }

            grounded = true;
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.layer == 6) grounded = false;
    }
}
