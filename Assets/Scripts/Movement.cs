using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;

    List<Vector3> pathWorldpos;
    
    [SerializeField] float moveSpeed = 1f;

    private void Awake() 
    {
        gridObject = GetComponent<GridObject>();
    }

    public void Move(List<PathNode> path)
    {
        pathWorldpos = gridObject.targetGrid.ConvertPathNodesToWorldPos(path);

        gridObject.posOnGrid.x = path[path.Count - 1].pos_x;
        gridObject.posOnGrid.y = path[path.Count - 1].pos_y;
    }

    private void Update()
    {
        if (pathWorldpos == null)
        {
            return;
        }
        if (pathWorldpos.Count == 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, pathWorldpos[0], moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathWorldpos[0]) < 0.05f)
        {
            pathWorldpos.RemoveAt(0);
        }

    }
}
