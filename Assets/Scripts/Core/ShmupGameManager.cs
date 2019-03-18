using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShmupGameManager : MonoBehaviour {

    public List<SpawnPoint> spawnPoints;
    public ShmupPlayer player;
    public GameObject warpAnim;

    public List<ShmupLevel> levels;
    public int currentLevelIndex;

    //Instance Managing;
    public static ShmupGameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }


    public GameObject dialogComponent;
    // Use this for initialization
    void Start () {
        spawnPoints = levels[0].GetSpawnPoints();
        player = FindObjectOfType<ShmupPlayer>();
        player.gameObject.SetActive(false);

        dialogComponent.SetActive(true);

        StartLevel();
	}

    //This will be the thing that starts off the whole game from whatever the current save point is
    public void StartLevel()
    {
        levels[currentLevelIndex].StartLevel();
    }


    public void RespawnPlayer()
    {
        SpawnPoint closestSpawn = spawnPoints.First(x => x.activated);
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




    public IEnumerator PlayCutscene(TextAsset dialog, bool withPortraits, float autoPlay = 0.0f)
    {
        CutscenePause();
        
        List<string> dialogEntries = DialogEngine.CreateDialogComponents(dialog.text);
        CutsceneDialogController.instance.StartCutscene(dialogEntries, autoPlay);

        while (!CutsceneDialogController.instance.IsCutsceneFinished())
            yield return null;

        CutsceneUnpause();
        yield return null;
    }

    public IEnumerator StartConversation(TextAsset dialog, bool withPortraits)
    {
        List<string> dialogEntries = DialogEngine.CreateDialogComponents(dialog.text);
        ConversationController.instance.StartConversation(dialogEntries);
        yield return null;
    }

    private void CutscenePause()
    {
        PauseGameplay();
    }
    private void CutsceneUnpause()
    {
        ResumeGameplay();
    }

    private void MenuPause()
    {
        PauseGameplay();
        //StartCoroutine(Letterbox.instance.TurnOn(2.0f));
        //Display a pause menu or something
    }

    private void MenuUnpause()
    {
        ResumeGameplay();
        //StartCoroutine(Letterbox.instance.TurnOn(2.0f));
        //Display a pause menu or something
    }

    bool gamePaused = false;
    public bool Paused { get { return gamePaused; } }
    public void PauseGameplay()
    {
        gamePaused = true;
        foreach(ShmupEntity entity in GameObject.FindObjectsOfType<ShmupEntity>())
            entity.Suspend();
    }

    public void ResumeGameplay()
    {
        gamePaused = false;
        foreach (ShmupEntity entity in GameObject.FindObjectsOfType<ShmupEntity>())
            entity.Unsuspend();
    }
}
