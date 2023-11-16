using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] GridHighlight highlight;
    List<Vector2Int> attackPos;

    public void CalculateAttackArea(Vector2Int characterPosOnGrid, int attackRange, bool selfTargetable = false)
    {
        if (attackPos == null)
        {
            attackPos = new List<Vector2Int>();
        }

        else
        {
            attackPos.Clear();
        }
        
        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int y = -attackRange; y <= attackRange; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) >  attackRange)
                {
                    continue;
                }
                if (selfTargetable == false)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                }
                if (targetGrid.CheckBoundry(characterPosOnGrid.x + x, characterPosOnGrid.y + y) == true)
                {
                    attackPos.Add(new Vector2Int(characterPosOnGrid.x + x, characterPosOnGrid.y + y));
                }
            }
        }

        highlight.Highlight(attackPos);
    }

    public GridObject GetAttackTarget(Vector2Int posOnGrid)
    {
        GridObject target = targetGrid.GetPlacedObject(posOnGrid);
        return target;
    }

    public bool Check(Vector2Int posOnGrid)
    {
        return attackPos.Contains(posOnGrid);
    }

    /*private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
            {
                Vector2Int gridPos = targetGrid.GetGridPos(hit.point);

                if (attackPos.Contains(gridPos))
                {
                    GridObject gridObject = targetGrid.GetPlacedObject(gridPos);
                    if (gridObject == null)
                    {
                        return;
                    }
                    selectedCharacter.GetComponent<Attack>().AttackPos(gridObject);
                }
            }
        }
    }*/
}
