using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public int resist;
    public float damageMultiplier = 1;

    public TMP_Text attributes;

    private GameManager gm;
    private SavedData savedData;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        savedData = FindObjectOfType<SavedData>();
        maxHealth = savedData.playerHealthMax;
        health = savedData.playerHealth;
        attributes.text = string.Format("{0}\n{1}", health, resist);
    }

    private void Update()
    {
        attributes.text = string.Format("{0}\n{1}", health, resist);
    }

    public void Damage(int damage)
    {
        if (!gameObject.activeSelf && this == null)
            return;
        health -= (damage - resist) > 0 ? (damage - resist) : 0;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        health = (health + amount) > maxHealth ? maxHealth : health + amount;
    }

    public void Resist(int amount)
    {
        resist += amount;
    }
}
