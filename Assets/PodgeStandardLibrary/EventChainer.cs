using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//We have to wrap the coroutine with our own class called event which signifies a start and end
//Code that up later, for now we're hacking things together :V
//TODO: Fix this later!!
public class EventChainger:MonoBehaviour
{
    public List<IEnumerator> events;
    public bool inProgress;

    public void begin()
    {
        StartCoroutine(playEvents());
    }

    //plays all of our stored events
    private IEnumerator playEvents()
    {
        foreach(IEnumerator f in events)
        {
            StartCoroutine(f);
            inProgress = true;
            while (inProgress)
                yield return null;
        }
        yield return null;
    }

    private IEnumerator playRoutine(IEnumerator routine)
    {
        //Do the actual event running stuff here I guess
        yield return null;
        inProgress = false;
        yield return null;
    }
}