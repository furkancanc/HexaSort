using System;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private LayerMask hexagonLayerMask;
    private HexStack currentStack;
    private Vector3 currentStackInitialPosition;
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
        else if (Input.GetMouseButton(0))
        {
            ManageMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ManageMouseUp();
        }
    }

    private void ManageMouseDown()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, hexagonLayerMask);

        if (hit.collider == null)
        {
            Debug.Log("We have not detected any hexagon");
            return;
        }

        currentStack = hit.collider.GetComponent<Hexagon>().HexStack;
        currentStackInitialPosition = currentStack.transform.position;
    }

    private void ManageMouseUp()
    {
        
    }

    private void ManageMouseDrag()
    {
        
    }

    
}
