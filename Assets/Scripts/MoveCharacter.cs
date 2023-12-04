using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] Grid targetGrid;

    Pathfinding pathfinding;

    [SerializeField] GridHighlight gridHighlight;


    private void Awake() 
    {
        pathfinding = targetGrid.GetComponent<Pathfinding>();
    }

    public void CheckWalkableTerrain(Character targetCharacter)
    {
        GridObject gridObject = targetCharacter.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        pathfinding.Clear();
        pathfinding.CalculateWalkableNodes(gridObject.posOnGrid.x, gridObject.posOnGrid.y, targetCharacter.movementPoints, ref walkableNodes);
        gridHighlight.Hide();
        gridHighlight.Highlight(walkableNodes);
    }

    public bool CheckOccupied(Vector2Int posOnGrid)
    {
        return targetGrid.CheckOccupied(posOnGrid);
    }

    public List<PathNode> GetPath(Vector2Int from)
    {
        List<PathNode> path = pathfinding.TraceBackPath(from.x, from.y);

        if (path == null)
        {
            return null;
        }
        if (path.Count == 0)
        {
            return null;
        }

        path.Reverse();
        
        return path;
    }

    private void Update() 
    {
        
                /*
                Vector2Int gridPos = targetGrid.GetGridPos(hit.point);

                //path = pathfinding.FindPath(targetCharacter.posOnGrid.x, targetCharacter.posOnGrid.y, gridPos.x, gridPos.y);
                path = pathfinding.TraceBackPath(gridPos.x, gridPos.y);

                path.Reverse();
                if (path == null)
                {
                    return;
                }
                if (path.Count == 0)
                {
                    return;
                }
                targetCharacter.GetComponent<Movement>().Move(path);
                */
    }
}
