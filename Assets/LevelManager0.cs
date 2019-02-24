using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager0 : ShmupLevel {

    public bool PlayOpeningCutscene;
    public TextAsset openingCutscene;

    public bool PlayOpeningBanter;
    public bool PlayClosingBanter;
    public TextAsset openingBanter;
    public TextAsset closingBanter;

    public Encounter TutorialEncounter;

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
        yield return new WaitForSeconds(0.5f);

        if (PlayOpeningCutscene)
        {
            //TextAsset dialog = ProgressManager.instance.GetStartDialog();
            StartCoroutine(ShmupGameManager.instance.PlayCutscene(openingCutscene, true));
            yield return null;
            while (ShmupGameManager.instance.Paused)
                yield return null;
        }

        if(PlayOpeningBanter)
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
    }

    private void Update()
    {
        if (TutorialEncounter.IsCompleted() && PlayClosingBanter)
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

        Application.LoadLevel(1);
    }
}
