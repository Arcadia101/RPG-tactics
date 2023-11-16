using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GridObject gridObject;
    
    void Awake()
    {
        gridObject = GetComponent<GridObject>();
    }

    public void AttackPos(GridObject targetGridObject)
    {
        RotateCharacter(targetGridObject.transform.position);
    }

    public void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
