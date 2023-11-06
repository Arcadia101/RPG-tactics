using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    public int width = 25;
    public int length = 25;
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask terrainLayer;
    private void Awake() 
    {
        GenerateGrid();
        
    }

    public List<Vector3> ConvertPathNodesToWorldPos(List<PathNode> path)
    {
        List<Vector3> worldPos = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            worldPos.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
        }

        return worldPos;
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

        CalculateElevation();
        CheckPassable();
    }

    private void CalculateElevation()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Ray ray = new Ray(GetWorldPosition(x,y) + Vector3.up * 100f, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
                {
                    grid[x,y].elevation = hit.point.y;
                }

            }
        }
    }

    private void CheckPassable()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPos = GetWorldPosition(x,y);
                bool passable = !Physics.CheckBox(worldPos, Vector3.one/2 * cellSize, Quaternion.identity, obstacleLayer);
                //grid[x,y] = new Node();
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

    internal bool CheckBoundry(int posX, int posY)
    {
        if (posX < 0 || posX >= length)
        {
            return false;
        }
        if (posY < 0 || posY >= width)
        {
            return false;
        }
        return true;
    }

    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return grid[pos_x,pos_y].passable;
    }

    public Vector2Int GetGridPos(Vector3 worldPos)
    {
        worldPos.x += cellSize / 2;
        worldPos.z += cellSize / 2;
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
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y);
                    Gizmos.DrawCube(pos, Vector3.one/4);
                }
            }
        }
        else
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y, true);
                    Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one/4);
                }
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y, bool elevation = false)
    {
        return new Vector3(transform.position.x + (x * cellSize), elevation == true ? grid[x,y].elevation : 0f, transform.position.z + (y * cellSize));
        //return new Vector3(x * cellSize, 0f, y * cellSize);
    }
}

