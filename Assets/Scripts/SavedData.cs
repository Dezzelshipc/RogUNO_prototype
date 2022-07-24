using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public List<string> items = new();
    public int lastDotId = 0;
    public int playerHealth = 30;
    public int playerHealthMax = 30;

    public int enemyHealth = 30;
    public int enemyDamage = 5;

    public bool isLast = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
