using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ChooseItems : MonoBehaviour
{
    public Transform[] oddSlots;
    public Transform[] evenSlots;

    public ItemsData itemsData;
    public SavedData savedData;

    public Dictionary<string, ItemsData.Data> itemsDataDict;

    public TMP_Text textOverPrefab;
    public TMP_Text textNewPrefab;

    void Start()
    {
        savedData = FindObjectOfType<SavedData>();

        itemsData.Gen();
        itemsDataDict = itemsData.itemsData;

        foreach(var item in savedData.items)
        {
            itemsDataDict.Remove(item);
        }

        Dictionary<string, ItemsData.Data> chooseItems = new();

        int count = itemsDataDict.Count >= 3 ? 3 : itemsDataDict.Count;
        for (int i = 0; i < count; i++)
        {
            string item = itemsDataDict.Keys.ElementAt(Random.Range(0, itemsDataDict.Keys.Count));
            chooseItems.Add(item, itemsDataDict[item]);
            itemsDataDict.Remove(item);
        }

        int j = 0;
        Transform[] slots = count % 2 == 0 ? evenSlots : oddSlots;

        foreach (var item in chooseItems)
        {
            TMP_Text text = Instantiate(textOverPrefab);
            text.fontSize = 16;
            text.alignment = TextAlignmentOptions.Center;
            text.text = item.Value.name;
            text.transform.position = slots[j].transform.position;

            text.GetComponent<MouseOver>().showedText = item.Value.description;
            text.GetComponent<MouseOver>().x = 0;
            text.GetComponent<MouseOver>().x_s = 0;
            text.GetComponent<MouseOver>().y = -1f;
            text.GetComponent<MouseOver>().y_s = 0;
            text.GetComponent<MouseOver>().alignment = TextAlignmentOptions.Top;
            text.GetComponent<MouseOver>().textPrefab = textNewPrefab;
            text.GetComponent<MouseOver>().change = true;
            text.GetComponent<MouseOver>().isCoosingItem = true;
            text.GetComponent<MouseOver>().itemName = item.Value.key;

            j++;
        }
    }
}
