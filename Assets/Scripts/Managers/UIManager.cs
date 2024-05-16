using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Panels")]

    [SerializeField] private TMP_Text timerText;

    public void Init()
    {
        
    }

    public void DeInit()
    {
        
    }

    public void TimerText(string refText)
    {
        timerText.text = refText;
    }
}
