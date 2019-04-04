using UnityEngine;
using System.Collections;

public class WirelessPort : MonoBehaviour, ShmupSpawnable
{
    public int hackingThreshold;

    private int hackingProgress;
    public float decayRate; //We lose 1 hacking progress point every x seconds
    float timer;
    public MeshRenderer modelRenderer;

    public GameObject currentOwner;
    public WirelessNavPoint navPoint;

    // Update is called once per frame
    void Update()
    {
        //Controls decaying hacking rate
        if (hackingProgress > 0)
        {
            timer += Time.deltaTime;
            if (timer > decayRate)
            {
                timer = 0;
                hackingProgress--;
            }
        }
    }

    public void OnHit(float damage, GameObject owner)
    {
        if (owner != currentOwner)
        {
            currentOwner = owner;
            //ResetProgress();
        }

        //hackingProgress++;

        //if (hackingProgress > hackingThreshold)

        Teleport();
    }

    void ResetProgress()
    {
        hackingProgress = 0;
    }

    void Teleport()
    {
        print("teleportcall");
        if (currentOwner.GetComponent<ShmupPlayer>() != null && !currentOwner.GetComponent<ShmupPlayer>().inGrazeForm)
        {
            StartCoroutine(TransmitEntity(currentOwner));
            hackingProgress = 0;
        }
    }

    IEnumerator TransmitEntity(GameObject player)
    {
        AudioManager.instance.OnEthernetStart();

        currentOwner.GetComponent<ShmupPlayer>().Graze();
        currentOwner.GetComponent<ShmupPlayer>().FreezeVelocity();

        WirelessNavPoint currentNavPoint = navPoint;
        while(currentNavPoint != null)
        {
            //Do the teleporting here
            Vector3 FromLocation = player.transform.position;
            Vector3 ToLocation = currentNavPoint.transform.position;

            while (player.transform.position != ToLocation)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, ToLocation, 3.0f);
                yield return new WaitForSeconds(0.2f);
            }

            if (currentNavPoint.isEndpoint && currentNavPoint != navPoint)
            {
                player.transform.position += (ToLocation- FromLocation).normalized;
                break;
            }

            currentNavPoint = currentNavPoint.getNextNavPoint(Controls.getDirection());
        }

        player.transform.position = currentNavPoint.transform.position - currentNavPoint.transform.forward * 4.0f; //Makes sure the player appears in front of the new port
        currentOwner.GetComponent<ShmupPlayer>().Materialize();

        this.Deactivate();

        AudioManager.instance.OnEthernetStop();

        yield return null;
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        int count = 0;

        WirelessNavPoint currentNavPoint = navPoint;
        while (currentNavPoint != null)
        {
            WirelessNavPoint nextNavPoint = currentNavPoint.getNextNavPoint(Controls.getDirection(), currentNavPoint);

            foreach (GameObject obj in currentNavPoint.associatedDevices)
            {
                ShmupSpawnable spawnableObject = obj.GetComponent<ShmupSpawnable>();
                if (spawnableObject != null)
                    spawnableObject.Die();
            }
            currentNavPoint = nextNavPoint;

            count++;
            if (count > 30)
            {
                print("deleting nav points probably resulted in a infinite loop");
                break;
            }
            //if (currentNavPoint.isEndpoint)
            //    break;
        }
    }

    public void Die()
    {
        StartCoroutine(DissolveAnim());
    }

    private IEnumerator DissolveAnim()
    {
        float dissolveTime = 1.0f;
        float animTimer = dissolveTime;
        while(animTimer > 0)
        {
            animTimer -= Time.deltaTime;
            modelRenderer.material.SetFloat("_DissolveIntensity", 1.0f-(animTimer / dissolveTime));
            yield return null;
        }
        this.gameObject.SetActive(false);
        yield return null;
    }

    public bool IsDead()
    {
        throw new System.NotImplementedException();
    }
}
