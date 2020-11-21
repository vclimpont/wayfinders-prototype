using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public Light lanternLight;
    public float maxIntensity;
    public float fadingSpeed;
    public Material lanternMaterialOff;

    public Material LanternMaterialOn { get; set; }
    public int CurrentLanternProperty { get; set; }
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

    public void ReloadLantern()
    {
        lanternLight.intensity = maxIntensity;
    }
}
