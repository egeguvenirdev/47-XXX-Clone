using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMatchFinder : MonoBehaviour
{
    private List<GridElement> gridElements;

    public List<GridElement> FindAllConnected(GridElement startElement)
    {
        List<GridElement> connectedElements = new List<GridElement>();
        Queue<GridElement> queue = new Queue<GridElement>();

        if (startElement.OnClicked)
        {
            queue.Enqueue(startElement);
            connectedElements.Add(startElement);
        }

        while (queue.Count > 0)
        {
            GridElement current = queue.Dequeue();

            foreach (GridElement neighbor in GetNeighbors(current))
            {
                if (neighbor.OnClicked && !connectedElements.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    connectedElements.Add(neighbor);
                }
            }
        }

        return connectedElements;
    }

    private List<GridElement> GetNeighbors(GridElement element)
    {
        List<GridElement> neighbors = new List<GridElement>();

        foreach (GridElement e in gridElements)
        {
            if ((e.Height == element.Height && Mathf.Abs(e.Width - element.Width) == 1) ||
                (e.Width == element.Width && Mathf.Abs(e.Height - element.Height) == 1))
            {
                neighbors.Add(e);
            }
        }

        return neighbors;
    }

    public void InitializeGridElements(List<GridElement> elements)
    {
        gridElements = elements;
    }
}