using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private TMP_Text matchValueText;

    public void Init()
    {
        ActionManager.MatchedGrids += OnMatchedGrids;
        matchValueText.text = "Match Count: " + 0;
    }

    public void DeInit()
    {
        ActionManager.MatchedGrids -= OnMatchedGrids;
    }

    private void OnMatchedGrids(int currentValue)
    {
        matchValueText.text = "Match Count: " + currentValue;
    }
}
