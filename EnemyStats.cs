using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public static float currentHealth;
    public CharacterStat damage;
    public Slider bar;
    public TextMeshProUGUI txtHealth;
    public TextMeshProUGUI txtEnemy;
    private float mCurrentValue;
    private float mCurrentPercent;
    void Awake()
    {
        currentHealth = 50;
        SetHealth(currentHealth);

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 0;
            General.GameWon = true;
        }
        SetHealth(currentHealth);
    }
    public void AddHealth(float health)
    {
        currentHealth += health;
        Debug.Log(transform.name + " heals " + health + " health.");
        SetHealth(currentHealth);
    }
    public virtual void Die()
    {
        //Overwrite
        Debug.Log(transform.name + " died.");
    }
    public void SetHealth(float health)
    {
        if (health != mCurrentValue)
        {
            mCurrentValue = health;
            mCurrentPercent = (float)mCurrentValue / (float)maxHealth;
        }
        txtEnemy.text = string.Format("{0}%", Mathf.RoundToInt(mCurrentPercent * 100));
        txtHealth.text = string.Format("{0}%", 100 - Mathf.RoundToInt(mCurrentPercent * 100));
        bar.value = 1-mCurrentPercent;
    }
}
