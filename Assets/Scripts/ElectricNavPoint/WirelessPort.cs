using UnityEngine;
using System.Collections;

public class WirelessPort : MonoBehaviour
{
    public int hackingThreshold;

    private int hackingProgress;
    public float decayRate; //We lose 1 hacking progress point every x seconds
    float timer;

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
            ResetProgress();
        }

        hackingProgress++;

        if (hackingProgress > hackingThreshold)
            Teleport();
    }

    void ResetProgress()
    {
        hackingProgress = 0;
    }

    void Teleport()
    {
        if (currentOwner.GetComponent<ShmupPlayer>() != null)
        {
            StartCoroutine(TransmitEntity(currentOwner));
            hackingProgress = 0;
        }
    }

    IEnumerator TransmitEntity(GameObject player)
    {
        currentOwner.GetComponent<ShmupPlayer>().Graze();

        WirelessNavPoint currentNavPoint = navPoint;
        while(currentNavPoint != null)
        {
            //Do the teleporting here
            Vector3 FromLocation = player.transform.position;
            Vector3 ToLocation = currentNavPoint.transform.position;

            while (player.transform.position != ToLocation)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, ToLocation, 1f);
                yield return new WaitForSeconds(0.1f);
            }

            if (currentNavPoint.isEndpoint && currentNavPoint != navPoint)
            {
                player.transform.position += (ToLocation- FromLocation).normalized;
                break;
            }

            currentNavPoint = currentNavPoint.getNextNavPoint(Controls.getDirection());
        }

        currentOwner.GetComponent<ShmupPlayer>().Materialize();

        yield return null;
    }
}
