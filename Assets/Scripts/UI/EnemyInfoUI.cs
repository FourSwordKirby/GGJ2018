using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyInfoUI : MonoBehaviour {

    public ShmupEnemy currentEnemy;

    public Text enemyName;
    public Image maxHealthBar;
    public Image redHealthBar;
    public Image healthBar;

    public float currentHealth = 0;
    public float maxHealth = 1;
    public float targetHealth;

    private float reductionTimer = 0.0f;
    private float lastHealthValue = 0;

    private void Start()
    {
        lastHealthValue = currentEnemy.GetHealth();
    }

    void Update()
    {
        if(currentEnemy != null)
        {
            targetHealth = currentEnemy.GetHealth();
            maxHealth = currentEnemy.GetMaxHealth();
        }


        if (lastHealthValue != targetHealth)
        {
            lastHealthValue = targetHealth;
            reductionTimer = 1.0f;
        }

        reductionTimer -= Time.deltaTime;
        if (reductionTimer <= 0)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, maxHealth * Time.deltaTime * 0.5f);
        }

        if(currentHealth > targetHealth)
        {
            redHealthBar.fillAmount = currentHealth / maxHealth;
            healthBar.fillAmount = targetHealth / maxHealth;
        }
        else
        {
            redHealthBar.fillAmount = targetHealth / maxHealth;
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void DisplayEnemyInfo(ShmupEnemy enemy)
    {
        currentEnemy = enemy;
        lastHealthValue = enemy.GetHealth();
    }

    public void SetHealth(float health)
    {
        this.targetHealth = health;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }
}
