using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    // hij moet gaan wachten een bepaalde tijd voor dat die de speler volgt.
    // de speler volgen.
    // ontplof timer.
    // ontplof wanneer in de range van de speler.
    [SerializeField] private float Speed;
    [SerializeField] private GameObject Player;

    private void Awake()
    {
        Player = FindObjectOfType<Health>().gameObject;
    }

    private void Update()
    {
        // follows the player and looks at the player
        transform.LookAt(Player.transform.position);
        transform.position = transform.forward * (Speed * Time.deltaTime);
    }
}