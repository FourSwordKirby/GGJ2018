using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBitCloud : MonoBehaviour {

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
	}

    private void OnTriggerEnter(Collider col)
    {
        print(col);
        foreach(FloatingBit myBit in bits)
        {
            myBit.TrackObject(col.gameObject);
        }
    }
}
