using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public GameObject bulletDeathEffect;
    public Rigidbody signalBeam;
    public Rigidbody UnlockSignalBeam;

    //Instance Managing;
    public static EffectManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SpawnLinkSignal(Vector3 originPos, Vector3 targetPos, bool isUnlockSignal = false)
    {
        StartCoroutine(SpawnLockSignalEffect(originPos, targetPos, isUnlockSignal));
    }

    private IEnumerator SpawnLockSignalEffect(Vector3 originPos, Vector3 targetPos, bool isUnlockSignal)
    {
        Rigidbody beam;
        if (isUnlockSignal)
            beam = UnlockSignalBeam;
        else
            beam = signalBeam;

        Rigidbody signal = Instantiate(beam, originPos, Quaternion.identity);
        Vector3 dirVector = targetPos - originPos;

        float time = dirVector.magnitude;

        float upMagnitude = -Physics.gravity.y * 0.5f;


        signal.velocity = dirVector.x * Vector3.right + upMagnitude * Vector3.up + dirVector.z * Vector3.forward;

        float timer = 0;
        float duration = 1f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(signal.gameObject);
        yield return null;
    }

    public void SpawnCollisionEffect(EntityType victimType, EntityType attackerType, Vector3 victomPosition)
    {
        if(victimType == EntityType.Bullet && (attackerType == EntityType.Bullet || attackerType == EntityType.Environment))
        {
            StartCoroutine(SpawnBulletCollisionEffect(victomPosition));
        }
    }

    public EntityType LayerToEntityType(int layer)
    {
        string layerName = LayerMask.LayerToName(layer);
        if (layerName.Contains("Bullet"))
            return EntityType.Bullet;
        else
            return EntityType.None;
    }

    private IEnumerator SpawnBulletCollisionEffect(Vector3 position)
    {
        GameObject effect = Instantiate(bulletDeathEffect);
        effect.transform.position = position;
        effect.transform.forward = Camera.main.transform.forward;

        effect.transform.localScale = Vector3.zero;
        float maxSize = 0.50f;

        float timer = 0;
        float duration = 0.25f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            effect.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * maxSize, timer / duration);
            yield return null;
        }
        Destroy(effect.gameObject);
        yield return null;
    }
}


public enum EntityType
{
    Obstacle,
    Bullet,
    Enemy,
    Environment,
    None
}