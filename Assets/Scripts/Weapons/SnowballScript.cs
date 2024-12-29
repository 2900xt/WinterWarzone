using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SnowballScript : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if(IsServer) 
        {
            Invoke(nameof(Despawn), 8f);
        }
    }
    
    public void Despawn()
    {
        GetComponent<NetworkObject>().Despawn();
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            ulong objOwner = col.GetComponent<NetworkObject>().OwnerClientId;
            if(objOwner != OwnerClientId)
            {
                col.GetComponent<PlayerData>().CollisionHandleServerRpc(OwnerClientId, GetComponent<NetworkObject>().NetworkObjectId);
            }
        }
    }
}