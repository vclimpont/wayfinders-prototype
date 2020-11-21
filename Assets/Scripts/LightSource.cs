using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public Light sourceLight;
    public int sourceProperty;
    public Color sourceColor;
    public Material sourceMaterial;

    private void Start()
    {
        sourceLight.color = sourceColor;
        GetComponent<MeshRenderer>().material = sourceMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.ChangeLanternProperty(sourceProperty, sourceColor, sourceMaterial);
        }
    }
}
