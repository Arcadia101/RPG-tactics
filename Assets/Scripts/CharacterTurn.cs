using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Allegiance
{
    Player,
    Ally,
    Opponent
}

public class CharacterTurn : MonoBehaviour
{
    public Allegiance allegiance;

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
