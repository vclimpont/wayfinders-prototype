using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLantern : LanternProperty
{
    // Update is called once per frame
    void Update()
    {
        UsePropertyAction();
    }

    protected override void UsePropertyAction()
    {
        Debug.Log("RED LANTERN");
    }
}
