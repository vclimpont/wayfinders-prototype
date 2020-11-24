using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleLantern : LanternProperty
{
    private GameObject pointObject;
    private Light pointLight;
    private Color lightColor;
    private float detectRadius;

    private Collider[] hitCollidersBfr;
    private Collider[] hitColliders;

    private Vector3 hitPoint;

    public override void UsePropertyAction(RaycastHit hitInfo)
    {
        if(hitInfo.transform == null)
        {
            return;
        }

        ShowLanternLight(hitInfo);

        hitColliders = Physics.OverlapSphere(hitInfo.point, detectRadius);
        hitPoint = hitInfo.point;

        HideObjects();
        RestoreObjects();
        hitCollidersBfr = hitColliders;
    }

    public override void StopPropertyAction()
    {
        HideObjects(false);
        Destroy(pointObject);
        hitColliders = null;
        hitCollidersBfr = null;
    }

    public void SetProperties(Color col, float rad)
    {
        lightColor = col;
        detectRadius = rad;
    }

    private void HideObjects(bool hide = true)
    {
        if (hitColliders != null)
        {
            foreach (Collider hit in hitColliders)
            {
                MonoBehaviour hitMB = hit.GetComponent<MonoBehaviour>();
                if (hitMB is IHiddable hitHiddable)
                {
                    hitHiddable.Hide(hide);
                }
            }
        }
    }

    private void RestoreObjects()
    {
        if(hitCollidersBfr != null)
        {
            foreach (Collider hitBfr in hitCollidersBfr)
            {
                if(!Contains(hitColliders, hitBfr))
                {
                    MonoBehaviour hitMB = hitBfr.GetComponent<MonoBehaviour>();
                    if (hitMB is IHiddable hitHiddable)
                    {
                        hitHiddable.Hide(false);
                    }
                }
            }
        }
    }

    private bool Contains(Collider[] colls, Collider c)
    {
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].Equals(c))
            {
                return true;
            }
        }
        return false;
    }

    private void ShowLanternLight(RaycastHit hitInfo)
    {
        if (pointObject == null)
        {
            pointObject = new GameObject();
        }
        pointObject.transform.position = hitInfo.point + (hitInfo.normal * 2f);
        if (pointLight == null)
        {
            SetLight();
        }
    }

    private void SetLight()
    {
        pointLight = pointObject.AddComponent<Light>();
        pointLight.type = LightType.Point;
        pointLight.color = lightColor;
        pointLight.intensity = 5;
        pointLight.range = detectRadius * 2f;
        pointLight.renderMode = LightRenderMode.ForcePixel;
        pointLight.bounceIntensity = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint, detectRadius);
    }
}
