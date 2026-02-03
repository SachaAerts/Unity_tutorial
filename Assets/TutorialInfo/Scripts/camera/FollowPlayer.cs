using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public  Transform player;

    [SerializeField]
    private Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Camera>().transform.position = player.position + cameraPosition;
    }

    // Réduire la ligne
}
