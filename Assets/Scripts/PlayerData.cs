using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    private float health;

    public float Health{
        get => health;
        set => health = value;
    }

    public override void OnNetworkSpawn()
    {
        health = 100f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(IsServer)
        {
            if(collision.gameObject.CompareTag("Bullet"))
            {
                health -= 10f;
                if(health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
