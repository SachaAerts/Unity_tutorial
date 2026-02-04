using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseAiming: MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseAiming();
    }

    private void HandleMouseAiming()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);

            Vector3 direction = targetPoint - transform.position;
            direction.y = 0;

            RotatePlayer(direction);
        }
    }

    private void RotatePlayer(Vector3 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = targetRot;
        }
    }
}