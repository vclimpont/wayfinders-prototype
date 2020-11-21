using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public Light lanternLight;
    public MeshRenderer lanternRenderer;
    public Material lanternMaterialOn;
    public Material lanternMaterialOff;
    public float fadingSpeed;

    public bool Active { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Active = true;
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
        }
    }

    public void SwitchActive()
    {
        Active = !Active;
        lanternLight.enabled = Active;
        lanternRenderer.material = Active ? lanternMaterialOn : lanternMaterialOff;
    }
}
