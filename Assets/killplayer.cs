using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class killplayer : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var networkObject = other.GetComponent<NetworkObject>();
            if(networkObject.OwnerClientId == 0)
            {
                networkObject.GetComponent<PlayerData>().IncreaseScore2Rpc();
            }
            else 
            {
                networkObject.GetComponent<PlayerData>().IncreaseScore1Rpc();
            }
            
            //respawn player
            networkObject.GetComponent<PlayerData>().RespawnRpc();
        }
    }
}
