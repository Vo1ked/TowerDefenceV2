using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIWindow 
{
    string Name { get; }
    void SetWindow(bool isEnable);

}
