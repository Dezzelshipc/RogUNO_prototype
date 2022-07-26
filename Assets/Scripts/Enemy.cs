using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class Enemy : MonoBehaviour
{
    public int health = 30;

    public int attackDamage = 5;

    private int skips = 0;

    public string scriptName = "Attack";
    private string skipText;

    public TMP_Text attributes;
    public TMP_Text effects;

    private GameManager gm;
    private SavedData savedData;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        skipText = LocalizationSettings.StringDatabase.GetLocalizedString("UItext", "Skips");
        gm = FindObjectOfType<GameManager>();
        savedData = FindObjectOfType<SavedData>();

        health = savedData.enemyHealth;
        attackDamage = savedData.enemyDamage;
        attributes.text = $"{health}\n{attackDamage}";
    }

    public void Damage(int damage)
    {
        if (!gameObject.activeSelf && this == null)
            return;
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        attributes.text = $"{health}\n{attackDamage}";
    }

    public void Die()
    {
        gameObject.SetActive(false);
        gm.Transition();
    }

    public void Action()
    {
        if (skips > 0)
        {
            skips--;
            return;
        }
        Invoke(scriptName, 0f);
    }

    public void Attack()
    {
        gm.player.Damage(attackDamage);
    }

    public void Skip(int amount)
    {
        skips += amount;
    }

    public void Active(bool state)
    {
        gameObject.SetActive(state);
    }

    private void Update()
    {
        effects.text = "";
        if (skips > 0)
        {
            effects.text += skipText + $" {skips}";
        }
    }
}
