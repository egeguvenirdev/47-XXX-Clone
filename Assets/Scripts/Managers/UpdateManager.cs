using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private bool canUpdateGame;

    public void Init()
    {
        canUpdateGame = true;
    }

    public void DeInit()
    {
        canUpdateGame = false;
    }

    void Update()
    {
        if (!canUpdateGame) return;

        ActionManager.Updater?.Invoke(Time.deltaTime);
        CoroutineManager.Tick();
    }
}
