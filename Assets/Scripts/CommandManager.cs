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
    Command currentComand;

    CommandInput commandInput;

    private void Start()
    {
        commandInput = GetComponent<CommandInput>();
    }

    private void Update() 
    {
        if (currentComand != null)
        {
            ExecuteCommand();
        }
    }

    public void ExecuteCommand()
    {
        //MovementCommandExecute();
        AttackCommandExecute();
    }

    public void MovementCommandExecute()
    {
        Character receiver = currentComand.character;
        receiver.GetComponent<Movement>().Move(currentComand.path);
        currentComand = null;
        commandInput.HighlightWalkableTerrain();
    }

    public void AttackCommandExecute()
    {
        Character receiver = currentComand.character;
        receiver.GetComponent<Attack>().AttackPos(currentComand.target);
        currentComand = null;
        commandInput.HighlightWalkableTerrain();
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentComand = new Command(character, selectedGrid, CommandType.MoveTo);
        currentComand.path = path;
    }

    public void AddAttackCommand(Character attaker, Vector2Int selectedGrid, GridObject target)
    {
        currentComand = new Command(attaker, selectedGrid, CommandType.Attack);
        currentComand.target = target;
    }

    public void AddCommand(Character character, Vector2Int selectedGrid, CommandType commandType)
    {
        currentComand = new Command(character, selectedGrid, commandType);
    }
}
