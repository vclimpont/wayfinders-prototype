using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLantern : LanternProperty
{
    private LineRenderer line;
    private Material lineMaterial;
    private float chargeValue;

    public override void UsePropertyAction(RaycastHit hitInfo)
    {
        if (hitInfo.transform == null)
        {
            return;
        }

        List<Vector3> laserPoints = CastLaser(hitInfo.point);
        DrawLaser(laserPoints);
    }

    public override void StopPropertyAction()
    {
        if (line != null)
        {
            Destroy(line);
        }
    }

    public void SetProperties(Material mat, float c)
    {
        lineMaterial = mat;
        chargeValue = c;
    }

    private List<Vector3> CastLaser(Vector3 targetPoint)
    {
        Vector3 laserStart = transform.position;
        Vector3 laserEnd = targetPoint;
        List<Vector3> laserPoints = new List<Vector3>();

        laserPoints.Add(laserStart);
        for (float i = 0.1f; i < 1; i += 0.1f)
        {
            Vector3 i_laser = (laserStart * i) + (laserEnd * (1f - i));
            Vector3 r = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            laserPoints.Add(i_laser + r);
        }
        laserPoints.Add(laserEnd);

        return laserPoints;
    }

    private void DrawLaser(List<Vector3> hitPoints)
    {
        if (hitPoints.Count < 2)
        {
            return;
        }

        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.material = lineMaterial;
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
        }
        line.positionCount = hitPoints.Count;
        line.SetPositions(hitPoints.ToArray());
    }
}
