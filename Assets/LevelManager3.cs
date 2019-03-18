using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager3 : ShmupLevel {
    public bool PlayOpeningBanter;
    public TextAsset openingBanter;
    public TextAsset lockBanter;
    public TextAsset closingBanter;
    public AudioClip levelBgm;

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

        BgmController.instance.PlayBGM(levelBgm);

        if (PlayOpeningBanter)
        {
            List<string> dialogEntries = DialogEngine.CreateDialogComponents(openingBanter.text);
            ShmupGameManager.instance.PauseGameplay();
            ConversationController.instance.StartConversation(dialogEntries);
            yield return null;

            while (ShmupGameManager.instance.Paused)
                yield return null;
        }

        ShmupGameManager.instance.player.gameObject.SetActive(true);
        ShmupGameManager.instance.RespawnPlayer();
        ShmupGameManager.instance.ResumeGameplay();

        yield return new WaitForSeconds(2.0f);

        List<string> lockEntries = DialogEngine.CreateDialogComponents(lockBanter.text);
        ConversationController.instance.StartConversation(lockEntries);
        yield return null;

        while (ShmupGameManager.instance.Paused)
            yield return null;

    }

    IEnumerator EndSequence()
    {
        List<string> dialogEntries = DialogEngine.CreateDialogComponents(closingBanter.text);
        ShmupGameManager.instance.PauseGameplay();
        ConversationController.instance.StartConversation(dialogEntries);
        yield return null;

        while (ShmupGameManager.instance.Paused)
            yield return null;

        BgmController.instance.StopBGM();

        ChapterHud.instance.EndLevel();
        while (!ChapterHud.instance.AnimationFinished())
        {
            yield return null;
        }
        SceneManager.LoadScene(3);
    }
}
