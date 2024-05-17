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

    public override void Init(int height, int width, float gapBetweenGrids)
    {
        this.Height = height;
        this.Width = width;

        SetPos(gapBetweenGrids);
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
            ActionManager.GridSelected?.Invoke(this);
            return;
        }

        OnClicked = false;
        sprite.enabled = false;
    }

    public void OnMatch()
    {
        OnClicked = false;
        sprite.enabled = false;
    }

    private void SetPos(float gapBetweenGrids)
    {
        Vector3 gridPos = new Vector3(
                (Height - 1) + (Height - 1) * gapBetweenGrids,
                5,
                (Width - 1) + (Width - 1) * gapBetweenGrids);

        transform.position = gridPos;
    }
}
