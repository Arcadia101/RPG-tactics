using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Grid grid;
    [SerializeField] GameObject movePoint;
    [SerializeField] GameObject movePointsContainer;
    List<GameObject> movePointGOs;
    [SerializeField] List<Vector2Int> targetPos;

    void Start()
    {
        grid = GetComponent<Grid>();
        movePointGOs = new List<GameObject>();
        //Highlight(targetPos);
        
    }

    public void Highlight(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetMovePointGO(i));
        }
        
    }

    public void Highlight(List<PathNode> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].pos_x, positions[i].pos_y, GetMovePointGO(i));
        }
        
    }

    private GameObject GetMovePointGO(int i)
    {
        if (movePointGOs.Count < i)
        {
            return movePointGOs[i];
        }

        GameObject newHighlightObject = CreateMovePointHighlightObject();
        return newHighlightObject;
    }

    void Highlight(int posX, int posY, GameObject highlightObject)
    {
        Vector3 pos = grid.GetWorldPosition(posX, posY, true);
        pos += Vector3.up * 0.2f;
        highlightObject.transform.position = pos;

    }

    private GameObject CreateMovePointHighlightObject() 
    {
        GameObject go = Instantiate(movePoint);
        movePointGOs.Add(go);
        go.transform.SetParent(movePointsContainer.transform);
        return go;
    }
}
