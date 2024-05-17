using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridMatchFinder matchFinder;
    [SerializeField] private GridInstantiator instantiator;

    private List<GridElement> grids = new();

    public void Init()
    {
        ActionManager.GridCreated += OnGridCreated;
        ActionManager.ClearGrids += OnClearGrids;
        ActionManager.GridSelected += OnGridSelected;

        instantiator.Init();
    }

    public void DeInit()
    {
        ActionManager.GridCreated -= OnGridCreated;
        ActionManager.ClearGrids -= OnClearGrids;
        ActionManager.GridSelected -= OnGridSelected;

        instantiator.DeInit();
    }

    private void OnGridCreated(GridElement grid)
    {
        grids.Add(grid);
        matchFinder.InitializeGridElements(grids);
    }

    private void OnClearGrids()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].DeInit();
        }

        grids.Clear();
    }

    private void OnGridSelected(GridElement element)
    {
        List<GridElement> neighborGrids = matchFinder.FindAllConnected(element);

        if (neighborGrids.Count >= 3)
        {
            Debug.Log("komsular eslesti");
        }
    }
}
