using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MouseOver : MonoBehaviour
{
    public string showedText;
    public TMP_Text textPrefab;

    private TMP_Text text;

    public bool change = false;

    public float x, y, z = 0,
        x_s = -0.5f, y_s = -0.5f, z_s = 0;
    public TextAlignmentOptions alignment = TextAlignmentOptions.TopLeft;

    public bool isCoosingItem = false;
    public string itemName;

    private void Start()
    {
        if (!change)
        {
            x = transform.position.x;
            y = transform.position.y;
        }
    }

    private void OnMouseEnter()
    {
        text = Instantiate(textPrefab);
        text.text = showedText;
        text.alignment = alignment;
        text.fontSize = 6;
        
    }

    private void OnMouseOver()
    {
        text.transform.position = new Vector3(x + x_s, y + y_s, z + z_s);
    }

    private void OnMouseExit()
    {
        Destroy(text.gameObject);
    }

    private void OnMouseDown()
    {
        if (isCoosingItem)
        {
            SavedData savedData = FindObjectOfType<SavedData>();
            savedData.items.Add(itemName);
            MouseOver[] mouseOvers = FindObjectsOfType<MouseOver>();
            foreach (MouseOver mouseOver in mouseOvers)
            {
                mouseOver.isCoosingItem = false;
            }
            Invoke(nameof(NextMove), 0.25f);
        }
    }

    private void NextMove()
    {
        SceneManager.LoadScene("Map");
    }
}
