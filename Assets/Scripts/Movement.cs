using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;
    //CharacterAnimator characterAnimator;

    List<Vector3> pathWorldpos;

    public bool IS_MOVING
    {
        get
        {
            if (pathWorldpos == null)
            {
                return false;
            }
            return pathWorldpos.Count > 0;
        }
    }
    
    [SerializeField] float moveSpeed = 1f;

    private void Awake() 
    {
        gridObject = GetComponent<GridObject>();
        //characterAnimator = GetComponentInChild<CharacterAnimator>();
    }

    public void Move(List<PathNode> path)
    {
        if (IS_MOVING)
        {
            SkipAnimation();
        }
        pathWorldpos = gridObject.targetGrid.ConvertPathNodesToWorldPos(path);

        gridObject.targetGrid.RemoveObject(gridObject.posOnGrid, gridObject);

        gridObject.posOnGrid.x = path[path.Count - 1].pos_x;
        gridObject.posOnGrid.y = path[path.Count - 1].pos_y;

        gridObject.targetGrid.PlaceObject(gridObject.posOnGrid, gridObject);

        RotateCharacter(transform.position, pathWorldpos[0]);

        //characterAnimator.StartMoving();
    }

    public void RotateCharacter(Vector3 originPos, Vector3 destinationPos)
    {
        Vector3 direction = (destinationPos - originPos).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
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
            if (pathWorldpos.Count == 0)
            {
                //characterAnimator.StopMoving();
            }
            else
            {
                RotateCharacter(transform.position, pathWorldpos[0]);
            }
        }

    }

    public void SkipAnimation()
    {
        if (pathWorldpos.Count < 2)
        {
            return;
        }
        transform.position = pathWorldpos[pathWorldpos.Count - 1];
        Vector3 originPos = pathWorldpos[pathWorldpos.Count - 2];
        Vector3 destinationPos = pathWorldpos[pathWorldpos.Count - 1];
        RotateCharacter(originPos, destinationPos);
        pathWorldpos.Clear();
        //characterAnimator.StopMoving();
    }
}
