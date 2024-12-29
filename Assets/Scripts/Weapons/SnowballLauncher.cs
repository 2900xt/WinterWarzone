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
        GameObject snowball = Instantiate(snowballPrefab, fpsCam.transform.position, fpsCam.transform.rotation);
        snowball.GetComponent<NetworkObject>().Spawn();
        snowball.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * launchStrength, ForceMode.Impulse);
        Debug.Log("Snowball launched by: " + OwnerClientId);
    }
}
