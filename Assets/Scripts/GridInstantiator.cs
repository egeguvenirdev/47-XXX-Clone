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
    private float screenRatio;
    private int sizeOfGrid;
    private bool canCheck;

    private IEnumerator Start()
    {
        yield return CoroutineManager.GetTime(1f, 10f);

        pooler = ObjectPooler.Instance;
        CreateGridElements(size);
        canCheck = true;
    }

    private void Update()
    {
        if (!canCheck) return;
        ArrangeElements();
    }

    public void CreateGridElements(int size)
    {
        sizeOfGrid = size;

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

        Debug.Log(screenRatio);
        ActionManager.SetCamSize?.Invoke(screenRatio);

        for (int i = 0; i < grids.Count; i++)
        {
            Vector3 gridPos = new Vector3(
                (grids[i].height - 1) + (grids[i].height - 1) * gapBetweenGrids,
                5,
                (grids[i].width - 1) + (grids[i].width - 1) * gapBetweenGrids);

            grids[i].transform.position = gridPos;
        }
    }

    private bool CalculateRatio()
    {
        if (screenSize == Screen.width || screenSize == Screen.height) return false;

        if (Screen.width < Screen.height) screenSize = Screen.width;
        else screenSize = Screen.height;

        screenRatio = (screenSize / 1000f) * (sizeOfGrid + ((sizeOfGrid - 1) * gapBetweenGrids));

        return true;
    }
}
