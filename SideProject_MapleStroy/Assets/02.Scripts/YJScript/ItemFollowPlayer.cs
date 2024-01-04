using UnityEngine;

public class ItemFollowPlayer : MonoBehaviour
{
    public Transform targetPosition;

    void Update()
    {
        transform.position = Vector3.Slerp(
        transform.position, targetPosition.transform.position, 0.06f);
    }
}
