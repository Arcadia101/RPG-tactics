using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMenu : MonoBehaviour
{
    [SerializeField] GameObject turnPanel;
    SelectCharacter selectCharacter;
    void Awake()
    {
        selectCharacter = GetComponent<SelectCharacter>();
    }

    void Update()
    {
        if (selectCharacter.enabled == false)
        {
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            turnPanel.SetActive(true);
        }
    }
}
