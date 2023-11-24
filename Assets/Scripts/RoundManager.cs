using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    private void Awake() 
    {
        instance = this;
    }

    [SerializeField] ForceContainer playerForceContainer;
    [SerializeField] ForceContainer opponentForceContainer;
    [SerializeField] ForceContainer allyForceContainer;

    int round = 1;

    [SerializeField] TMPro.TextMeshProUGUI turnCountText;
    [SerializeField] TMPro.TextMeshProUGUI forceRoundText;

    private void Start() 
    {
        UpdateTextOnScreen();
    }

    public void AddMe(CharacterTurn character)
    {
        if (character. allegiance == Allegiance.Player)
        {
            playerForceContainer.AddMe(character);
        }

        if (character. allegiance == Allegiance.Opponent)
        {
            opponentForceContainer.AddMe(character);
        }

        if (character. allegiance == Allegiance.Ally)
        {
            allyForceContainer.AddMe(character);
        }
    }

    Allegiance currentTurn;

    public void NextTurn()
    {
        switch (currentTurn)
        {
            case Allegiance.Player:
            currentTurn = Allegiance.Opponent;
            break;
            case Allegiance.Opponent:
            currentTurn = Allegiance.Ally;
            break;
            case Allegiance.Ally:
            NextRound();
            currentTurn = Allegiance.Player;
            break;
        }

        GrantTurnToForce();

        UpdateTextOnScreen();
    }

    public void GrantTurnToForce()
    {
        switch (currentTurn)
        {
            case Allegiance.Player:
            playerForceContainer.GrantTurn();
            break;
            case Allegiance.Opponent:
            opponentForceContainer.GrantTurn();
            break;
            case Allegiance.Ally:
            allyForceContainer.GrantTurn();
            break;
        }
    }

    public void NextRound()
    {
        round += 1;
    }

    void UpdateTextOnScreen()
    {
        turnCountText.text = "turn: " + round.ToString();
        forceRoundText.text = currentTurn.ToString();
    }
}
