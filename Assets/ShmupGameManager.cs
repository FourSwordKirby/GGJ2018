using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShmupGameManager : MonoBehaviour {

    public List<SpawnPoint> spawnPoints;
    public ShmupPlayer player;
    public GameObject warpAnim;

    //Instance Managing;
    public static ShmupGameManager instance;
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

    // Use this for initialization
    void Start () {
        spawnPoints = FindObjectsOfType<SpawnPoint>().ToList();
        player = FindObjectOfType<ShmupPlayer>();
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        RespawnPlayer();

        //TextAsset dialog = ProgressManager.instance.GetStartDialog();
        //DialogEngine.instance.startConversation(dialog);
    }

    public IEnumerator PlayCutscene(TextAsset dialog, bool withPortraits)
    {
        ConversationPause();
        while (Letterbox.instance.isOn)
            yield return null;
        DialogEngine.instance.StartDialog(dialog, withPortraits);

        while(DialogEngine.instance.currentlyActive)
            yield return null;

        ConversationUnpause();
        yield return null;
    }

    public IEnumerator StartConversation(TextAsset dialog, bool withPortraits)
    {
        DialogEngine.instance.StartDialog(dialog, withPortraits);
        yield return null;
    }

    private void ConversationPause()
    {
        PauseGameplay();
        StartCoroutine(Letterbox.instance.TurnOn(2.0f));
    }
    private void ConversationUnpause()
    {
        StartCoroutine(Letterbox.instance.TurnOff(2.0f));
        ResumeGameplay();
    }

    private void MenuPause()
    {
        PauseGameplay();
        StartCoroutine(Letterbox.instance.TurnOn(2.0f));
        //Display a pause menu or something
    }

    private void PauseGameplay()
    {
        Time.timeScale = 0.0f;
        Controls.DisableGameplayControls();
    }

    private void ResumeGameplay()
    {
        Time.timeScale = 1.0f;
        Controls.EnableGameplayControls();
    }


    public void RespawnPlayer()
    {
        SpawnPoint closestSpawn = spawnPoints[0];
        float closestDistance = Vector3.Distance(player.transform.position, closestSpawn.transform.position);
        foreach (SpawnPoint spawn in spawnPoints)
        {
            if (spawn.activated != true)
                continue;

            float dist = Vector3.Distance(player.transform.position, spawn.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestSpawn = spawn;
            }
        }

        player.Spawn(closestSpawn);
        GameObject anim = Instantiate(warpAnim);
        anim.transform.position = player.transform.position;
    }
}
