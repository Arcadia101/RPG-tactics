using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommandType
{
    MoveTo,
    Attack
}

public class Command
{
    public Character character;
    public Vector2Int selectedGrid;
    public CommandType commandType;
    public List<PathNode> path;
    public GridObject target;

    public Command(Character character, Vector2Int selectedGrid, CommandType commandType)
    {
        this.character = character;
        this.selectedGrid = selectedGrid;
        this.commandType = commandType;
    }
}

public class CommandManager : MonoBehaviour
{
    ClearUtility clearUtility;

    Command currentCommand;

    CommandInput commandInput;

    private void Awake() 
    {
        clearUtility = GetComponent<ClearUtility>();
    }

    private void Start()
    {
        commandInput = GetComponent<CommandInput>();
    }

    private void Update() 
    {
        if (currentCommand != null)
        {
            ExecuteCommand();
        }
    }

    public void ExecuteCommand()
    {
        switch (currentCommand.commandType)
        {
            case CommandType.Attack:
            AttackCommandExecute();
            break;

            case CommandType.MoveTo:
            MovementCommandExecute();
            break;

            default:
            break;
        }
    }

    private void MovementCommandExecute()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<Movement>().Move(currentCommand.path);
        receiver.GetComponent<CharacterTurn>().canMove = false;
        currentCommand = null;
        clearUtility.ClearPathfinging();
        clearUtility.ClearGridHighlightMove();
    }

    private void AttackCommandExecute()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<Attack>().AttackGridObject(currentCommand.target);
        receiver.GetComponent<CharacterTurn>().canAct = false;
        currentCommand = null;
        clearUtility.ClearGridHighlightAttack();
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.MoveTo);
        currentCommand.path = path;
    }

    public void AddAttackCommand(Character attaker, Vector2Int selectedGrid, GridObject target)
    {
        currentCommand = new Command(attaker, selectedGrid, CommandType.Attack);
        currentCommand.target = target;
    }

    public void AddCommand(Character character, Vector2Int selectedGrid, CommandType commandType)
    {
        currentCommand = new Command(character, selectedGrid, commandType);
    }
}
