using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Daniel.Marquez.KeyCard.System;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform Player;
    [SerializeField] protected Vector3 DistanceToPlayer;
    public bool DoorIslocked;

    public KeyCard.KeyType doorType;
    public KeyHolder keyHolder;

    public float openingDistance;
    public float ClosingDistance;

    public bool isAnimating = false; // Houdt bij of de deur aan het animeren is
    public bool isDoorOpen = false; // Houdt bij of de deur open is

    protected float emissiveIntensity = 150f;
    [SerializeField] protected Animator Door;

    [SerializeField] protected AudioSource AudioDenied;
    [SerializeField] protected AudioSource AudioAcces;
    [SerializeField] protected AudioSource openDoor;

    protected bool hasKey;

    protected bool audioPlayedAcces;
    private bool isStreetScene;

    public virtual void Start()
    {
        Player = FindObjectOfType<Health>().transform;
        keyHolder = FindObjectOfType<KeyHolder>();
    }

    public virtual void Update()
    {
        DistanceToPlayer = Player.position - transform.position;
        // Controleer of de speler de sleutel heeft opgepakt
        hasKey = keyHolder.keyList.Contains(doorType);

        // Pas de emissieve kleur aan op basis van de vergrendelingsstatus en of de sleutel is opgepakt
        if (DoorIslocked && hasKey)
        {
            UnlockDoor();
        }
        else if (!DoorIslocked && hasKey && DistanceToPlayer.magnitude < openingDistance)
        {
            if (!isStreetScene)
            {
                if (!AudioAcces.isPlaying)
                {
                    if (!audioPlayedAcces)
                    {
                        AudioAcces.PlayOneShot(AudioAcces.clip);
                        audioPlayedAcces = true;
                    }
                }
            }
        }
        else if (DoorIslocked && !hasKey && DistanceToPlayer.magnitude < openingDistance)
        {
            if (!isStreetScene)
            {
                if (!AudioDenied.isPlaying)
                {
                    AudioDenied.PlayOneShot(AudioDenied.clip);
                }
            }
        }


        // Controleer de afstand tot de speler om de deur te openen of te sluiten
        if (DistanceToPlayer.magnitude <= openingDistance)
        {
            if (!DoorIslocked && !isDoorOpen)
            {
                OpenDoor();
            }
        }
        else if (DistanceToPlayer.magnitude >= ClosingDistance && !DoorIslocked && isDoorOpen)
        {
            CloseDoor();
        }
    }


    public virtual void CloseDoor()
    {
        if (!isAnimating && isDoorOpen)
        {
            StartCoroutine(CloseDoorCoroutine());
        }
    }

    public virtual void UnlockDoor()
    {
        DoorIslocked = false;
    }

    public virtual void OpenDoor()
    {
        if (!isAnimating && !isDoorOpen)
        {
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    public virtual IEnumerator OpenDoorCoroutine()
    {
        isAnimating = true;
        // Start de animatie en wacht tot deze klaar is
        Door.SetBool("Doors", true);
        if (!AudioDenied.isPlaying)
        {
            openDoor.PlayOneShot(openDoor.clip);
        }

        yield return new WaitForSeconds(2f);
        isAnimating = false;
        isDoorOpen = true;
    }

    public virtual IEnumerator CloseDoorCoroutine()
    {
        isAnimating = true;
        // Start de animatie en wacht tot deze klaar is
        Door.SetBool("Doors", false);
        yield return new WaitForSeconds(2f);
        isAnimating = false;
        isDoorOpen = false;
    }
}