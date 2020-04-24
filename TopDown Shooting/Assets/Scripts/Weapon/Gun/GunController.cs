using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold; // 무기를 잡는 부분(GameObject)
    public Gun setGun; // 장착할 Gun

    private Gun equippedGun; // 현재 장착된 Gun

    

    void Start()
    {
        if(setGun != null)
            EquipGun(setGun); // 장착
    }

    public void EquipGun(Gun gunToEquip)
    {
        equippedGun = Instantiate(gunToEquip,weaponHold.position,weaponHold.rotation) as Gun; // (as Gun) 형변환
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if(equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
