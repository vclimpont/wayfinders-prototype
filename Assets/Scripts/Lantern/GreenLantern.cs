using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLantern : LanternProperty
{
    // Update is called once per frame
    void Update()
    {
        UsePropertyAction();
    }

    protected override void UsePropertyAction()
    {
        Debug.Log("GREEN LANTERN");
    }
}
