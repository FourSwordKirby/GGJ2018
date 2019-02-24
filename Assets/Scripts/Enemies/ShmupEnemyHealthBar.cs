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
            StartCoroutine(FadeIn());

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

        if (currentHealth > targetHealthValue)
        {
            redHealthBar.alphaCutoff = currentHealth / maxHealth;
            healthBar.alphaCutoff = targetHealthValue / maxHealth;
        }
        else
        {
            redHealthBar.alphaCutoff = targetHealthValue / maxHealth;
            healthBar.alphaCutoff = currentHealth / maxHealth;
        }

        redHealthBar.alphaCutoff = 1 - redHealthBar.alphaCutoff;
        healthBar.alphaCutoff = 1 - healthBar.alphaCutoff;
    }

    IEnumerator FadeIn()
    {
        float timer = 0.2f;
        while(timer > 0)
        {
            healthSprite.color = Color.Lerp(Color.clear, Color.white, timer / 0.2f);
            redHealthSprite.color = Color.Lerp(Color.clear, Color.red, timer / 0.2f);
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
