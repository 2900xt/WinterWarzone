using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SnowballScript : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if(IsOwner) Invoke(nameof(Despawn), 8f);
    }
    public void OnTriggerEnter(Collider col)
    {
        if(IsOwner)
        {
            ulong objOwner = col.gameObject.GetComponent<NetworkObject>().OwnerClientId;
            if(col.gameObject.CompareTag("Player") && objOwner != OwnerClientId)
            {
                PlayerData playerData = col.gameObject.GetComponent<PlayerData>();
                playerData.TakeDamage(10);
                GetComponent<NetworkObject>().Despawn();
            }
        }
    }

    public void Despawn()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
