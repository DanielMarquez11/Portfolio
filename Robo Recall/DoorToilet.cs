using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToilet : DoorBehaviour
{
    public override void Start()
    {
        base.Start();
    }

    public override IEnumerator OpenDoorCoroutine()
    {
        isAnimating = true;
        // Start de animatie en wacht tot deze klaar is
        LeanTween.rotateY(gameObject, 100, 2);
        yield return new WaitForSeconds(2f);
        isAnimating = false;
        isDoorOpen = true;
    }

    public override IEnumerator CloseDoorCoroutine()
    {
        isAnimating = true;
        // Start de animatie en wacht tot deze klaar is
        LeanTween.rotateY(gameObject, -1.1f, 2).setEase(LeanTweenType.easeOutElastic);
        yield return new WaitForSeconds(2f);
        isAnimating = false;
        isDoorOpen = false;
    }
}