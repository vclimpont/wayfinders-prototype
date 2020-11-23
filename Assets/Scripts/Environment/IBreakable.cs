using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

    void Damage(float damageValue);
    void Break();
}
