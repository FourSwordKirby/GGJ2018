using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : ShmupLevel {

    public bool PlayOpeningCutscene;
    public TextAsset openingCutscene;

    public override void StartLevel()
    {
        StartCoroutine(IntroSequence());
    }

    bool endingLevel;

    private float CreditsDuration = 4.0f;

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
        if (PlayOpeningCutscene)
        {
            ChapterHud.instance.StartLevel();
            while (!ChapterHud.instance.AnimationFinished())
            {
                yield return null;
            }

            //TextAsset dialog = ProgressManager.instance.GetStartDialog();
            StartCoroutine(ShmupGameManager.instance.PlayCutscene(openingCutscene, true));
            yield return null;
        }

        ShmupGameManager.instance.player.gameObject.SetActive(true);
        ShmupGameManager.instance.RespawnPlayer();
        ShmupGameManager.instance.ResumeGameplay();

        yield return new WaitForSeconds(CreditsDuration);
        EndLevel();
    }

    IEnumerator EndSequence()
    {
        ChapterHud.instance.EndLevel();
        while (!ChapterHud.instance.AnimationFinished())
        {
            yield return null;
        }
        SceneManager.LoadScene(1);
        yield return null;
    }
}
