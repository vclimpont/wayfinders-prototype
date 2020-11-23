using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddableObject : MonoBehaviour, IHiddable
{
    public bool Hidden { get; set; }

    public void Hide(bool hide)
    {
        if(Hidden == hide)
        {
            return;
        }

        Hidden = hide;
        gameObject.SetActive(!Hidden);
    }
}
