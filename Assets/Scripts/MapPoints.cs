using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPoints : MonoBehaviour
{
    public List<GameObject> dotsFrom = new();
    public bool isCurrent = false;
    public string sceneName;
    public string actionName;

    public int dotId;
    public bool isLast = false;

    public int enemyHealth;
    public int enemyDamage;

    private GameObject player;
    private SavedData savedData;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        savedData = FindObjectOfType<SavedData>();
    }

    private void OnMouseDown()
    {
        foreach(GameObject dot in dotsFrom)
        {
            if (dot.GetComponent<MapPoints>().isCurrent)
            {
                dot.GetComponent<MapPoints>().isCurrent = false;
                player.transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, -1);
                savedData.lastDotId = dotId;
                savedData.enemyHealth = enemyHealth;
                savedData.enemyDamage = enemyDamage;
                savedData.isLast = isLast;

                Invoke(nameof(Wait), 0.25f);
            }
        }
    }
    void Wait()
    {
        isCurrent = true;
        if (actionName == "" || actionName == null)
            SceneManager.LoadScene(sceneName);
        else
            Invoke(actionName, 0);
    }

    private void Heal()
    {
        savedData.playerHealth = savedData.playerHealthMax;
    }
}
