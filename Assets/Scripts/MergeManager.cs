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

            if (!neighborGridCell.IsOccupied) continue;

            if (neighborGridCell == gridCell) continue;

            neighborGridCells.Add(neighborGridCell);
        }

        if (neighborGridCells.Count <= 0)
        {
            Debug.Log("No neighbors for this cell");
            return;
        }

        // At this point, we have a list of the neighbor grid cells, that are occupied
        Color gridCellTopHexagonColor = gridCell.Stack.GetTopHexagonColor();

        List<GridCell> similarNeighborGridCells = new List<GridCell>();

        foreach (GridCell neighborGridCell in neighborGridCells)
        {
            Color neighborGridCellTopHexagaonColor = neighborGridCell.Stack.GetTopHexagonColor();

            if (gridCellTopHexagonColor == neighborGridCellTopHexagaonColor)
            {
                similarNeighborGridCells.Add(neighborGridCell);
            }
        }

        if (similarNeighborGridCells.Count <= 0)
        {
            Debug.Log("No similar neighbors for this cell");
            return;
        }

        // At this point, we have a list of similar neighbors
        List<Hexagon> hexagonsToAdd = new List<Hexagon>();

        foreach (GridCell neighborCell in similarNeighborGridCells)
        {
            HexStack neighborCellHexStack = neighborCell.Stack;

            for (int i = neighborCellHexStack.Hexagons.Count - 1; i >= 0; --i)
            {
                Hexagon hexagon = neighborCellHexStack.Hexagons[i];
                if (hexagon.Color != gridCellTopHexagonColor)
                {
                    break;
                }

                hexagonsToAdd.Add(hexagon);
                hexagon.SetParent(null);
            }
        }

        // Remove the hexagons from their stacks
        foreach (GridCell neighborCell in similarNeighborGridCells)
        {
            HexStack stack = neighborCell.Stack;

            foreach (Hexagon hexagon in hexagonsToAdd)
            {
                if (stack.Contains(hexagon))
                {
                    stack.Remove(hexagon);
                }
            }
        }

        // At this point, we have removed the stacks we don't need anymore
        // We have some free grid cells

        float initialY = gridCell.Stack.Hexagons.Count * .2f;

        for (int i = 0; i < hexagonsToAdd.Count; ++i)
        {
            Hexagon hexagon = hexagonsToAdd[i];

            float targetY = initialY + i * .2f;
            Vector3 targetLocalPosition = Vector3.up * targetY;

            gridCell.Stack.Add(hexagon);
            hexagon.transform.localPosition = targetLocalPosition;

        }

        // Do these neighbors have the same top hex color?

        // We need to merge !

        // Merge everything inside of this cell

        // Is the stack on this cell complete?
        // Does it have 10 or more similar hexagons?

        // Check the updated cells ?
        // Repeat


    }
}
