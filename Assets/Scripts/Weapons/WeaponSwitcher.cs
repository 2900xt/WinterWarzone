using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WeaponSwitcher : NetworkBehaviour
{   
    private int maxWeapons;
    public int selectedWeapon;
    // Start is called before the first frame update
    void Awake()
    {
        maxWeapons = transform.childCount;
    }

    void Start()
    {
        if(!IsLocalPlayer) return;
        selectedWeapon = 0;
        UpdateCurrentWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsLocalPlayer) return;
        int previousSelectedWeapon = selectedWeapon;
        List<KeyCode> keys = new() {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0};
        for(int i = 0; i < System.Math.Min(keys.Count, maxWeapons); i++){
            if(Input.GetKeyDown(keys[i])){
                selectedWeapon = i;
            }
        }
        if(previousSelectedWeapon != selectedWeapon){
            UpdateCurrentWeapon();
        }
    }

    void UpdateCurrentWeapon(){
        foreach(Transform w in transform){
            w.gameObject.SetActive(false);
        }
        transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }

}
