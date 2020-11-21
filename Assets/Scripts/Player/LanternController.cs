using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public Light lanternLight;
    public MeshRenderer lanternRenderer;
    public Material lanternMaterialOn;
    public Material lanternMaterialOff;
    public float maxIntensity;
    public float fadingSpeed;

    public bool Active { get; set; }

    private Material currentMat;

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        lanternLight.intensity = maxIntensity;
        currentMat = lanternMaterialOn;
    }

    // Update is called once per frame
    void Update()
    {

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
            lanternRenderer.material.Lerp(lanternMaterialOn, lanternMaterialOff, (lanternLight.intensity - maxIntensity) / -maxIntensity);
            currentMat = lanternRenderer.material;
        }
    }

    public void SwitchActive()
    {
        Active = !Active;
        lanternLight.enabled = Active;
        lanternRenderer.material = Active ? currentMat : lanternMaterialOff;
    }
}
