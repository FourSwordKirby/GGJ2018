using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingEffect : MonoBehaviour {

    public float maxSize;
    public float duration;
    public AnimationCurve curve;

    private float timer;
    private void Start()
    {
        this.transform.localScale = Vector3.zero;
    }
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        this.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * maxSize, timer / duration);

        this.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.Lerp(Color.clear, Color.white, curve.Evaluate(timer/duration)));
        
        if (timer > duration)
        {
            Destroy(this.gameObject);
        }
	}
}
