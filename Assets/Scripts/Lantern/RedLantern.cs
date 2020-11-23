using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLantern : LanternProperty
{
    private LineRenderer line;
    private Material lineMaterial;

    public override void UsePropertyAction(RaycastHit hitInfo)
    {
        if(hitInfo.transform == null)
        {
            return;
        }

        List<Vector3> hitPoints = CastLaser(transform.position, Vector3.Normalize(hitInfo.point - transform.position));
        DrawLaser(hitPoints);
    }

    public override void StopPropertyAction()
    {
        if(line != null)
        {
            Destroy(line);
        }
    }

    public void SetProperties(Material mat)
    {
        lineMaterial = mat;
    }

    private List<Vector3> CastLaser(Vector3 basePosition, Vector3 baseDirection)
    {
        List<Vector3> hitPoints = new List<Vector3>();
        hitPoints.Add(transform.position);

        Vector3 position = basePosition;
        Vector3 direction = baseDirection;
        bool hitMirror = true;
        int bounces = 0;

        while(hitMirror && bounces < 10)
        {
            RaycastHit hit;
            Physics.Raycast(position, direction, out hit);

            if (hit.transform == null)
            {
                return hitPoints;
            }

            hitPoints.Add(hit.point);

            if (hit.transform.CompareTag("Mirror"))
            {
                position = hit.point;
                direction = GetReflectDirection(direction, hit.normal);
                bounces++;
            }
            else
            {
                hitMirror = false;
            }
        }

        return hitPoints;
    }

    private Vector3 GetReflectDirection(Vector3 direction, Vector3 normal)
    {
        return (normal * Vector3.Dot(direction * -1f, normal) * 2f) + direction;
    }

    private void DrawLaser(List<Vector3> hitPoints)
    {
        if (hitPoints.Count < 2)
        {
            return;
        }

        if(line == null)
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
