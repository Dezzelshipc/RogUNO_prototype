using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySavedData : MonoBehaviour
{
    void Start()
    {
        SavedData savedData = FindObjectOfType<SavedData>();
        Destroy(savedData.gameObject);
    }
}
