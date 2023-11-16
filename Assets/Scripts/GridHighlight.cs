using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Grid grid;
    [SerializeField] GameObject highlightPoint;
    [SerializeField] GameObject container;
    List<GameObject> highlightPointsGO;

    void Start()
    {
        grid = GetComponentInParent<Grid>();
        highlightPointsGO = new List<GameObject>();
        
    }

    public void Hide()
    {
        for (int i = 0; i < highlightPointsGO.Count; i++)
        {
            highlightPointsGO[i].SetActive(false);
        }
    }

    public void Highlight(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetHighlightPointGO(i));
        }
        
    }

    public void Highlight(List<PathNode> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].pos_x, positions[i].pos_y, GetHighlightPointGO(i));
        }
        
    }

    private GameObject GetHighlightPointGO(int i)
    {
        if (highlightPointsGO.Count < i)
        {
            return highlightPointsGO[i];
        }

        GameObject newHighlightObject = CreatePointHighlightObject();
        return newHighlightObject;
    }

    void Highlight(int posX, int posY, GameObject highlightObject)
    {
        highlightObject.SetActive(true);
        Vector3 pos = grid.GetWorldPosition(posX, posY, true);
        pos += Vector3.up * 0.2f;
        highlightObject.transform.position = pos;

    }

    private GameObject CreatePointHighlightObject() 
    {
        GameObject go = Instantiate(highlightPoint);
        highlightPointsGO.Add(go);
        go.transform.SetParent(container.transform);
        return go;
    }
}
