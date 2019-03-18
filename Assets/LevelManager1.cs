using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager1 : ShmupLevel {

    public bool PlayOpeningBanter;
    public TextAsset openingBanter;
    public TextAsset spaceInvaderBanter;
    public TextAsset closingBanter;
    public AudioClip levelBgm;

    public Encounter mainEncounter;
    public Wave spaceInvaderWave;
    private bool spaceInvaderDialogStarted;

    public override void StartLevel()
    {
        StartCoroutine(IntroSequence());
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
        if (PlayOpeningBanter)
        {
            ChapterHud.instance.StartLevel();
            while (!ChapterHud.instance.AnimationFinished())
            {
                yield return null;
            }

            BgmController.instance.PlayBGM(levelBgm);

            List<string> dialogEntries = DialogEngine.CreateDialogComponents(openingBanter.text);
            ShmupGameManager.instance.PauseGameplay();
            ConversationController.instance.StartConversation(dialogEntries);
            yield return null;

            while (ShmupGameManager.instance.Paused)
                yield return null;
        }

        ShmupGameManager.instance.ResumeGameplay();
        ShmupGameManager.instance.player.gameObject.SetActive(true);
        ShmupGameManager.instance.RespawnPlayer();
    }

    private void Update()
    {
        if(spaceInvaderWave.started && !spaceInvaderDialogStarted)
        {
            spaceInvaderDialogStarted = true;
            List<string> dialogEntries = DialogEngine.CreateDialogComponents(spaceInvaderBanter.text);
            ConversationController.instance.StartConversation(dialogEntries);
        }
        if (mainEncounter.IsCompleted())
            EndLevel();
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
        SceneManager.LoadScene(2);
    }
}
