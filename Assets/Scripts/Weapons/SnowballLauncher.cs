using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballLauncher : MonoBehaviour
{
    public GameObject snowballPrefab;
    public float launchStrength;

    void Update(){
        if(Input.GetKeyDown("Fire11")){
            ShootSnowball();
        }
    }

    void ShootSnowball(){
        GameObject snowball = Instantiate(snowballPrefab, transform.position, transform.rotation);
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * launchStrength, ForceMode.Impulse);
    }
}
