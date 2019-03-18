using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour {

    public AudioSource audioSrc;

    public static BgmController instance;
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (BgmController.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    public void PlayBGM(AudioClip bgm, float volume = 1.0f, bool fadeIn = true)
    {
        if (audioSrc.clip != bgm)
            audioSrc.clip = bgm;

        if (fadeIn)
        {
            audioSrc.volume = 0.0f;
            StartCoroutine(FadeTowards(volume));
        }

        audioSrc.Play();
    }

    public void StopBGM()
    {
        StartCoroutine(FadeTowards(0.0f));
    }

    // Update is called once per frame
    public IEnumerator FadeTowards (float targetVolume, float duration=3.0f) {
        float timer = 0.0f;

        float startingVolume = audioSrc.volume;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            audioSrc.volume = Mathf.Lerp(startingVolume, targetVolume, timer / duration);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
