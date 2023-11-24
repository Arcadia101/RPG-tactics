using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject moveButton;
    CommandInput commandInput;

    SelectCharacter selectCharacter;

    private void Awake() 
    {
        commandInput = GetComponent<CommandInput>();
        selectCharacter = GetComponent<SelectCharacter>();
    }


    public void OpenPanel(CharacterTurn characterTurn)
    {
        selectCharacter.enabled = false;
        panel.SetActive(true);

        if (characterTurn.allegiance != Allegiance.Player)
        {
            attackButton.SetActive(false);
            moveButton.SetActive(false);
        }

        else
        {
            if (characterTurn.canAct)
            {
                attackButton.SetActive(true);
            }
            else
            {
                attackButton.SetActive(false);
            }
            if (characterTurn.canMove)
            {
                moveButton.SetActive(true);
            }
            else
            {
                moveButton.SetActive(false);
            }
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void MoveCommandSelected()
    {
        if (selectCharacter.selected.GetComponent<CharacterTurn>().canMove)
        {
            commandInput.SetCommandType(CommandType.MoveTo);
            commandInput.InitCommand();
            ClosePanel();
        }
    }

    public void AttackCommandSelected()
    {
        if (selectCharacter.selected.GetComponent<CharacterTurn>().canAct)
        {
            commandInput.SetCommandType(CommandType.Attack);
            commandInput.InitCommand();
            ClosePanel();
        }
    }
}
