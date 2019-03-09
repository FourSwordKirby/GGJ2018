using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ConversationExpressions
{
    Normal,
    Worried,
    Disappointed,
    Embarrassed,
    Annoyed
}

public class ConversationController : MonoBehaviour {

    public Animator selfAnimator;
    public DialogUI dialogUIActor1;
    public DialogUI dialogUIActor2;

    private bool conversationFinished;

    const float AUTO_DIALOG_ADVANCE_TIME = 100.0f;
    const string NAME_ACTOR_1 = "Circe";
    const string NAME_ACTOR_2 = "Kali";

    public static ConversationController instance;
    public void Awake()
    {
        if (ConversationController.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    public bool IsConversationFinished()
    {
        return conversationFinished;
    }

    public void StartConversation(List<string> dialog)
    {
        dialogUIActor1.displayDialog("", NAME_ACTOR_1);
        dialogUIActor2.displayDialog("", NAME_ACTOR_2);
        StartCoroutine(PlayConversation(dialog));
    }

    private IEnumerator PlayConversation(List<string> dialogComponents)
    {
        conversationFinished = false;
        string speaker = "";
        string dialog = "";
        bool waitForAdvance;
        float autoAdvanceTimer = 0.0f;

        selfAnimator.SetTrigger("OpenDialog");
        yield return new WaitForSeconds(1.0f); //Used to offset the dialog bar appearance time

        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string currentDialog = dialogComponents[i];
            print(currentDialog);
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



            //Parsing instructions??
            //Filler
            foreach (string rawInstruction in instructionPieces)
            {
                string instruction = rawInstruction.Trim();

                if (instruction.Contains("Pan"))
                {
                    if(instruction.Contains("Player"))
                        CameraControlsTopDown3D.instance.Target(ShmupGameManager.instance.player.gameObject);
                    else if (instruction.Contains("Focus"))
                    {
                        GameObject focus = GameObject.FindObjectsOfType<FocalPoint>().Where(x => x.gameObject.activeSelf).ToList()[0].gameObject;
                        CameraControlsTopDown3D.instance.Target(focus);
                        focus.SetActive(false);
                    }
                }
                if (instruction.Contains("Resume Game"))
                {
                    print("Resuming Game");
                    ShmupGameManager.instance.ResumeGameplay();
                }
                else if (instruction.Contains("PauseGame"))
                {
                    print("Pausing Game");
                    ShmupGameManager.instance.PauseGameplay();
                }
            }

            //parsing speaker
            DialogUI dialogUI = null;
            if (speaker == NAME_ACTOR_1)
            {
                //selfAnimator.SetBool("SpeakingActor1", true);
                //selfAnimator.SetBool("SpeakingActor2", false);
                dialogUI = dialogUIActor1;
            }
            else if (speaker == NAME_ACTOR_2)
            {
                //selfAnimator.SetBool("SpeakingActor1", false);
                //selfAnimator.SetBool("SpeakingActor2", true);
                dialogUI = dialogUIActor2;
            }
            else if (!(speaker == "" && dialog == ""))
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
            while (waitForAdvance)
            {
                autoAdvanceTimer += Time.deltaTime;
                if (Controls.dialogAdvanceDown() || autoAdvanceTimer > AUTO_DIALOG_ADVANCE_TIME)
                {
                    autoAdvanceTimer = 0.0f;
                    break;
                }
                yield return null;
            }
        }

        selfAnimator.SetTrigger("CloseDialog");
        conversationFinished = true;

        yield return null;
    }
}
