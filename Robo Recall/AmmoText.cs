using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoText : MonoBehaviour
{
    public Weapon Weapon;
    public TextMeshProUGUI text;

    void Start()
    {
        UpdateAmmoText();
    }

    void Update()
    {
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        // the ammotext will be updated on the UI
        text.text = $"{Weapon.currentClip}/{Weapon.currentAmmo}";
        text.transform.position = transform.position;
    }
}