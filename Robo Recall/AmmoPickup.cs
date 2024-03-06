using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    // when colliding the ammo will be added
    // the ammo pickup object will be destroyed
    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.gameObject.GetComponent<Weapon>();
        if (weapon)
        {
            weapon.AddAmmo(weapon.maxAmmo);
            Destroy(gameObject);
        }
    }
}
