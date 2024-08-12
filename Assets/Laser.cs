using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private float laserRange;
    private Transform origin;

    public void SetUpLaser(float laserRange, Transform origin)
    {
        this.laserRange = laserRange;
        this.origin = origin;
    }

    public RaycastHit RenderLaser()
    {
        if (lineRenderer == null) return default;

        lineRenderer.SetPosition(0, origin.position);
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.forward, out hit, laserRange))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, origin.position + origin.forward * laserRange);
        }
        return hit;
    }
}
