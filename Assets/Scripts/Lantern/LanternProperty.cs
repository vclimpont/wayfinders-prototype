using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LanternProperty : MonoBehaviour
{
    public abstract void UsePropertyAction(RaycastHit hitInfo);

    public abstract void StopPropertyAction();
}
