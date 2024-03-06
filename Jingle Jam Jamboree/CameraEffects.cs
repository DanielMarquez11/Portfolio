using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour
{
    public float DefaultCamFOV = 43;

    private CinemachineVirtualCamera _cam;
    private bool _startElapsedTwo = false;

    private void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    public IEnumerator CameraZoom(float target, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            _cam.m_Lens.FieldOfView = Mathf.Lerp(DefaultCamFOV, target, elapsed / duration);
            elapsed += Time.deltaTime;
        }

        if (elapsed >= duration)
        {
            _startElapsedTwo = true;
        }

        float elapsedTwo = 0.0f;

        while (elapsedTwo < duration && _startElapsedTwo)
        {
            _cam.m_Lens.FieldOfView = Mathf.Lerp(target, DefaultCamFOV, elapsedTwo / duration);
            elapsedTwo += Time.deltaTime;
            yield return null;
        }
    }
}
