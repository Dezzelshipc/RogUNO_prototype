using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public class Data
    {
        public string key;
        public string name;
        public string description;
        public IActionType action;

        public Data(string _key, string _name, string _description, IActionType _action)
        {
            key = _key;
            name = _name;
            description = _description;
            action = _action;
        }

        public void Action()
        {
            action.Action();
        }
    }

    public interface IActionType
    {
        public void Action();
    }

    public class DamageMultiplier : IActionType
    {
        public GameManager gm;
        public float[] numbers;
        public DamageMultiplier(GameManager _gm, float[] _numbers)
        {
            gm = _gm;
            numbers = _numbers;
        }

        public void Action()
        {
            gm.damageMultiplier += numbers[0];
            foreach (var action in FindObjectsOfType<Action>())
            {
                if (action.tags.ToLower().Contains("e_damage"))
                {
                    for (int i = 0; i < action.numbers.Length; i++)
                        action.numbers[i] = (int)Mathf.Ceil((float)action.numbers[i] * gm.damageMultiplier);
                }
                action.TextUpdate();
            }
        }
    }

    public class AddResist : IActionType
    {
        public GameManager gm;
        public float[] numbers;
        public AddResist(GameManager _gm, float[] _numbers)
        {
            gm = _gm;
            numbers = _numbers;
        }

        public void Action()
        {
            gm.player.resist += (int)numbers[0];
        }
    }

    public class AddMaxTurns : IActionType
    {
        public GameManager gm;
        public float[] numbers;
        public AddMaxTurns(GameManager _gm, float[] _numbers)
        {
            gm = _gm;
            numbers = _numbers;
        }

        public void Action()
        {
            gm.turnsTotal += (int)numbers[0];
        }
    }


    public Dictionary<string, Data> itemsData = new();

    private GameManager gm;

    public void Start()
    {
        Gen();
    }

    public void Gen()
    {
        if (itemsData.Count > 0)
            return;
        gm = FindObjectOfType<GameManager>();
        itemsData.Add("IncMult", new Data("IncMult", "D", "Increases Damage multiplier by +50%", new DamageMultiplier(gm, new float[] { 0.5f })));
        itemsData.Add("StartResist", new Data("StartResist", "R", "Get +1 RES at the start of battle", new AddResist(gm, new float[] { 1f })));
        itemsData.Add("MoreTurns", new Data("MoreTurns", "T", "Add +5 turns for battles", new AddMaxTurns(gm, new float[] { 5f })));
    }
}
