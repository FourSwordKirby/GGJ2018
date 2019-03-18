using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsText : MonoBehaviour {

    public string desiredText;
    public ShmupSimpleEnemy Letter;

    public float travelSpeed;
    public float fontSize;

    public float kerning;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        int count = 0;
        Vector3 x_modifier = ((-(desiredText.ToCharArray().Length)+1) / 2.0f) * kerning * Vector3.right;
        foreach (char c in desiredText.ToCharArray())
        {
            if(c != ' ')
            {
                ShmupSimpleEnemy enemy = Instantiate(Letter);
                enemy.transform.position = transform.position + (count * Vector3.right * kerning) + x_modifier;
                enemy.transform.parent = this.transform;
                enemy.target = ShmupGameManager.instance.player.gameObject;
                enemy.GetComponentInChildren<TextMesh>().text = c.ToString();
                enemy.GetComponentInChildren<TextMesh>().gameObject.transform.localScale = Vector3.one * fontSize;
            }

            count++;
        }
    }


    float delayTime = 2.0f;
    private void Update()
    {
        delayTime -= Time.deltaTime;
        if(delayTime < 0)
            transform.position += Time.deltaTime * travelSpeed * Vector3.forward;

        if (transform.localPosition.z > 48)
            Destroy(gameObject);
    }
}
