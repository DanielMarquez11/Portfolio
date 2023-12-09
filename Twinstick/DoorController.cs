using System;
using UnityEngine;
using UnityEngine.Daniel.Marquez.KeyCard.System;
using System.Collections;

public class DoorController : DoorBehaviour
{
    [SerializeField] protected Renderer doorRenderer;
    [SerializeField] protected Material doorMaterial;


    public override void Start()
    {
        doorMaterial = doorRenderer.material;
        base.Start();
    }

    public override IEnumerator OpenDoorCoroutine()
    {
        isAnimating = true;
        Door.SetBool("Doors", true);
        openDoor.PlayOneShot(openDoor.clip);
        isAnimating = false;
        isDoorOpen = true;
        yield return null;
    }

    public override void UnlockDoor()
    {
        DoorIslocked = false;
        UpdateEmissiveColor();
    }

    private void UpdateEmissiveColor()
    {
        Color emissiveColor = DoorIslocked ? Color.red : Color.green;
        Color finalEmissiveColor = emissiveColor * emissiveIntensity;
        doorMaterial.SetColor("_EmmisionColor", finalEmissiveColor);
        doorMaterial.EnableKeyword("_EMISSION");
    }

    public override IEnumerator CloseDoorCoroutine()
    {
        isAnimating = true;
        // Start de animatie en wacht tot deze klaar is
        Door.SetBool("Doors", false);
        isAnimating = false;
        isDoorOpen = false;
        yield return null;
    }
}