using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int handIndex;

    public int type;
    public int number;

    private GameManager gm;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (hasBeenPlayed)
        {
            return;
        }
        hasBeenPlayed = true;
        gm.availableCardSlots[handIndex] = true;
        gm.PutCard(this);
    }
}
