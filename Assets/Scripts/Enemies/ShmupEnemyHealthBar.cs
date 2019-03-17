using UnityEngine;
using System.Collections;

public class ShmupEnemyHealthBar : MonoBehaviour
{
    public ShmupEnemy Enemy;

    public SpriteRenderer healthSprite;
    public SpriteRenderer redHealthSprite;

    public SpriteMask healthBar;
    public SpriteMask redHealthBar;

    private float currentHealth;
    private float maxHealth;

    private float lastHealthValue;
    private float targetHealthValue;

    public float reductionTimer = 0.0f;

    const float REDUCTION_TIME = 0.30f;
    const float DEATH_REDUCTION_TIME = 0.15f;

    private void Start()
    {
        currentHealth = Enemy.GetHealth();
        maxHealth = Enemy.GetMaxHealth();

        lastHealthValue = targetHealthValue = currentHealth;

        healthSprite.color = Color.clear;
        redHealthSprite.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        targetHealthValue = Enemy.GetHealth();
        if(targetHealthValue != maxHealth)
        {
            healthSprite.color = Color.white;
            redHealthSprite.color = Color.red;
        }

        if (lastHealthValue != targetHealthValue)
        {
            lastHealthValue = targetHealthValue;
            if (targetHealthValue > 0)
                reductionTimer = REDUCTION_TIME;
            else
                reductionTimer = DEATH_REDUCTION_TIME;
        }

        reductionTimer -= Time.deltaTime;
        if (reductionTimer <= 0)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealthValue, maxHealth * Time.deltaTime);
        }

        redHealthBar.alphaCutoff = 1 -  Mathf.Max(currentHealth / maxHealth, targetHealthValue / maxHealth);
        healthBar.alphaCutoff = 1- targetHealthValue / maxHealth;

        if(this.redHealthBar.alphaCutoff == 1)
        {
            redHealthSprite.enabled = false;
            healthSprite.enabled = false;
        }
    }
}
