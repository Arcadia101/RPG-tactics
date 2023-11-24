using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    MouseInput mouseInput;
    CommandMenu commandMenu;

    public Character selected;
    GridObject hoverOverGridObject;
    public Character hoverOverCharacter;
    Vector2Int posOnGrid = new Vector2Int(-1, -1);
    [SerializeField] Grid targetGrid;

    void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
        commandMenu = GetComponent<CommandMenu>();
    }


    void Update()
    {
        HoverOverObject();

        SelectInput();
        DeselectInput();
    }

    void SelectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverCharacter != null && selected == null)
            {
                selected = hoverOverCharacter;
            }
        }

        UpdatePanel();
    }

    void DeselectInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;
        }

        UpdatePanel();
    }

    void HoverOverObject()
    {
        if (posOnGrid != mouseInput.posOnGrid)
        {
            posOnGrid = mouseInput.posOnGrid;
            hoverOverGridObject = targetGrid.GetPlacedObject(posOnGrid);
            if (hoverOverGridObject != null)
            {
                hoverOverCharacter = hoverOverGridObject.GetComponent<Character>();
            }

            else
            {
                hoverOverCharacter = null;
            }
        }
    }

    void UpdatePanel()
    {
        if (selected != null)
        {
            commandMenu.OpenPanel(selected.GetComponent<CharacterTurn>());
        }

        else
        {
            commandMenu.ClosePanel();
        }
    }

    public void Deselect()
    {
        selected = null;
    }
}
