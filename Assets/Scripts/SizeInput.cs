using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeInput : MonoBehaviour
{
    private int value = 3;

    public void OnInput(string value)
    {
        //this.value = value;

        int.TryParse(value, out int result);
        this.value = result;
        Debug.Log(value);
    }

    public void OnButtonClick()
    {
        ActionManager.RebuildBySize?.Invoke(value);
    }
}
