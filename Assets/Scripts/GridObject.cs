using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] Grid targetGrid;


    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Init()
    {
        Vector2Int posOnGrid = targetGrid.GetGridPos(transform.position);
        targetGrid.PlaceObject(posOnGrid, this);
    }
}
