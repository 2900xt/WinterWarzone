using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneGun : Weapon
{      

    public GameObject shootEffect;


    public override void Shoot(){

        Vector3 start = fpsCam.transform.position;
        Vector3 direction = fpsCam.transform.forward;
        RaycastHit hit;


        if(Physics.Raycast(start, direction, out hit, range)){
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null){
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(shootEffect, hit.point, Quaternion.LookRotation(hit.normal) * Quaternion.Euler(90, 0, 0));
            Destroy(impact, 2f);
        }
    }
}
