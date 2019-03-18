using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : ShmupLevel {

    public bool PlayOpeningCutscene;
    public TextAsset openingCutscene;
    public float CreditsDelay;
    public AudioClip creditsBgm;

    public List<GameObject> CreditObjects;

    public override void StartLevel()
    {
        StartCoroutine(CreditsSequence());
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

    IEnumerator CreditsSequence()
    {
        if (PlayOpeningCutscene)
        {
            ChapterHud.instance.StartLevel();
            while (!ChapterHud.instance.AnimationFinished())
            {
                yield return null;
            }
            
            StartCoroutine(ShmupGameManager.instance.PlayCutscene(openingCutscene, true, 6.0f));
            yield return null;

            while (ShmupGameManager.instance.Paused)
                yield return null;
        }

        ShmupGameManager.instance.player.gameObject.SetActive(true);
        ShmupGameManager.instance.RespawnPlayer();
        BgmController.instance.PlayBGM(creditsBgm);

        int currentCreditsIndex = 0;
        while(currentCreditsIndex < CreditObjects.Count)
        {
            CreditObjects[currentCreditsIndex].SetActive(true);
            while (CreditObjects[currentCreditsIndex].transform.childCount > 0)
                yield return null;
            currentCreditsIndex += 1;
        }

        EndLevel();
    }

    IEnumerator EndSequence()
    {
        BgmController.instance.StopBGM();

        ChapterHud.instance.EndLevel();
        while (!ChapterHud.instance.AnimationFinished())
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
        yield return null;
    }
}
