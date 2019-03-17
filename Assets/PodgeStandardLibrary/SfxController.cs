using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour {

    public List<AudioClip> sfx;
    public static SfxController instance;
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SfxController.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    public void PlaySound(string soundName, GameObject source = null, float volume = 1.0f)
    {
        Vector3 position;
        AudioClip sound = null;

        sound = sfx.Find(x => x!= null && x.name == soundName);
        if (source == null)
            position = Camera.main.transform.position;
        else
            position = source.transform.position;
        

        AudioSource.PlayClipAtPoint(sound, position, volume);
    }
}
