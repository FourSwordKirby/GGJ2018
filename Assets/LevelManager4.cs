using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager4 : ShmupLevel {
    public bool PlayOpeningCutscene;
    public TextAsset openingCutscene;
    public AudioClip levelBgm;

    public Encounter mainEncounter;

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

    private void Update()
    {
        if (mainEncounter.IsCompleted())
            EndLevel();
    }

    void EndSequence()
    {
        Debug.Log("I'm ending the level");
    }
}