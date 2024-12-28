using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneGun : MonoBehaviour
{   
    public Camera fpsCam;
    public float damage = 10f;
    public float range = 100f;
    public int candyCaneCount = 10;
    public bool aiming = false;

    public GameObject shootEffect;

    // Update is called once per frame
    void Update()
    {
        aiming = Input.GetMouseButton(1);
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot(){
        if(candyCaneCount <= 0){
            Debug.Log("No Candy Canes");
            return;
        }
        candyCaneCount -= 1;

        Vector3 start = aiming? fpsCam.transform.position : transform.position;
        Vector3 direction = aiming? fpsCam.transform.forward : transform.forward;
        RaycastHit hit;


        if(Physics.Raycast(start, direction, out hit, range)){
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null){
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(shootEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }
}
