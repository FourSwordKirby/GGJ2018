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

    // Update is called once per frame
    public IEnumerator FadeTowards (float targetVolume, float duration=1.0f) {
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
