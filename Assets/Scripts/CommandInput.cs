using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    CommandManager commandManager;
    MouseInput mouseInput;
    MoveCharacter moveCharacter;
    CharacterAttack characterAttack;
    SelectCharacter selectCharacter;
    ClearUtility clearUtility;


    void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
        selectCharacter = GetComponent<SelectCharacter>();
        clearUtility = GetComponent<ClearUtility>();
    }

    [SerializeField] CommandType currentCommand;

    bool isInputCommand;

    private void Update() 
    {
        if (isInputCommand == false)
        {
            return;
        }
        
        switch (currentCommand)
        {
            case CommandType.Attack:
            AttackCommandInput();
            break;

            case CommandType.MoveTo:
            MoveCommandInput();
            break;

            default:
            break;
        }
    }

    public void SetCommandType(CommandType commandType)
    {
        currentCommand = commandType;
    }

    public void InitCommand()
    {
        isInputCommand = true;
        switch (currentCommand)
        {   
            case CommandType.Attack:
            HighlightAttackbleTerrain();
            break;

            case CommandType.MoveTo:
            HighlightWalkableTerrain();
            break;

            default:
            break;
        }
    }

    private void AttackCommandInput() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (characterAttack.Check(mouseInput.posOnGrid) == true)
            {
                GridObject gridObject = characterAttack.GetAttackTarget(mouseInput.posOnGrid);
                if (gridObject == null)
                {
                    return;
                }
                commandManager.AddAttackCommand(selectCharacter.selected, mouseInput.posOnGrid, gridObject);
                StopCommandInput();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightAttack();
            //selectCharacter.selected.GetComponent<Movement>().SkipAnimation();
        }
    }

    private void MoveCommandInput() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (moveCharacter.CheckOccupied(mouseInput.posOnGrid) == true)
            {
                return;
            }
            List<PathNode> path = moveCharacter.GetPath(mouseInput.posOnGrid);
            if (path == null)
            {
                return;
            }
            if (path.Count == 0)
            {
                return;
            }
            commandManager.AddMoveCommand(selectCharacter.selected, mouseInput.posOnGrid, path);
            StopCommandInput();
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightMove();
            clearUtility.ClearPathfinging();
            //selectCharacter.selected.GetComponent<Movement>().SkipAnimation();
        }
    }

    public void StopCommandInput()
    {
        selectCharacter.Deselect();
        selectCharacter.enabled = true;
        isInputCommand = false;
    }

    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectCharacter.selected);
    }

    public void HighlightAttackbleTerrain()
    {
        characterAttack.CalculateAttackArea(selectCharacter.selected.GetComponent<GridObject>().posOnGrid, selectCharacter.selected.attackRange);
    }
}
