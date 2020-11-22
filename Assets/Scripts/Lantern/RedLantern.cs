using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLantern : LanternProperty
{

    public override void UsePropertyAction(Vector3 mousePosition)
    {
        Debug.Log("KABOOM");
        CastLaser(transform.position, mousePosition);
    }

    private void CastLaser(Vector3 lanternPosition, Vector3 targetPosition)
    {
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, lanternPosition);
        line.SetPosition(1, targetPosition);

        StopAllCoroutines();
        StartCoroutine(DestroyLine(line));
    }

    IEnumerator DestroyLine(LineRenderer line)
    {
        yield return new WaitForSeconds(2f);

        Destroy(line);
    }
}
