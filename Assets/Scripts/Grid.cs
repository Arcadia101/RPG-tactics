using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    [SerializeField] int width = 25;
    [SerializeField] int length = 25;
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    private void Awake() 
    {
        GenerateGrid();
        
    }

    private void GenerateGrid()
    {
        grid = new Node[length, width];

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                grid[x,y] = new Node();
            }
        }

        CheckPassable();
    }

    private void CheckPassable()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPos = GetWorldPosition(x,y);
                bool passable = !Physics.CheckBox(worldPos, Vector3.one/2 * cellSize, Quaternion.identity, obstacleLayer);
                grid[x,y] = new Node();
                grid[x,y].passable = passable;
            }
        }
    }

    public bool CheckBoundry(Vector2Int posOnGrid)
    {
        if (posOnGrid.x < 0 || posOnGrid.x >= length)
        {
            return false;
        }
        if (posOnGrid.y < 0 || posOnGrid.y >= width)
        {
            return false;
        }
        return true;
    }

    public Vector2Int GetGridPos(Vector3 worldPos)
    {
        worldPos -= transform.position;
        Vector2Int posOnGrid = new Vector2Int((int)(worldPos.x/cellSize), (int)(worldPos.z/cellSize));
        return posOnGrid;
    }

    public void PlaceObject(Vector2Int posOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(posOnGrid) == true)
        {
            grid[posOnGrid.x, posOnGrid.y].gridObject = gridObject;
        }
        else
        {
            Debug.Log("out of limits");
        }
        
    }

    public GridObject GetPlacedObject(Vector2Int gridpos)
    {
        if (CheckBoundry(gridpos) == true)
        {
            GridObject gridObject = grid[gridpos.x, gridpos.y].gridObject;
            return gridObject;
        }
        return null;
        
        
    }


    private void OnDrawGizmos() 
    {
        if (grid == null)
        {
            return;
        }
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 pos = GetWorldPosition(x, y);
                Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                Gizmos.DrawCube(pos, Vector3.one/4);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(transform.position.x + (x * cellSize), 0f, transform.position.z + (y * cellSize));
    }
}

