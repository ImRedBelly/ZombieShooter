using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = -15;
        transform.position = newPosition;
    }
}
