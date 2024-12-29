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
            health.SetDirty(true);
        }
    }

    public void TakeDamage(float damage)
    {
        if(IsServer)
        {
            Debug.Log("Took Damage: " + damage);
            Debug.Log("Health: " + health.Value);
            health.Value -= damage;
            if(health.Value <= 0)
            {
                health.Value = 0;
                NetworkManager.Singleton.GetComponent<GameManager>().PlayerDiedServerRpc();
            }
            health.SetDirty(true);
        }
    }
}