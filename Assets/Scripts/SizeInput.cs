using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeInput : MonoBehaviour
{
    private int value = 3;

    public void OnInputInt(int value)
    {
        this.value = value;
    }

    public void OnButtonClick()
    {
        ActionManager.RebuildBySize?.Invoke(value);
    }
}
