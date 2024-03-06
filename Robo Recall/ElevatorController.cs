using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : DoorBehaviour
{
    [Header("Elevator Left and Right")] public GameObject[] Doors;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override IEnumerator OpenDoorCoroutine()
    {
        isAnimating = true;
        // Start de animatie en wacht tot deze klaar is
        LeanTween.moveLocalZ(Doors[1], 1.36f, 2);
        LeanTween.moveLocalZ(Doors[0], -1.36f, 2);
        isAnimating = false;
        isDoorOpen = true;
        yield return null;
    }

    public override IEnumerator CloseDoorCoroutine()
    {
        isAnimating = true;
        // Start de animatie en wacht tot deze klaar is
        LeanTween.moveLocalZ(Doors[1], 0.3f, 2);
        LeanTween.moveLocalZ(Doors[0], -0.3f, 2);
        isAnimating = false;
        isDoorOpen = false;
        yield return null;
    }
}