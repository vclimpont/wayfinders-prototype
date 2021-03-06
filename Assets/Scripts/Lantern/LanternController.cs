﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    [Header("Red lantern")]
    public Material redMaterial;
    [Header("Green lantern")]
    public Material greenMaterial;
    public float chargeValue;
    [Header("Purple lantern")]
    public Color purpleColor;
    public float detectRadius;
    [Header("Lantern Controller")]
    public Light lanternLight;
    public float maxIntensity;
    public float fadingSpeed;
    public Material lanternMaterialOff;

    public Material LanternMaterialOn { get; set; }
    public int CurrentLanternPropertyID { get; set; }
    public LanternProperty LanternProperty { get; set; }
    public bool Active { get; set; }

    private MeshRenderer lanternRenderer;
    private Material currentMatFading;

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        lanternRenderer = GetComponent<MeshRenderer>();
        LanternMaterialOn = new Material(lanternRenderer.material);

        lanternLight.intensity = maxIntensity;
        currentMatFading = LanternMaterialOn;

        LanternProperty = gameObject.AddComponent<OrangeLantern>();
        CurrentLanternPropertyID = 1;
    }

    private void FixedUpdate()
    {
        FadeLight();
    }

    private void FadeLight()
    {
        if(lanternLight.isActiveAndEnabled && lanternLight.intensity > 0)
        {
            lanternLight.intensity -= fadingSpeed;
            lanternRenderer.material.Lerp(LanternMaterialOn, lanternMaterialOff, (lanternLight.intensity - maxIntensity) / -maxIntensity);
            currentMatFading = lanternRenderer.material;
        }
    }

    public void SwitchActive()
    {
        Active = !Active;
        lanternLight.enabled = Active;
        lanternRenderer.material = Active ? currentMatFading : lanternMaterialOff;
    }

    public void ReloadLantern(float ratio)
    {
        lanternLight.intensity = Mathf.Min(maxIntensity, lanternLight.intensity + ratio * maxIntensity);
    }

    public void UseLanternProperty(RaycastHit hitInfo)
    {
        if(lanternLight.intensity > 0 && Active)
        {
            LanternProperty.UsePropertyAction(hitInfo);
        }
        else
        {
            LanternProperty.StopPropertyAction();
        }
    }

    public void StopLanternProperty()
    {
        LanternProperty.StopPropertyAction();
    }

    public void UpdateLanternProperty(int lanternProperty)
    {
        StopLanternProperty();
        Destroy(LanternProperty);
        CurrentLanternPropertyID = lanternProperty;

        switch (lanternProperty)
        {
            case 1:
                LanternProperty = gameObject.AddComponent<OrangeLantern>();
                break;
            case 2:
                LanternProperty = gameObject.AddComponent<GreenLantern>();
                ((GreenLantern)LanternProperty).SetProperties(greenMaterial, chargeValue);
                break;
            case 3:
                LanternProperty = gameObject.AddComponent<RedLantern>();
                ((RedLantern)LanternProperty).SetProperties(redMaterial);
                break;
            case 4:
                LanternProperty = gameObject.AddComponent<PurpleLantern>();
                ((PurpleLantern)LanternProperty).SetProperties(purpleColor, detectRadius);
                break;
            default:
                break;
        }
    }

    public void ChangeLightProperties(Color lightColor, Material lanternMaterial)
    {
        lanternLight.color = lightColor;
        LanternMaterialOn = new Material(lanternMaterial);
        currentMatFading = LanternMaterialOn;
    }
}
