using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterHud : MonoBehaviour {

    public GameObject framing;
    public Text chapterTitle;
    public Text chapterSubtitle;
    public UILoadingBar loadingBar;

    public Animation openingAnimation;

    private void Start()
    {
        framing.SetActive(false);
        chapterTitle.gameObject.SetActive(false);
        chapterSubtitle.gameObject.SetActive(false);
        loadingBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayAnimation();
        }
	}

    void PlayAnimation()
    {
        framing.SetActive(true);
        chapterTitle.gameObject.SetActive(true);
        chapterSubtitle.gameObject.SetActive(true);
        loadingBar.gameObject.SetActive(true);
        openingAnimation.Play();
    }
}
