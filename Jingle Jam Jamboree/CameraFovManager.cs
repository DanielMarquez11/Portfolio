using Cinemachine;
using UnityEngine;

public class CameraFovManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _camera;
    [SerializeField] private float _DefaultCameraX;
    [SerializeField] private float _DefaultAimX;
    [SerializeField] private float _NewDefaultCameraX;
    [SerializeField] private float _NewDefaultAimX;


    public void CheckPlayerCount()
    {
        if (FindObjectOfType<MultiplayerManager>().PlayerIndex == 2)
        {
            CinemachineComponentBase newDefaultCamera = _camera[0].GetCinemachineComponent(CinemachineCore.Stage.Body);
            CinemachineComponentBase newAimCamera = _camera[1].GetCinemachineComponent(CinemachineCore.Stage.Body);

            if (newDefaultCamera is CinemachineFramingTransposer framingTransposer)
            {
                framingTransposer.m_ScreenX = _NewDefaultCameraX;
            }

            if (newAimCamera is CinemachineFramingTransposer AimframingTransposer)
            {
                AimframingTransposer.m_ScreenX = _NewDefaultAimX;
            }
        }
        else
        {
            CinemachineComponentBase newDefaultCamera = _camera[0].GetCinemachineComponent(CinemachineCore.Stage.Body);
            CinemachineComponentBase newAimCamera = _camera[1].GetCinemachineComponent(CinemachineCore.Stage.Body);

            if (newDefaultCamera is CinemachineFramingTransposer framingTransposer)
            {
                framingTransposer.m_ScreenX = _DefaultCameraX;
            }

            if (newAimCamera is CinemachineFramingTransposer AimframingTransposer)
            {
                AimframingTransposer.m_ScreenX = _DefaultAimX;
            }
        }
    }
}
