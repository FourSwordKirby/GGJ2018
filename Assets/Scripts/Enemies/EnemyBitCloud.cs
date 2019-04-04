using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBitCloud : MonoBehaviour {
    public ShmupEnemy associatedEnemy;

    public GameObject bit;

    public float minSpeed;
    public float maxSpeed;

    public float minSize;
    public float maxSize;

    public float minRotation;
    public float maxRotation;

    public Gradient colorGradient;

    public int bitCount;

    public List<FloatingBit> bits;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < bitCount; i++)
        {
            FloatingBit myBit = Instantiate(bit).GetComponent<FloatingBit>();
            myBit.gameObject.transform.position = this.transform.position;
            myBit.gameObject.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
            myBit.selfbody.velocity = Random.insideUnitSphere * Random.Range(minSpeed, maxSpeed);

            myBit.SetColor(colorGradient.Evaluate(Random.Range(0.0f, 1.0f)));

            bits.Add(myBit);
        }

        if(bitCount == 0)
            StartCoroutine(GarbageCollect());
    }

    private void OnTriggerEnter(Collider col)
    {
        ShmupEntity entity = col.GetComponentInParent<ShmupEntity>();
        foreach(FloatingBit myBit in bits)
            myBit.TrackObject(entity);

        AudioManager.instance.OnBombCollect();
        StartCoroutine(GarbageCollect());
    }

    bool startedGarbageCollection;
    private IEnumerator GarbageCollect()
    {
        if (startedGarbageCollection)
            yield break;
        startedGarbageCollection = true;

        yield return new WaitForSeconds(2.0f);
        while (bits.Where(x => x != null).Count() != 0 && associatedEnemy.IsDead())
            yield return new WaitForSeconds(3.0f);

        Destroy(associatedEnemy.gameObject);
        yield return null;
    }
}
