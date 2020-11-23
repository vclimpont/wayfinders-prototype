using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadableObject : MonoBehaviour, ILoadable
{
    [SerializeField] private float maxLoad;
    [SerializeField] private Material loadMaterial;

    public float MaxLoad { get; set; }
    public float CurrentLoad { get; set; }

    private MeshRenderer mesh;
    private Material startMaterial;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLoad = 0;
        MaxLoad = maxLoad;
        mesh = GetComponent<MeshRenderer>();
        startMaterial = new Material(mesh.material);
    }

    private void LerpMaterial()
    {
        mesh.material.Lerp(startMaterial, loadMaterial, CurrentLoad / MaxLoad);
    }

    public void Load(float damageValue)
    {
        if(CurrentLoad >= MaxLoad)
        {
            return;
        }

        CurrentLoad += damageValue;
        LerpMaterial();
    }
}
