using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GridObject gridObject;
    //CharacterAnimator characterAnimator;
    Character character;
    
    void Awake()
    {
        character = GetComponent<Character>();
        gridObject = GetComponent<GridObject>();
        //characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void AttackGridObject(GridObject targetGridObject)
    {
        RotateCharacter(targetGridObject.transform.position);
        //characterAnimator.Attack();
        targetGridObject.GetComponent<Character>().TakeDamage(character.damage);
    }

    public void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
