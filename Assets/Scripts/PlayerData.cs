using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    public float health;
   
    public override void OnNetworkSpawn()
    {
        health = 100f;
    }

    private void HandleHealthChanged(float oldHealth, float newHealth)
    {
        Debug.Log($"Health changed from {oldHealth} to {newHealth}");
    }


    [Rpc(SendTo.ClientsAndHost)]
    public void RespawnRpc(RpcParams rpcParams = default)
    {
        Debug.Log("Respawning player " + OwnerClientId);
        GetComponent<PlayerMotor>().OnNetworkSpawn();
        health = 100f;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void TakeDamageRpc(RpcParams rpcParams = default)
    {
        health -= 10;
        SoundManager.PlaySoundRpc(SoundType.HIT_SNOWBALL, 1f);
    }

    

    [Rpc(SendTo.Server)]
    public void CollisionHandleServerRpc(ulong OwnerClientId, ulong collisionID, RpcParams rpcParams = default)
    {
        Debug.Log("Snowball hit player" + OwnerClientId);
        TakeDamageRpc();
        
        var networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[collisionID];

        if(health <= 0)
        {
            Debug.Log("Player " + OwnerClientId + " died");
            if(OwnerClientId == 0)
            {
                IncreaseScore2Rpc();
            }
            else 
            {
                IncreaseScore1Rpc();
            }
            
            //respawn player
            RespawnRpc();
        }
        
        networkObject.Despawn(true);
    }

    
    [Rpc(SendTo.ClientsAndHost)]
    public void IncreaseScore1Rpc(RpcParams rpcParams = default)
    {
        GameManager.Instance.score1++;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void IncreaseScore2Rpc(RpcParams rpcParams = default)
    {
        GameManager.Instance.score2++;
    }
}