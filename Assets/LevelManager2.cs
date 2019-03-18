using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager2 : ShmupLevel {
    public bool PlayOpeningCutscene;
    public TextAsset openingCutscene;
    public AudioClip levelBgm;

    public Encounter mainEncounter;

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
        ChapterHud.instance.StartLevel();
        while (!ChapterHud.instance.AnimationFinished())
        {
            yield return null;
        }
        
        BgmController.instance.PlayBGM(levelBgm);

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
        BgmController.instance.StopBGM();

        ChapterHud.instance.EndLevel();
        while (!ChapterHud.instance.AnimationFinished())
        {
            yield return null;
        }
        SceneManager.LoadScene(2);
    }
}