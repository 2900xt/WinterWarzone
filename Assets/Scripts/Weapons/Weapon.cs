using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Weapon : NetworkBehaviour
{
    public Transform fpsCam;
    public int ammo = 1000000;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;

    public float timeSinceShot = 0;

    // Update is called once per frame
    void Update()
    {
        if(!IsLocalPlayer) return;
        
        timeSinceShot += Time.deltaTime;
        if(Input.GetButtonDown("Fire1"))
        {
            TryToShoot();
        }
    }

    void TryToShoot(){
        if(timeSinceShot < 1 / fireRate){
            Debug.Log("Too Fast");
            return;
        }
        timeSinceShot = 0f;
        ammo -= 1;
        Shoot();
    }

    public virtual void Shoot(){
        return;
    }
}
