using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public  Transform player;

    [SerializeField]
    private Vector3 cameraPosition;

    [SerializeField]
    private Transform cameraPlayer;

    void Update()
    {
        cameraPlayer.position = player.position + cameraPosition;
    }
}
