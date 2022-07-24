using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapHandler : MonoBehaviour
{
    public GameObject Player;
    public int lastId;
    private SavedData savedData;

    public TMP_Text playerHealth;

    void Start()
    {
        savedData = FindObjectOfType<SavedData>();
        lastId = savedData.lastDotId;
        MapPoints[] mapPoints = FindObjectsOfType<MapPoints>();

        for (int i = 0; i < mapPoints.Length; i++)
        {
            if(mapPoints[i].dotId == lastId)
            {
                Player.transform.position = new Vector3(mapPoints[i].transform.position.x, mapPoints[i].transform.position.y + mapPoints[i].transform.localScale.y, Player.transform.position.z);
                mapPoints[i].isCurrent = true;
            }
            if(mapPoints[i].dotId == 0 && lastId != 0)
            {
                mapPoints[i].isCurrent = false;
            }
        }
    }

    private void Update()
    {
        playerHealth.text = savedData.playerHealth.ToString();
    }
}
