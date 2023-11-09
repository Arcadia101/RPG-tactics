using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayer;

    Vector2Int currentGridPos = new Vector2Int(-1, -1);

    [SerializeField] GridObject hoveringOver;
    [SerializeField] SelectableGridObject SelectedObject;


    private void Update() 
    {   
        HoverOverObjectCheck();

        SelectObject();
        DeselectObject();
    }

    private void HoverOverObjectCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
        {
            Vector2Int gridPos = targetGrid.GetGridPos(hit.point);
            if (gridPos == currentGridPos)
            {
                return;
            }
            currentGridPos = gridPos;
            GridObject gridObject = targetGrid.GetPlacedObject(gridPos);
            hoveringOver = gridObject;
        }
    }

    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoveringOver == null)
            {
                return;
            }
            SelectedObject = hoveringOver.GetComponent<SelectableGridObject>();
        }
    }
    private void DeselectObject()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectedObject = null;
        }
    }

}
