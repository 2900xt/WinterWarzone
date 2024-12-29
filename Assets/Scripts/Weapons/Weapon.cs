using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Weapon : NetworkBehaviour
{
    public Camera fpsCam;
    public int ammo = 100;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;

    public float timeSinceShot = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        
        timeSinceShot += Time.deltaTime;
        if(Input.GetButtonDown("Fire1")){
            TryToShoot();
        }
    }

    void TryToShoot(){
        if(timeSinceShot < 1 / fireRate){
            Debug.Log("Too Fast");
            return;
        }
        timeSinceShot = 0f;
        if(ammo <= 0){
            Debug.Log("Out Of Ammo");
            return;
        }
        ammo -= 1;
        Shoot();
    }

    public virtual void Shoot(){
        return;
    }
}
