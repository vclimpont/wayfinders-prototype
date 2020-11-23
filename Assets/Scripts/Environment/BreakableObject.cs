using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakable
{
    [SerializeField] private float health;
    [SerializeField] private Material breakMaterial;

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    private MeshRenderer mesh;
    private Material startMaterial;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        CurrentHealth = MaxHealth;
        mesh = GetComponent<MeshRenderer>();
        startMaterial = new Material(mesh.material);
    }

    private void LerpMaterial()
    {
        mesh.material.Lerp(startMaterial, breakMaterial, 1f - (CurrentHealth / MaxHealth));
    }

    public void Damage(float damageValue)
    {
        CurrentHealth -= damageValue;
        LerpMaterial();
        
        if(CurrentHealth <= 0)
        {
            Break();
        }
    }

    public void Break()
    {
        Destroy(gameObject);
    }
}
