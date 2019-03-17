using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombField : MonoBehaviour {

    public List<GameObject> fieldRings;
    List<float> initialRotations = new List<float>();

    public float lifeTime = 2.0f;
    public float expansionDuration;
    public float timer = 0.0f;

    public AnimationCurve expansionCurve;

    public GameObject StunHitbox;

    public void SpawnField()
    {
        int sign = Random.Range(0, 1) * 2 - 1;
        foreach(GameObject ring in fieldRings)
        {
            sign *= -1;
            float rotation = Random.Range(90.0f, 180.0f) * sign;
            initialRotations.Add(rotation);

            ring.transform.localRotation = Quaternion.Euler(Vector3.up * rotation);
            ring.transform.parent = this.gameObject.transform;
            ring.transform.localPosition = Vector3.zero;
        }
        this.transform.localScale = Vector3.one * expansionCurve.Evaluate(0);
    }

    // Update is called once per frame
    void Update () {
        if(fieldRings.Count > 0)
        {
            timer += Time.deltaTime;

            //Handles enlarging
            if (timer < expansionDuration)
            {
                this.transform.localScale = Vector3.one * expansionCurve.Evaluate(timer / expansionDuration);
            }

            //Handles rotating
            for(int i = 0; i < fieldRings.Count; i++)
            {
                GameObject ring = fieldRings[i];
                float currentRotation = ring.transform.localRotation.eulerAngles.y;
                //Make the rotation a consistent speed
                ring.transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.up * initialRotations[i], Vector3.zero, timer / (expansionDuration)));
            }

            if (timer > expansionDuration * 1.2f && timer < expansionDuration * 1.5f)
            {
                this.transform.localScale += Vector3.up;
                if(!StunHitbox.activeSelf)
                {
                    SfxController.instance.PlaySound("Bomb Activate");

                    CameraControlsTopDown3D.instance.Shake(0.4f, 0.5f);
                    StunHitbox.SetActive(true);
                }
            }
            else if (timer > expansionDuration * 1.5f)
            {
                foreach (GameObject ring in fieldRings)
                {
                    List<Renderer> renderers = ring.GetComponentsInChildren<Renderer>().ToList();
                    foreach(Renderer render in renderers)
                    {
                        render.material.color *= 0.92f;
                    }
                }
            }

            if (timer > lifeTime)
                Destroy(this.gameObject);
        }
    }
}
