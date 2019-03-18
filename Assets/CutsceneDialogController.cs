using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CutsceneExpressions
{
    Normal,
    Worried,
    Disappointed,
    Embarrassed,
    Annoyed
}

public class CutsceneDialogController : MonoBehaviour
{
    public Animator selfAnimator;
    public DialogUI dialogUIActor1;
    public DialogUI dialogUIActor2;

    private bool cutsceneFinished;
    private bool dialogOpen;

    const string NAME_ACTOR_1 = "Circe";
    const string NAME_ACTOR_2 = "Kali";

    public static CutsceneDialogController instance;
    public void Awake()
    {
        if (CutsceneDialogController.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    public void OpenCutsceneWindow()
    {
        if (!dialogOpen)
        {
            selfAnimator.SetBool("ShowLetterbox", true);
            dialogOpen = true;
        }
        else
            throw new Exception("Attempting to start cutscene while its already active");
    }

    public void HideCutsceneWindow()
    {
        if (dialogOpen)
        {
            selfAnimator.SetBool("ShowLetterbox", false);
            dialogOpen = false;
        }
        else
            throw new Exception("Attempting to stop cutscene while its already closed");
    }
    
    public bool IsCutsceneFinished()
    {
        return cutsceneFinished;
    }


    public void StartCutscene(List<string> dialog, float autoPlay = 0.0f)
    {
        cutsceneFinished = false;
        StartCoroutine(PlayCutscene(dialog, autoPlay));
    }

    public void EndCutscene()
    {
        cutsceneFinished = true;
    }

    private IEnumerator PlayCutscene(List<string> dialogComponents, float autoPlay = 0.0f)
    {
        string speaker = "";
        string dialog = "";
        bool waitForAdvance;

        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string currentDialog = dialogComponents[i];

            string[] instructionPieces = new string[0];
            if (dialogComponents[i].Split('[', ']').Count() >= 3 && dialogComponents[i].Split('[', ']')[0] == "")
            {
                instructionPieces = currentDialog.Split('[', ']')[1].Split(',');
                currentDialog = currentDialog.Split('[', ']')[2].Trim();
            }

            string[] dialogPieces = currentDialog.Split(':');

            if (dialogPieces.Length > 1)
            {
                speaker = dialogPieces[0];
                dialog = dialogPieces[1];
            }
            else
            {
                speaker = "";
                dialog = dialogPieces[0];
            }

            speaker = speaker.Trim();
            dialog = dialog.Trim();



            //Parsing instructions
            foreach (string rawInstruction in instructionPieces)
            {
                string instruction = rawInstruction.Trim();

                if (instruction.Contains("Resume Gameplay"))
                {
                    print("Resuming Game");
                    ShmupGameManager.instance.ResumeGameplay();
                }

                if (instruction.Contains("Show"))
                {
                    if(instruction.Contains("1"))
                        selfAnimator.SetBool("ShowActor1", true);
                    if (instruction.Contains("2"))
                        selfAnimator.SetBool("ShowActor2", true);
                    if (instruction.Contains("Letterbox"))
                        OpenCutsceneWindow();
                }
                if (instruction.Contains("Hide"))
                {
                    if (instruction.Contains("1"))
                        selfAnimator.SetBool("ShowActor1", false);
                    if (instruction.Contains("2"))
                        selfAnimator.SetBool("ShowActor2", false);
                    if (instruction.Contains("Letterbox"))
                        HideCutsceneWindow();
                }
            }

            //parsing speaker
            DialogUI dialogUI = null;
            if (speaker == NAME_ACTOR_1)
            {
                while (!dialogUIActor1.gameObject.activeSelf)
                    yield return null;
                selfAnimator.SetBool("SpeakingActor1", true);
                selfAnimator.SetBool("SpeakingActor2", false);
                dialogUI = dialogUIActor1;
            }
            else if (speaker == NAME_ACTOR_2)
            {
                while (!dialogUIActor2.gameObject.activeSelf)
                    yield return null;
                selfAnimator.SetBool("SpeakingActor1", false);
                selfAnimator.SetBool("SpeakingActor2", true);
                dialogUI = dialogUIActor2;
            }
            else if(!(speaker == "" && dialog == ""))
                throw new Exception("This dialog is not supported" + dialogComponents[i] + "/" + speaker + "/" + dialog);

            //Showing dialog
            if (!(speaker == "" && dialog == ""))
            {
                waitForAdvance = true;
                dialogUI.displayDialog(dialog, speaker);
                yield return null;

                while (!dialogUI.dialogCompleted)
                {
                    if (Controls.dialogAdvanceDown())
                        dialogUI.resolveDialog();
                    yield return null;
                }
            }
            else
                waitForAdvance = false;

            yield return new WaitForSeconds(0.25f);
            //Replace this with things in the control set
            float autoAdvanceTimer = 0.0f;
            while (waitForAdvance)
            {
                autoAdvanceTimer += Time.deltaTime;
                if (Controls.dialogAdvanceDown() || (autoPlay > 0.0f && autoAdvanceTimer > autoPlay))
                    break;
                yield return null;
            }
        }

        EndCutscene();
        yield return null;
    }
}
