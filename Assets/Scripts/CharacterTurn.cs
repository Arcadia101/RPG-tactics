using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurn : MonoBehaviour
{
    public bool canAct;
    public bool canMove;

    private void Start() 
    {
        AddToRoundManager();
        GrantTurn();
    }

    public void GrantTurn()
    {
        canMove = true;
        canAct = true;
    }

    public void AddToRoundManager()
    {
        RoundManager.instance.AddMe(this);
    }
}
