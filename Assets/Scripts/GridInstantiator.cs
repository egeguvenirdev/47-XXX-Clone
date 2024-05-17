using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInstantiator : MonoBehaviour
{
    [SerializeField] private float gapBetweenGrids = 0.1f;

    private int size = 3;
    private List<GridElement> grids = new();
    private ObjectPooler pooler;

    //Screen Settings
    private float screenSize = 0;
    private float screenRatioMultiplier;
    private float screenRatio;
    private int sizeOfGrid;
    private bool canCheck;
    private float anchorPos;

    private void Update()
    {
        CalculateRatio();
    }

    public void Init()
    {
        pooler = ObjectPooler.Instance;
        CreateGridElements(size);
        canCheck = true;

        ActionManager.RebuildBySize += CreateGridElements;
    }

    public void DeInit()
    {
        ActionManager.RebuildBySize -= CreateGridElements;
    }

    public void CreateGridElements(int size)
    {
        ActionManager.ClearGrids?.Invoke();

        sizeOfGrid = size;

        if (anchorPos % 2 == 1) anchorPos = (size - 1) / 2;
        else anchorPos = size / 2;

        for (int i = 1; i <= size; i++)
        {
            for (int y = 1; y <= size; y++)
            {
                PoolableObjectBase currentGrid = pooler.GetPooledObjectWithType(PoolObjectType.Grid); 
                currentGrid.Init(i, y, gapBetweenGrids);
                currentGrid.gameObject.SetActive(true);
                ActionManager.GridCreated?.Invoke(currentGrid.GetComponent<GridElement>());
            }
        }

        ArrangeElements();
    }

    private void ArrangeElements()
    {
        if (!CalculateRatio()) return;

        ActionManager.SetCamSize?.Invoke(screenRatioMultiplier, anchorPos, screenRatio);
    }

    private bool CalculateRatio()
    {
        //if (screenSize == Screen.width && screenSize == Screen.height) return false;

        screenRatioMultiplier = (sizeOfGrid + (sizeOfGrid + 1) * gapBetweenGrids);

        if (Screen.width < Screen.height)
        {
            screenRatio = (float)Screen.height / (float)Screen.width;
            screenRatioMultiplier = (sizeOfGrid + (sizeOfGrid + 1) * gapBetweenGrids);
        }
        else
        {
            screenRatio = (float)Screen.width / (float)Screen.height;
            screenRatioMultiplier = (sizeOfGrid + (sizeOfGrid + 1) * gapBetweenGrids);
        }

        return true;
    }
}
