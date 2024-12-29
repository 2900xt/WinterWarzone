using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SnowballLauncher : Weapon
{
    public GameObject snowballPrefab;
    public float launchStrength;

    public override void Shoot()
    {
        ShootServerRpc(fpsCam.transform.position, fpsCam.transform.forward);
    }

    [Rpc(SendTo.Server)]
    public void ShootServerRpc(Vector3 position, Vector3 forward, RpcParams rpcParams = default)
    {
        GameObject snowball = Instantiate(snowballPrefab, position, Quaternion.identity);
        snowball.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
        Vector3 launchForce = forward * launchStrength;
        snowball.GetComponent<Rigidbody>().AddForce(launchForce, ForceMode.Impulse);
    }
}
