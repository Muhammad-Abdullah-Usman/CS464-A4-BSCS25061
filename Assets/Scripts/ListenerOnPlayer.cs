using UnityEngine;

public class ListenerOnPlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    void LateUpdate()
    {
        transform.position = player.position;
    }
}