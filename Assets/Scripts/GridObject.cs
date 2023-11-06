using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Grid targetGrid;
    public Vector2Int posOnGrid;


    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Init()
    {
        posOnGrid = targetGrid.GetGridPos(transform.position);
        targetGrid.PlaceObject(posOnGrid, this);
        Vector3 pos = targetGrid.GetWorldPosition(posOnGrid.x, posOnGrid.y, true);
        transform.position = pos;
    }
}
