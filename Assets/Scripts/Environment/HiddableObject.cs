using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddableObject : MonoBehaviour, IHiddable
{
    [SerializeField] private float chargeTime;

    public bool Hidden { get; set; }

    private MeshRenderer mesh;
    private float currentCharge;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        currentCharge = 0;
    }

    private void SetMaterialAlpha(float a)
    {
        Color c = mesh.material.color;
        mesh.material.color = new Color(c.r, c.g, c.b, a);
    }

    public void Hide(bool hide)
    {
        if (hide)
        {
            if (currentCharge >= chargeTime)
            {
                Hidden = true;
                mesh.enabled = false;
            }
        }
        else
        {
            SetMaterialAlpha(1);
            currentCharge = 0;
            Hidden = false;
            mesh.enabled = true;
        }

        LoadHide();
    }

    public void LoadHide()
    {
        if (currentCharge <= chargeTime)
        {
            SetMaterialAlpha(1f - (currentCharge / chargeTime));
            currentCharge += Time.deltaTime;
        }
    }
}
