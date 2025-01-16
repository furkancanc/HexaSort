using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private void Awake()
    {
        StackController.onStackPlaced += StackPlacedCallback;
    }

    private void OnDestroy()
    {
        StackController.onStackPlaced -= StackPlacedCallback;
    }

    private void StackPlacedCallback(GridCell gridCell)
    {
        // Does this cell has neighbors?
        LayerMask gridCellMask = 1 << gridCell.gameObject.layer;

        List<GridCell> neighborGridCells = new List<GridCell>();

        Collider[] neighborGridCellColliders = Physics.OverlapSphere(gridCell.transform.position, 2, gridCellMask);

        // At this point, we have the grid cell collider neighbors
        foreach (Collider gridCellCollider in neighborGridCellColliders)
        {
            GridCell neighborGridCell = gridCellCollider.GetComponent<GridCell>();

            if (neighborGridCell.IsOccupied) continue;

            if (neighborGridCell == gridCell) continue;

            neighborGridCells.Add(neighborGridCell);
        }

        // At this point, we have a list of the neighbor grid cells, that are occupied
        Color gridCellTopHexagonColor = gridCell.Stack.GetTopHexagonColor();

        Debug.Log(gridCellTopHexagonColor);
        // Do these neighbors have the same top hex color?

        // We need to merge !

        // Merge everything inside of this cell

        // Is the stack on this cell complete?
        // Does it have 10 or more similar hexagons?

        // Check the updated cells ?
        // Repeat


    }
}
