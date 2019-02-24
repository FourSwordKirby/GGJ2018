using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WirelessNavPoint : MonoBehaviour {
    public List<WirelessNavPoint> adjacentPoints;
    public bool isEndpoint;

    public Material connectionMaterial;
    public Material deviceConnectionMaterial;

    //TODO: Implement something where going over the navpoints disables devices
    public List<GameObject> associatedDevices;

    // Assign a unique ID to each NavPoint so we can draw the line renderers properly
    public static int IdCount = 0;
    public int id;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(adjacentPoints != null)
        {
            foreach (WirelessNavPoint navpoint in adjacentPoints)
            {
                if (navpoint.adjacentPoints.Contains(this))
                    Gizmos.DrawLine(this.transform.position, navpoint.transform.position);
            }
        }
    }

    void Awake()
    {
        id = IdCount;
        IdCount++;
    }

    void Start()
    {
        foreach (WirelessNavPoint navpoint in adjacentPoints)
        {
            // Only draw a line if this NavPoint's id is less than the others.
            // This ensures each line is only drawn once.
            if (this.id < navpoint.id)
            {
                GameObject obj = new GameObject("Line Renderer");
                obj.transform.parent = this.transform;
                LineRenderer lr = obj.AddComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, this.transform.position);
                lr.SetPosition(1, navpoint.transform.position);
                //lr.SetColors(connectionColor, connectionColor);
                lr.material = connectionMaterial;
                lr.startWidth = 0.2f;
                lr.endWidth = 0.2f;
            }
        }

        foreach(GameObject device in associatedDevices)
        {
            GameObject obj = new GameObject("Line Renderer");
            obj.transform.parent = this.transform;
            LineRenderer lr = obj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, this.transform.position);
            lr.SetPosition(1, device.transform.position);
            lr.material = deviceConnectionMaterial;
            lr.startWidth = 0.2f;
            lr.endWidth = 0.2f;
        }
    }

    public WirelessNavPoint getNextNavPoint(Vector3 direction, WirelessNavPoint previousNode = null)
    {

        if (adjacentPoints.Count == 0)
            return null;

        if (direction != Vector3.zero)
        {
            WirelessNavPoint nextNavPoint = adjacentPoints.Aggregate(
               (nav1, nav2) =>
               {
                   float angle1 = nav1 != previousNode ? Vector3.Angle(nav1.transform.position - gameObject.transform.position, direction) : float.PositiveInfinity;
                   float angle2 = nav2 != previousNode ? Vector3.Angle(nav2.transform.position - gameObject.transform.position, direction) : float.PositiveInfinity;
                   return angle1 < angle2 ? nav1 : nav2;
               });
             return nextNavPoint;
        }
        else
            return adjacentPoints[0];
    }

    //This will disable the associated electric device and do other things as necessary when the player enters it
    public void Trigger() {
    }

    internal WirelessNavPoint getFirstNavPoint()
    {
        return adjacentPoints[0];
    }
}
