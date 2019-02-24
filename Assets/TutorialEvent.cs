using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TutorialType
{
    Move,
    Shoot,
    Register,
    Aim,
    Gameflow,
    Certificate,
    Bomb,
    Respawn
}

public class TutorialEvent : ShmupGameEvent
{
    public string tutorialText;
    public TutorialType tutorialType;
    public Wave associatedWave;

    bool instructionOver = false;

    const float MIN_DISPLAY_TIME = 0.5f;
    const float FOCUS_TIME = 2.0f;
    const float PAUSE_TIME = 2.0f;

    public override void RunEvent()
    {
        instructionOver = false;
        StartCoroutine(RunTutorial(tutorialText));
    }

    private IEnumerator RunTutorial(string tutorialText)
    {
        if(this.tutorialText.Contains("Pan") || this.tutorialText.Contains("Pause"))
            InstructionHud.instance.DisplayInstructions(tutorialText.Split('[', ']')[2].Trim());
        else
            InstructionHud.instance.DisplayInstructions(tutorialText);

        //Parsing the tutorial Text
        if (this.tutorialText.Contains("Pan"))
        {
            if (this.tutorialText.Contains("Player"))
            {
                CameraControlsTopDown3D.instance.Target(ShmupGameManager.instance.player.gameObject);
                yield return new WaitForSeconds(FOCUS_TIME);
            }
            else if (this.tutorialText.Contains("Focus"))
            {
                GameObject focus = GameObject.FindObjectsOfType<FocalPoint>().Where((x) => x.gameObject.activeSelf).ToList()[0].gameObject;
                CameraControlsTopDown3D.instance.Target(focus);
                focus.SetActive(false);
                yield return new WaitForSeconds(FOCUS_TIME);
            }
            CameraControlsTopDown3D.instance.Target(ShmupGameManager.instance.player.gameObject);
            yield return null;
        }
        else if (this.tutorialText.Contains("Pause"))
        {
            ShmupGameManager.instance.PauseGameplay();
            yield return new WaitForSeconds(PAUSE_TIME);
            ShmupGameManager.instance.ResumeGameplay();
            yield return null;
        }

        Vector3 originalPosition = ShmupGameManager.instance.player.transform.position;
        Quaternion originalRotation = ShmupGameManager.instance.player.transform.rotation;
        float startingBombs = ShmupGameManager.instance.player.bombCharge;

        switch (tutorialType)
        {
            case TutorialType.Move:
                while ((ShmupGameManager.instance.player.transform.position-originalPosition).magnitude < 2)
                    yield return null;
                break;
            case TutorialType.Shoot:
                while (associatedWave.nextWaveTrigger != null && associatedWave.nextWaveTrigger.IsDead())
                    yield return null;
                break;
            case TutorialType.Register:
                while (associatedWave.nextWaveTrigger != null && !associatedWave.nextWaveTrigger.IsCompleted())
                    yield return null;
                break;
            case TutorialType.Aim:
                while (associatedWave.nextWaveTrigger != null && !associatedWave.nextWaveTrigger.IsCompleted())
                    yield return null;
                break;
            case TutorialType.Certificate:
                while (associatedWave.nextWaveTrigger != null && !associatedWave.nextWaveTrigger.IsCompleted() 
                        && ShmupGameManager.instance.player.bombCharge == 0)
                    yield return null;
                break;
            case TutorialType.Bomb:
                while (ShmupGameManager.instance.player.bombCharge < startingBombs)
                    yield return null;
                break;
            case TutorialType.Respawn:
                while (!ShmupGameManager.instance.player.IsDead())
                    yield return null;
                break;
            default:
                yield return new Exception("Not yet implemented");
                break;
        }

        yield return new WaitForSeconds(MIN_DISPLAY_TIME);
        EndEvent();
        yield return null;
    }

    public override void EndEvent()
    {
        InstructionHud.instance.HideInstructions();
        instructionOver = true;
    }

    public override bool EventCompleted()
    {
        return instructionOver;
    }
}