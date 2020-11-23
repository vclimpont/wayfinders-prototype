using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoadable
{
    float MaxLoad { get; set; }
    float CurrentLoad { get; set; }

    void Load(float loadValue);
}
