using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Int2Val
{
    public int current;
    public int max;

    public bool canGoNegative;

    public Int2Val(int current, int max)
    {
        this.current = current;
        this.max = max;
    }

    internal void Subtract(int amount)
    {
        current -= amount;

        if (canGoNegative == false)
        {
            if (current < 0)
            {
                current = 0;
            }
        }
    }


}

public class Character : MonoBehaviour 
{
    public string Name = "Nameless";
    public float movementPoints = 50f;
    public Int2Val health = new Int2Val(100,100);
    public int attackRange = 1;
    public int damage = 20;

    public bool defeated;

    CharacterAnimator characterAnimator;

    private void Start() 
    {
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    public void TakeDamage(int damage)
    {
        health.Subtract(damage);
        Debug.Log("HP: " + health.current);
    }

    public void CheckDefeat()
    {
        if (health.current <= 0)
        {
            Defeated();
        }
        else
        {
            Flinch();
        }
    }

    public void Flinch()
    {
        //characterAnimator.Flinch();
    }

    public void Defeated()
    {
        defeated = true;
        //characterAnimator.Defeated();
    }
}