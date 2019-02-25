using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager3 : ShmupLevel {
    public bool PlayOpeningCutscene;
    public TextAsset openingCutscene;

    public SecurityLock FloorOne_GuardedLock;
    public Encounter FloorOne_GuardedLockEncounter;
    public Barrier FloorOne_Gate;
    public EncounterTrigger FloorOne_GateEncounterTrigger;


    public SecurityLock FloorTwo_GuardedLock1;
    public Encounter FloorTwo_GuardedLockEncounter1;
    public SecurityLock FloorTwo_GuardedLock2;
    public Encounter FloorTwo_GuardedLockEncounter2;

    public SecurityLock FloorThree_GuardedLock;
    public Encounter FloorThree_AmbushEncounter;

    private void Update()
    {
        if(FloorOne_GateEncounterTrigger != null)
            FloorOne_GateEncounterTrigger.gameObject.SetActive(!FloorOne_Gate.isClosed());
        if(FloorOne_GuardedLock.unlocked)
            FloorOne_GuardedLockEncounter.StartEncounter();

        if (FloorTwo_GuardedLock1.unlocked)
            FloorTwo_GuardedLockEncounter1.StartEncounter();
        if (FloorTwo_GuardedLock2.unlocked)
            FloorTwo_GuardedLockEncounter2.StartEncounter();

        if (FloorThree_GuardedLock.unlocked)
            FloorThree_AmbushEncounter.StartEncounter();
    }

    public override void StartLevel()
    {
        StartCoroutine(IntroSequence());
        FloorOne_GateEncounterTrigger.gameObject.SetActive(false);
    }

    bool endingLevel;
    public override void EndLevel()
    {
        if (!endingLevel)
        {
            endingLevel = true;
            StartCoroutine(EndSequence());
        }
    }

    public override bool IsFinished()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IntroSequence()
    {
        ChapterHud.instance.StartLevel();
        while (!ChapterHud.instance.AnimationFinished())
        {
            yield return null;
        }

        if (PlayOpeningCutscene)
        {
            //TextAsset dialog = ProgressManager.instance.GetStartDialog();
            StartCoroutine(ShmupGameManager.instance.PlayCutscene(openingCutscene, true));
            yield return null;
        }

        if (!PlayOpeningCutscene)
        {
            ShmupGameManager.instance.ResumeGameplay();
        }
        ShmupGameManager.instance.player.gameObject.SetActive(true);
        ShmupGameManager.instance.RespawnPlayer();
    }

    IEnumerator EndSequence()
    {
        ChapterHud.instance.EndLevel();
        while (!ChapterHud.instance.AnimationFinished())
        {
            yield return null;
        }
        SceneManager.LoadScene(3);
    }
}
