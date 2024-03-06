using UnityEngine;

public class SetCameraLayers : MonoBehaviour
{
    [SerializeField] private LayerMask _Player1Culling;
    [SerializeField] private LayerMask _Player2Culling;
    [SerializeField] private LayerMask _Player3Culling;
    [SerializeField] private LayerMask _Player4Culling;
    [SerializeField] private Camera _Camera;
    private MultiplayerManager _playerManager;
    
    void Start()
    {
        _playerManager = FindObjectOfType<MultiplayerManager>();
        
        if (_playerManager.PlayerIndex == 1)
        {
            _Camera.cullingMask = _Player1Culling;
        }
        if (_playerManager.PlayerIndex == 2)
        {
            _Camera.cullingMask = _Player2Culling;
        }
        if (_playerManager.PlayerIndex == 3)
        {
            _Camera.cullingMask = _Player3Culling;
        }
        if (_playerManager.PlayerIndex == 4)
        {
            _Camera.cullingMask = _Player4Culling;
        }
    }
}
