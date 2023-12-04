using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRandomization : MonoBehaviour
{
    [SerializeField] List<GameObject> parts;
    [SerializeField] List<float> probablility;

    private void Start() 
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if (probablility[i] >= Random.value)
            {
                parts[i].SetActive(false);
            }
        }
    }
}
