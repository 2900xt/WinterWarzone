using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public Rigidbody rb;

    private static Surface ice = new Surface(0.8f);
    private static Surface ground = new Surface(10.0f);
    private static Surface currentSurface = ground;

    public float speed = 10.0f; 
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
            newForce.z += speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newForce.z -= speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newForce.x -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newForce.x += speed;
        }

        rb.AddForce((newForce + frictionForce));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
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
