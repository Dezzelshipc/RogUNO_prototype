using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card> ();
    public List<Card> discardPile = new List<Card> ();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public Transform discardSlot;
    public Card lastCard;
    public Transform[] actionsSlots;

    public Enemy enemy;
    public Player player;
    public Transform[] itemsSlots;
    public SavedData savedData;

    public int turnsTotal = 10;
    public int turnsRemaining = 10;

    public int[] points = new int[4];
    public float damageMultiplier = 1;

    public ItemsData itemsData;
    public ActionsData actionsData;

    public TMP_Text deckSizeText;
    public TMP_Text discardSizeText;
    public TMP_Text pointsText;
    public TMP_Text turnsText;

    public TMP_Text textOverPrefab;

    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range (0,deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i])
                {
                    randCard.gameObject.SetActive (true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].transform.position;
                    randCard.hasBeenPlayed = false;
                    availableCardSlots[i] = false;
                    deck.Remove (randCard);
                    return;
                }
            }
        }
        else
        {
            Shuffle();
            DrawCard();
        }
    }

    public void Shuffle()
    {
        if (discardPile.Count > 0)
        {
            deck.AddRange(discardPile);
            discardPile.Clear();
        }
    }

    public void PutCard(Card card)
    {
        if (CheckType(card))
            points[card.type] += card.number;
        lastCard.gameObject.SetActive (false);

        if (lastCard.number >= 0)
            discardPile.Add(lastCard);

        lastCard = card;
        card.transform.position = discardSlot.transform.position;

        DrawCard();
        TurnAction();
    }

    public bool CheckType(Card card)
    {
        return card.type == -1 || lastCard.type == -1 || card.type == lastCard.type || card.number == lastCard.number;
    }

    public void Points(Action action)
    {
        for (int i = 0; i < action.cost.Length; i++)
        {
            points[i] -= action.cost[i];
        }
        TurnAction();
    }

    private void TurnAction()
    {
        turnsRemaining--;
        if (turnsRemaining == 0)
        {
            enemy.Action();
            turnsRemaining = turnsTotal;
        }
    }

    public void Transition()
    {
        savedData.playerHealth = player.health;
        if (!savedData.isLast)
            SceneManager.LoadScene("GetItems");
        else
            SceneManager.LoadScene("Win");
    }

    private void ItemsShow()
    {
        for (int i = 0; i < savedData.items.Count; i++)
        {
            if (itemsData.itemsData.TryGetValue(savedData.items[i], out ItemsData.Data action))
                action.Action();
            else
            {
                Debug.Log("Items table error");
                return;
            }

            TMP_Text text = Instantiate(textOverPrefab);
            text.fontSize = 8;
            text.alignment = TextAlignmentOptions.Center;
            text.text = action.name;
            text.transform.position = itemsSlots[i].transform.position;

            text.GetComponent<MouseOver>().showedText = action.description;
        }
    }

    private void ActionsShow()
    {
        GameObject ationPrefab = actionsData.actionPrefab;
        int i = 0;
        foreach (var action in actionsData.actionsData.Values)
        {
            GameObject actionObject = Instantiate(ationPrefab);
            actionObject.transform.position = actionsSlots[i].transform.position;
            actionObject.name = action.name;
            actionObject.GetComponent<Action>().Name = action.name;
            actionObject.GetComponent<Action>().description = action.description;
            actionObject.GetComponent<Action>().tags = action.tags;
            actionObject.GetComponent<Action>().numbers = action.numbers;
            actionObject.GetComponent<Action>().cost = action.cost;
            actionObject.GetComponent<Action>().actionType = action.actionType;


            actionObject.GetComponent<Action>().TextUpdate();

            i++;
        }
    }

    private void UpdateActionsNumbers()
    {
        foreach (var action in FindObjectsOfType<Action>())
        {
            for(int i = 0; i < action.numbers.Length; i++)
            {
                action.numbers[i] = (int)action.numbers[i];
            }
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        savedData = FindObjectOfType<SavedData>();

        ActionsShow();

        ItemsShow();

        UpdateActionsNumbers();

        turnsRemaining = turnsTotal;
        lastCard.transform.position = discardSlot.transform.position;
        lastCard.gameObject.SetActive(true);

        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i])
            {
                Card randCard = deck[Random.Range(0, deck.Count)];
                randCard.gameObject.SetActive(true);
                randCard.handIndex = i;

                randCard.transform.position = cardSlots[i].transform.position;
                randCard.hasBeenPlayed = false;
                availableCardSlots[i] = false;
                deck.Remove(randCard);
            }
        }
    }

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardSizeText.text = discardPile.Count.ToString();
        pointsText.text = $"{points[0]}\n{points[1]}\n{points[2]}\n{points[3]}";
        turnsText.text = turnsRemaining.ToString();
    }
}
