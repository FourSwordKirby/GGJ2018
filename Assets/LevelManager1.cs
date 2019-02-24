using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager1 : ShmupLevel {

    public bool PlayOpeningBanter;
    public TextAsset openingBanter;
    public TextAsset spaceInvaderBanter;

    public Encounter mainEncounter;
    public Wave spaceInvaderWave;
    private bool spaceInvaderDialogStarted;

    public override void StartLevel()
    {
        StartCoroutine(IntroSequence());
    }

    public override void EndLevel()
    {
        EndSequence();
    }

    public override bool IsFinished()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(0.5f);

        if (PlayOpeningBanter)
        {
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
        if (mainEncounter.IsCompleted())
            EndLevel();
        if(spaceInvaderWave.started && !spaceInvaderDialogStarted)
        {
            spaceInvaderDialogStarted = true;
            List<string> dialogEntries = DialogEngine.CreateDialogComponents(spaceInvaderBanter.text);
            ConversationController.instance.StartConversation(dialogEntries);
        }
    }

    void EndSequence()
    {
        Application.LoadLevel(2);
        Debug.Log("end level");
    }
}
