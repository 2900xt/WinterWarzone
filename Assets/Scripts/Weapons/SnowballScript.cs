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
}