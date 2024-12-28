using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{   
    private int maxWeapons;
    public int selectedWeapon;
    // Start is called before the first frame update
    void Awake()
    {
        maxWeapons = transform.childCount;
    }

    void Start(){
        selectedWeapon = 0;
        UpdateCurrentWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCurrentWeapon(){
        int i = 0;
        foreach(Transform w in transform){
            Debug.Log(w.name);
        }
    }

}
