using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PlayerControllers : MonoBehaviour
{
    [Header("Systems")]
    private PlayerStatistics playerStatistics;
    private LaserSystem laserSystem;

    [Header("Combat State")]
    private bool isFiring = false;
    private float lastFireTime = 0f;

    [Header("Movement State")]
    private Vector2 moveInput = Vector2.zero;

    void Start()
    {
        playerStatistics = new();
        laserSystem = new(gameObject);
    }

    void Update()
    {
        if (moveInput.sqrMagnitude > 0)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            transform.Translate(moveDirection.normalized * playerStatistics.Speed * Time.deltaTime);
        }

        if (isFiring && Time.time - lastFireTime >= playerStatistics.fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
        }

        laserSystem.UpdateLaserVisual(gameObject, playerStatistics.Range);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Ennemy"))
        {
            playerStatistics.TakeDamages();
        }      
    }

    private void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, playerStatistics.Range))
        {
            if (hit.collider.CompareTag("Ennemy"))
            {
                Ennemy ennemy = hit.collider.GetComponent<Ennemy>();
                ennemy.TakeDamages(playerStatistics.Damages);
            }
        }

        laserSystem.laserFlashTimer = laserSystem.laserFlashDuration;
    }
    
    #region Inputs Actions Callbacks

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isFiring = true;
            Shoot();
            lastFireTime = Time.time;
        }
        else if (ctx.canceled)
        {
            isFiring = false;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    #endregion
}