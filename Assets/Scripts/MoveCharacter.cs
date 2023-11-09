using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayer;

    [SerializeField] GridObject targetCharacter;

    Pathfinding pathfinding;
    List<PathNode> path;

    [SerializeField] GridHighlight gridHighlight;


    private void Start() 
    {
        pathfinding = targetGrid.GetComponent<Pathfinding>();
        CheckWalkableTerrain();
    }

    void CheckWalkableTerrain()
    {
        List<PathNode> walkableNodes = new List<PathNode>();
        pathfinding.CalculateWalkableNodes(targetCharacter.posOnGrid.x, targetCharacter.posOnGrid.y, targetCharacter.GetComponent<Character>().movementPoints, ref walkableNodes);
        gridHighlight.Highlight(walkableNodes);
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
            }
        }
    }
}
