using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] Transform marker;

    MouseInput mouseInput;

    Vector2Int currentPos;
    bool active;
    [SerializeField] Grid targetGrid;
    [SerializeField] float elevation = 2f; 

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }

    private void Update()
    {
        if (active != mouseInput.active)
        {
            active = mouseInput.active;
            marker.gameObject.SetActive(active);
        }

        if (active == false)
        {
            return;
        }

        if (currentPos != mouseInput.posOnGrid)
        {
            currentPos = mouseInput.posOnGrid;
            UpdateMarker();
        }
    
    }

    private void UpdateMarker()
    {
        if (targetGrid.CheckBoundry(currentPos) == false)
        {
            return;
        }
        Vector3 worldPos = targetGrid.GetWorldPosition(currentPos.x, currentPos.y, true);
        worldPos.y += elevation;
        marker.position = worldPos;
    }
}
