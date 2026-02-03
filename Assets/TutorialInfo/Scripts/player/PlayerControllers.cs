using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class Player : MonoBehaviour
{
    private PlayerStatistics playerStatistics;

    private LineRenderer lineRenderer;

    private Vector2 moveInput = Vector2.zero;

    void Start()
    {
        playerStatistics = new();
        InitiateLazer();
    }

    void Update()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + transform.forward * playerStatistics.Range;

        if (moveInput.sqrMagnitude > 0)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            transform.Translate(moveDirection.normalized * playerStatistics.Speed * Time.deltaTime);
        }
        
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Ennemy"))
        {
            playerStatistics.TakeDamages();
        }      
    }

    private void Raycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, playerStatistics.Range))
        {
            if (hit.collider.CompareTag("Ennemy"))
            {
                Ennemy ennemy = hit.collider.GetComponent<Ennemy>();
                ennemy.TakeDamages(playerStatistics.Damages);
            }
        }
    }

    private void InitiateLazer()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.02f;

        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");

        if (shader != null)
        {
            lineRenderer.material = new Material(shader)
            {
                color = Color.red
            };
        }
        else
        {
            Debug.LogError("Shader not found !");
        }
    }
    
    #region Inputs Actions Callbacks

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if(ctx.started) 
        {
            Raycast();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    #endregion
}