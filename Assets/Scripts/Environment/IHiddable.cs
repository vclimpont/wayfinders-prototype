using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHiddable
{
    bool Hidden { get; set; }

    void Hide(bool hide);
}
