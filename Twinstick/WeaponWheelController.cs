using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    private void Start()
    {
        weaponWheelSelected = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = true;
        }
        else
        {
            weaponWheelSelected = false;
        }

        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch (weaponID)
        {
            case 0: // nothing is selected
                selectedItem.sprite = noImage;
                break;
            case 1: // Pistol
                Debug.Log("Pistol");
                break;
            case 2: // Shotgun
                Debug.Log("Shotgun");
                break;
            case 3: // grenade
                Debug.Log("Grenade");
                break;
        }
    }
}