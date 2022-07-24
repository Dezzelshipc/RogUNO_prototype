using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Action : MonoBehaviour
{
    public string Name;
    public string description;
    public int[] cost = new int[4];
    public float[] numbers;
    public string tags;

    public ActionsData.IActionType actionType;

    public void DoAction()
    {
        actionType.Action(numbers);
    }

    private GameManager gm;
    public TMP_Text costText;
    public TMP_Text descriptionText;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        TextUpdate();
        costText.text = $"{cost[0]}\n{cost[1]}\n{cost[2]}\n{cost[3]}";
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < cost.Length; i++)
        {
            if (cost[i] > gm.points[i])
                return;
        }
        gm.Points(this);
        DoAction();
    }

    public void TextUpdate()
    {
        costText.text = $"{cost[0]}\n{cost[1]}\n{cost[2]}\n{cost[3]}";
        object[] _numbers = new object[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            _numbers[i] = numbers[i];
        }
        descriptionText.text = string.Format(description, _numbers);
    }
}
