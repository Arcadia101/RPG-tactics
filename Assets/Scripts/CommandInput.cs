using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    CommandManager commandManager;
    MouseInput mouseInput;
    MoveCharacter moveCharacter;
    CharacterAttack characterAttack;

    void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
    }

    [SerializeField] Character selectedCharacter;
    [SerializeField] CommandType currentCommand;

    void Start()
    {
        //HighlightWalkableTerrain();
        HighlightAttackbleTerrain();
        
    }

    private void Update() 
    {
        //MoveCommandInput();
        AttackCommandInput();
    }

    private void AttackCommandInput() {
        if (Input.GetMouseButtonDown(0))
        {
            if (characterAttack.Check(mouseInput.posOnGrid) == true)
            {
                GridObject gridObject = characterAttack.GetAttackTarget(mouseInput.posOnGrid);
                if (gridObject == null)
                {
                    return;
                }
                commandManager.AddAttackCommand(selectedCharacter, mouseInput.posOnGrid, gridObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }

    private void MoveCommandInput() {
        if (Input.GetMouseButtonDown(0))
        {
            List<PathNode> path = moveCharacter.GetPath(mouseInput.posOnGrid);
            if (path == null)
            {
                return;
            }
            if (path.Count == 0)
            {
                return;
            }
            commandManager.AddMoveCommand(selectedCharacter, mouseInput.posOnGrid, path);
        }

        if (Input.GetMouseButtonDown(1))
        {
            selectedCharacter.GetComponent<Movement>().SkipAnimation();
        }
    }

    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectedCharacter);
    }

    public void HighlightAttackbleTerrain()
    {
        characterAttack.CalculateAttackArea(selectedCharacter.GetComponent<GridObject>().posOnGrid, selectedCharacter.attackRange);
    }
}
