using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : PoolableObjectBase
{
    [SerializeField] private SpriteRenderer sprite;
    private int height;
    private int width;
    private bool onClicked;

    public int Height { get => height; set => height = value; }
    public int Width { get => width; set => width = value; }
    public bool OnClicked { get => onClicked; set => onClicked = value; }

    public override void Init(int height, int width)
    {
        this.Height = height;
        this.Width = width;
    }

    public void DeInit()
    {
        sprite.enabled = false;
        height = 0;
        width = 0;
        gameObject.SetActive(false);
    }

    public void OnGridClicked()
    {
        if (!OnClicked)
        {
            OnClicked = true;
            sprite.enabled = true;
            return;
        }

        OnClicked = false;
        sprite.enabled = false;
    }
}
