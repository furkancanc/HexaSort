using System;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private LayerMask hexagonLayerMask;
    [SerializeField] private LayerMask gridHexagonLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    private HexStack currentStack;
    private Vector3 currentStackInitialPosition;

    [Header(" Data ")]
    private GridCell targetCell;

    private void Update()
    {
        ManageControl();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManageMouseDown();
        }
        else if (Input.GetMouseButton(0) && currentStack != null)
        {
            ManageMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0) && currentStack != null)
        {
            ManageMouseUp();
        }
    }
    private void ManageMouseUp()
    {
        if (targetCell == null)
        {
            currentStack.transform.position = currentStackInitialPosition;
            currentStack = null;
            return;
        }

        currentStack.transform.position = targetCell.transform.position.With(y: .2f);
        currentStack.transform.SetParent(targetCell.transform);
        currentStack.Place();

        targetCell.AssignStack(currentStack);

        targetCell = null;
        currentStack = null;
    }

    private void ManageMouseDrag()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500, gridHexagonLayerMask);

        if (hit.collider == null)
        {
            DraggingAboveGround();
        }
        else
        {
            DraggingAboveGridCell(hit);
        }
    }

    private void ManageMouseDown()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500, hexagonLayerMask);

        if (hit.collider == null)
        {
            Debug.Log("We have not detected any hexagon");
            return;
        }

        currentStack = hit.collider.GetComponent<Hexagon>().HexStack;
        currentStackInitialPosition = currentStack.transform.position;
    }
    private void DraggingAboveGround()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500, groundLayerMask);

        if (hit.collider == null)
        {
            Debug.LogError("No ground detected, this is unusual...");
        }

        Vector3 currentStackTargetPosition = hit.point.With(y: 2);
        currentStack.transform.position = Vector3.MoveTowards(
            currentStack.transform.position,
            currentStackTargetPosition, 
            Time.deltaTime * 30);

        targetCell = null;
    }

    private void DraggingAboveGridCell(RaycastHit hit)
    {
        GridCell gridCell = hit.collider.GetComponent<GridCell>();

        if (gridCell.IsOccupied)
        {
            DraggingAboveGround();
        }
        else
        {
            DragginAboveNonOccupiedGridCell(gridCell);
        }
    }

    private void DragginAboveNonOccupiedGridCell(GridCell gridCell)
    {
        Vector3 currentStackTargetPosition = gridCell.transform.position.With(y: 2);
        currentStack.transform.position = Vector3.MoveTowards(
           currentStack.transform.position,
           currentStackTargetPosition,
           Time.deltaTime * 30);

        targetCell = gridCell;
    }

    private Ray GetClickedRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
    
}
