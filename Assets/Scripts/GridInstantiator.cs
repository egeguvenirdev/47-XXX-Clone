using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInstantiator : MonoBehaviour
{
    public int size = 3;
    [SerializeField] private float gapBetweenGrids = 0.1f;
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
        if (!canCheck) return;
        ArrangeElements();
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
        if (grids.Count > 0) ResetTheList();

        sizeOfGrid = size;

        if (anchorPos % 2 == 1) anchorPos = (size - 1) / 2;
        else anchorPos = size / 2;

        for (int i = 1; i <= size; i++)
        {
            for (int y = 1; y <= size; y++)
            {
                PoolableObjectBase currentGrid = pooler.GetPooledObjectWithType(PoolObjectType.Grid);
                grids.Add(currentGrid.GetComponent<GridElement>());
                currentGrid.Init(i, y);
                currentGrid.gameObject.SetActive(true);
            }
        }


    }

    private void ArrangeElements()
    {
        if (!CalculateRatio()) return;

        ActionManager.SetCamSize?.Invoke(screenRatioMultiplier, anchorPos, screenRatio);

        for (int i = 0; i < grids.Count; i++)
        {
            Vector3 gridPos = new Vector3(
                (grids[i].Height - 1) + (grids[i].Height - 1) * gapBetweenGrids,
                5,
                (grids[i].Width - 1) + (grids[i].Width - 1) * gapBetweenGrids);

            grids[i].transform.position = gridPos;
        }
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


        //screenRatio = (screenSize / 1000f) * (sizeOfGrid + (sizeOfGrid + 1) * gapBetweenGrids);

        return true;
    }

    private void ResetTheList()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].DeInit();
        }

        grids.Clear();
    }
}
