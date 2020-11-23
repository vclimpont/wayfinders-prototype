using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleLantern : LanternProperty
{
    private GameObject pointObject;
    private Light pointLight;
    private Color lightColor;
    private float detectRadius;

    private Collider[] hitColliders;

    private void FixedUpdate()
    {
        if (hitColliders != null)
        {
            foreach (Collider hit in hitColliders)
            {
                MonoBehaviour hitMB = hit.GetComponent<MonoBehaviour>();
                if (hitMB is IHiddable hitHiddable)
                {
                    hitHiddable.Hide(true);
                }
            }
        }
    }

    public override void UsePropertyAction(RaycastHit hitInfo)
    {
        if(hitInfo.transform == null)
        {
            return;
        }

        hitColliders = Physics.OverlapSphere(hitInfo.point, detectRadius);

        if(pointObject == null)
        {
            pointObject = new GameObject();
            pointObject.transform.position = hitInfo.point;
            if(pointLight == null)
            {
                SetLight();
            }
        }
        SetLight();
    }

    public override void StopPropertyAction()
    {

    }

    public void SetProperties(Color col, float rad)
    {
        lightColor = col;
        detectRadius = rad;
    }

    private void SetLight()
    {
        pointLight = pointObject.AddComponent<Light>();
        pointLight.type = LightType.Point;
        pointLight.color = lightColor;
        pointLight.intensity = 5;
        pointLight.range = detectRadius;
    }
}
