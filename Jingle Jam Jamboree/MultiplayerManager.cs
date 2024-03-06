using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public int PlayerIndex;
    private PlayerInputManager _playerInputManager;

    private DeathCounter _deathCounter;

    private void Awake()
    {
        _playerInputManager = FindObjectOfType<PlayerInputManager>();
        _deathCounter = FindObjectOfType<DeathCounter>();
    }

    private void UpdateUI()
    {
        if(_deathCounter != null)
        {
            _deathCounter.DeathCounterText = TextMeshProUGUI.FindObjectsOfType<TextMeshProUGUI>();
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        // Register the parent GameObject
        RegisterPlayer(playerInput,PlayerIndex);
        PlayerIndex++;
        CameraFovManager[] cameraFov = FindObjectsOfType<CameraFovManager>();

        for (int i = 0; i < cameraFov.Length; i++)
        {
            if (cameraFov[i] != null)
            {
                cameraFov[i].CheckPlayerCount();
            }
        }
    }

    private void RegisterPlayer(PlayerInput playerInput, int playerIndex)
    {

        GameObject parentObject = playerInput.gameObject.transform.parent.gameObject;

        // Get all children of the parent GameObject
        int childCount = parentObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = parentObject.transform.GetChild(i);
            GameObject childObject = child.gameObject;
            childObject.gameObject.layer = 7 + playerIndex;
            parentObject.gameObject.layer = 7 + playerIndex;
            InputHandler[] inputHandlers = parentObject.GetComponentsInChildren<InputHandler>();
            inputHandlers[0].Horizontal = playerInput.actions.FindAction("Look");
            inputHandlers[1].Horizontal = playerInput.actions.FindAction("Look");
        }
        UpdateUI();
    }

}
