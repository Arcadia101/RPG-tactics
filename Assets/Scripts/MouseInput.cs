using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayer;

    public Vector2Int posOnGrid;
    public bool active;

    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
        {
            active = true;
            Vector2Int hitPos = targetGrid.GetGridPos(hit.point);
            if (hitPos != posOnGrid)
            {
                posOnGrid = hitPos;
            }
        }
        else
        {
            active = false;
        }
    }
}
