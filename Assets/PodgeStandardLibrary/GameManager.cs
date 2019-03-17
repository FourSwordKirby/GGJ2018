using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BgmController bgm;
    public AudioClip normalOst;
    public AudioClip arcadeOst;
    public AudioClip forestOst;


    public bool paused;
    public float startingTime;
    public float currentTime;
    public float timeLimit;
    private bool justInstantiated = true;

    private bool loadingScene = false;

    public List<AudioClip> menuSfx;
    public List<AudioClip> environmentSfx;
    public List<AudioClip> itemSfx;

    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    float prevTime;
    void Update()
    {
        //EMERGENCY RESET COMBINATION
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene("Menu");
            SuspendGame();
            return;
        }
    }

    /// <summary>
    /// Used when you're in a cutscene or dialog is happening, basically, stop the player + time and the movements
    /// </summary>
    public void SuspendGame()
    {
        paused = true;
        //add other stuff as needed.
        //Probably need a player function to initialize it or something
    }

    /// <summary>
    /// Used to restore normal player controls/the flow of time etc.
    /// </summary>
    public void UnsuspendGame()
    {
        paused = false;
    }

    public void TogglePauseMenu()
    {
        if(!paused)
        {
            GameManager.instance.playSound(SoundType.Menu, "MenuOpen");
        }
        else
        {
            GameManager.instance.playSound(SoundType.Menu, "MenuOpen");
        }
        paused = !paused;
    }

    public void playSound(SoundType soundType, string soundName, bool startingSound = false)
    {
        Vector3 position;
        AudioClip sound = null;
        if (!startingSound)
            position = CameraControls.instance.transform.position;
        else
            position = Vector3.back * 10.0f;
        if (soundType == SoundType.Menu)
            sound = menuSfx.Find(x => x.name == soundName);
        else if (soundType == SoundType.Environment)
            sound = environmentSfx.Find(x => x.name == soundName);
        else if (soundType == SoundType.Item)
            sound = itemSfx.Find(x => x.name == soundName);

        AudioSource.PlayClipAtPoint(sound, position);
    }

    public void startLoop(string soundName)
    {
        transform.Find("LoopingSounds").Find(soundName).gameObject.SetActive(true);
    }

    public void stopLoop(string soundName)
    {
        transform.Find("LoopingSounds").Find(soundName).gameObject.SetActive(false);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    
    public bool canSwitchRooms() {
        return !loadingScene;
    }

    public void roomSwitchSet(bool canSwitchRooms)
    {
        loadingScene = canSwitchRooms;
    }
}
