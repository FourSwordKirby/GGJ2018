using UnityEngine;
using System.Collections;

public class ShmupPlayerHealthBar : MonoBehaviour
{
    public ShmupPlayer Player;

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


    public float fadeTimer = 0.0f;
    const float FADE_TIME = 1.0f;
    const float FADE_OPACITY = 0.2f;    

    private void Start()
    {
        currentHealth = Player.health;
        maxHealth = Player.maxHealth;

        lastHealthValue = targetHealthValue = currentHealth;

        healthSprite.color = Color.clear;
        redHealthSprite.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        targetHealthValue = Player.health;

        if (Mathf.Abs(lastHealthValue - targetHealthValue) > 0.1f)
        {
            if (fadeTimer <= 0)
                StartCoroutine(FadeIn());
            lastHealthValue = targetHealthValue;
            if (targetHealthValue > 0)
                reductionTimer = REDUCTION_TIME;
            else
                reductionTimer = DEATH_REDUCTION_TIME;
            fadeTimer = FADE_TIME;
        }

        reductionTimer -= Time.deltaTime;
        if (reductionTimer <= 0)
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealthValue, maxHealth * Time.deltaTime);

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

        if (reductionTimer <= 0 && fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            if (fadeTimer <= 0)
                StartCoroutine(FadeOut());
        }
    }

    bool fading_in;
    IEnumerator FadeIn()
    {
        fading_in = true;
        float timer = 0.2f;
        while(timer > 0)
        {
            healthSprite.color = Color.Lerp(Color.white, Color.clear, timer / 0.2f);
            redHealthSprite.color = Color.Lerp(Color.red, Color.clear, timer / 0.2f);
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        fading_in = false;
        yield return null;
    }

    IEnumerator FadeOut()
    {
        float timer = 1.0f;
        while (timer > 0 && !fading_in)
        {
            healthSprite.color = Color.Lerp(Color.white - Color.black * (1.0f-FADE_OPACITY), Color.white, timer / 1.0f);
            redHealthSprite.color = Color.clear;
            timer -= Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
