using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayer;

    Pathfinding pathfinding;
    Vector2Int currentPosition = new Vector2Int();
    List<PathNode> path;


    private void Start() 
    {
        pathfinding = targetGrid.GetComponent<Pathfinding>();
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
            {
                Vector2Int gridPos = targetGrid.GetGridPos(hit.point);

                path = pathfinding.FindPath(currentPosition.x, currentPosition.y, gridPos.x, gridPos.y);
                currentPosition = gridPos;
                /*
                GridObject gridObject = targetGrid.GetPlacedObject(gridPos);
                if (gridObject == null)
                {
                    Debug.Log("x= " + gridPos.x + " y= " + gridPos.y + " is empty");
                }
                else
                {
                    Debug.Log("x= " + gridPos.x + " y= " + gridPos.y + gridObject.GetComponent<Character>().Name);
                }
                */
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (path == null)
        {
            Debug.Log("on draw is Null");
            return;
        }
        if (path.Count == 0)
        {
            Debug.Log("on draw is 0");
            return;
        }
        for (int i = 0; i < path.Count -1 ; i++)
        {
            Debug.Log("on draw is Drawing");
            Debug.DrawLine(targetGrid.GetWorldPosition(path[i].pos_x, path[i].pos_y, true), targetGrid.GetWorldPosition(path[i + 1].pos_x, path[i + 1].pos_y, true));
        }
    }
}
