using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public Rigidbody rb;

    private bool grounded = false;
    private static Surface ice = new Surface(0.8f);
    private static Surface ground = new Surface(10.0f);
    private static Surface currentSurface = ground;

    public float acceleration = 10.0f; 
    public float maxSpeed = 5.0f;

    void Awake(){
        rb = GetComponent<Rigidbody>();

    }

    
    void Update()
    {
        Vector3 newForce = Vector3.zero;
        Vector3 frictionForce = -rb.velocity.normalized * currentSurface.friction;

        if (Input.GetKey(KeyCode.W))
        {
            newForce.z += acceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newForce.z -= acceleration;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newForce.x -= acceleration;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newForce.x += acceleration;
        }
        frictionForce.y = 0;


        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            grounded = false;
        }

        rb.AddForce((newForce + frictionForce));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionStay(Collision other)
    {
        grounded = true;
        if (other.gameObject.tag == "Ice")
        {
            currentSurface = ice;
        }
        else
        {
            currentSurface = ground;
        }
    }

    [System.Serializable]
    class Surface{
        [SerializeField]
        public float friction;
        public Surface(float friction){
            this.friction = friction;
        }
    }
}
