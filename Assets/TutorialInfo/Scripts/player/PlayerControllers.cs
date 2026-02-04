using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PlayerControllers : MonoBehaviour
{
    [Header("Systems")]
    private LaserSystem laserSystem;

    [Header("Player stats")]
    private PlayerStatistics playerStatistics;

    [Header("Combat State")]
    private bool isFiring = false;
    private float lastFireTime = 0f;

    [Header("Movement State")]
    private Vector2 moveInput = Vector2.zero;

    [Header("Camera")]
    [SerializeField] private Transform playerCamera;

    void Start()
    {
        playerStatistics = new();
        laserSystem = new(gameObject);
    }

    void Update()
    {
        HandleMovement();

        if (isFiring && Time.time - lastFireTime >= playerStatistics.FireRate)
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
            LifePlayer.TakeDamages();
        }      
    }

    private void HandleMovement()
    {
        if (moveInput.sqrMagnitude > 0)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            
            cameraForward.y = 0;
            cameraRight.y = 0;
            
            cameraForward.Normalize();
            cameraRight.Normalize();
            
            Vector3 moveDirection = cameraRight * moveInput.x + cameraForward * moveInput.y;
            
            transform.position += playerStatistics.Speed * Time.deltaTime * moveDirection.normalized;
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, playerStatistics.Range))
        {
            if (hit.collider.CompareTag("Ennemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.TakeDamages(playerStatistics.Damages);

                VerifEnemyDeath(enemy);
            }
        }

        laserSystem.laserFlashTimer = laserSystem.laserFlashDuration;
    }

    private void VerifEnemyDeath(Enemy enemy)
    {
        if (enemy.IsPlayerKilledEnemy())
        {
            ScorePlayer.IncrementScore();
        }
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