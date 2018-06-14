using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Hitbox3D hitbox;
    public GameObject blastBase;
    public float fuse;

    public float explosionRate;
    public float duration;
    public float radius;

    public bool detonated;

	// Update is called once per frame
	void Update () {
        fuse -= Time.deltaTime;
        if (fuse < 0 && !detonated)
        {
            detonated = true;
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        while (blastBase.transform.localScale.x < radius)
        {
            blastBase.transform.localScale += Vector3.one * explosionRate * Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(this.gameObject);
        yield return null;
    }
}
