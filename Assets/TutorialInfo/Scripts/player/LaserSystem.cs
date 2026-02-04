using UnityEngine;
public class LaserSystem
{
    public float laserFlashDuration = 0.1f;
    
    public float laserFlashTimer = 0f;

    private LineRenderer lineRenderer;
    
    private Color normalColor = Color.white;
    
    private Color fireColor = Color.red;

    public LaserSystem(GameObject player)
    {
        InitiateLazer(player);
    }

    private void InitiateLazer(GameObject player)
    {
        lineRenderer = player.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.02f;

        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");

        if (shader != null)
        {
            lineRenderer.material = new Material(shader)
            {
                color = normalColor
            };
        }
        else
        {
            Debug.LogError("Shader not found !");
        }
    }

    public void UpdateLaserVisual(GameObject player, float range)
    {
        Vector3 startPoint = player.transform.position;
        Vector3 endPoint = startPoint + player.transform.forward * range;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        if (laserFlashTimer > 0)
        {
            lineRenderer.material.color = fireColor;
            laserFlashTimer -= Time.deltaTime;
        }
        else
        {
            lineRenderer.material.color = normalColor;
        }
    }
}