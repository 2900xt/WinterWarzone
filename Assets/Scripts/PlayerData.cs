using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    public NetworkVariable<float> health;
   
    public override void OnNetworkSpawn()
    {
        if(IsOwner) 
        {
            health = new NetworkVariable<float>(100f);
        }
    }

    public void TakeDamage(float damage)
    {
        if(IsOwner)
        {
            Debug.Log("Took Damage");
            health.Value -= damage;
            if(health.Value <= 0)
            {
                health.Value = 0;
                NetworkManager.Singleton.GetComponent<GameManager>().PlayerDiedServerRpc();
            }
        }
    }
}