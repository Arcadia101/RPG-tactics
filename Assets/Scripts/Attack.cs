using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Physical,
    Magical
}

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

        if (Random.value >= character.accuracy)
        {
            Debug.Log("miss");
            return;
        }

        
        Character target = targetGridObject.GetComponent<Character>();

        if (Random.value <= target.dodge)
        {
            Debug.Log("dodge");
            return;
        }

        int damage = character.damage;

        if (Random.value <= character.criticalChance)
        {
            damage = (int)(damage * character.criticalDamageMultiplicator);
            Debug.Log("Critical");
        }

        switch (character.damageType)
        {
            case DamageType.Physical:
            damage -= target.armor;
            break;
            case DamageType.Magical:
            damage -= target.resistance;
            break;
        }

        if (damage <= 0)
        {
            damage = 1;
        }

        target.TakeDamage(damage);
    }

    public void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
