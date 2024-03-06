using UnityEngine;

public class FollowPlayerTutorialText : MonoBehaviour
{
    [SerializeField] private Camera _Camera;

    private void Update()
    {
        Vector3 direction = transform.position - _Camera.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
}