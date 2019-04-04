using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterHud : MonoBehaviour {

    public GameObject framing;
    public Text chapterTitle;
    public Text chapterSubtitle;
    public UILoadingBar loadingBar;

    public Animation animations;

    public static ChapterHud instance;

    private void Awake()
    {
        if (ChapterHud.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayOpeningAnimation();
        }
	}

    public void StartLevel()
    {
        PlayOpeningAnimation();
    }

    public void EndLevel()
    {
        PlayClosingAnimation();
    }

    public bool AnimationFinished()
    {
        return !animations.isPlaying;
    }

    void PlayOpeningAnimation()
    {
        framing.SetActive(true);
        chapterTitle.gameObject.SetActive(true);
        chapterSubtitle.gameObject.SetActive(true);
        loadingBar.gameObject.SetActive(true);
        animations.Play("ChapterScreenAnimation");
    }

    void PlayClosingAnimation()
    {
        animations.Play("ChapterEndAnimation");
    }

    public void OnCameraSightFadeIn()
    {
        AudioManager.instance.OnCameraSightFadein();
    }

    public void OnPhaseLoadBarStart()
    {
        AudioManager.instance.OnPhaseLoadBarStart();
    }
    public void OnPhaseLoadBarStop()
    {
        AudioManager.instance.OnPhaseLoadBarStop();
    }
    public void OnPhaseNameFadein()
    {
        AudioManager.instance.OnPhaseNameFadein();
    }
    public void OnPhaseTextFadein()
    {
        AudioManager.instance.OnPhaseTextFadein();
    }
    public void OnPhaseTextFadeout()
{
    AudioManager.instance.OnPhaseTextFadeout();
}
}
