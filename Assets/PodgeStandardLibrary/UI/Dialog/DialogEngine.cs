using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogEngine : MonoBehaviour
{
    public static DialogEngine instance;
    public DialogUI dialogUI;
    public VisualNovelCharacterController characterController;

    public bool currentlyActive;

    public void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void StartDialog(TextAsset dialog, bool displayPortraits)
    {
        currentlyActive = true;
        List<string> dialogComponents = CreateDialogComponents(dialog.text);

        if(displayPortraits)
            StartCoroutine(displayDialog(dialogComponents));
    }


    string speaker;
    string dialog;
    private IEnumerator displayDialog(List<string> dialogComponents)
    {
        characterController.Open();

        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string currentDialog = dialogComponents[i];

            string[] instructionPieces = new string[0];
            if (dialogComponents[i].Split('[', ']').Count() >= 3 && dialogComponents[i].Split('[', ']')[0] == "")
            {
                instructionPieces = currentDialog.Split('[', ']')[1].Split(',');
                currentDialog = currentDialog.Split('[', ']')[2];
            }

            string[] dialogPieces = currentDialog.Split(':');

            if (dialogPieces.Length > 1)
            {
                speaker = dialogPieces[0];
                dialog = dialogPieces[1];
            }
            else
                dialog = dialogPieces[0];
            speaker = speaker.Trim();
            dialog = dialog.Trim();
            //Parsing instructions
            foreach (string rawInstruction in instructionPieces)
            {
                string instruction = rawInstruction.Trim();
                print(instruction);

                VisualNovelCharacterController.Direction dir = VisualNovelCharacterController.Direction.Left;
                if (instruction[1] == 'l')
                    dir = VisualNovelCharacterController.Direction.Left;
                if (instruction[1] == 'r')
                    dir = VisualNovelCharacterController.Direction.Right;

                string affectedCharacter = instruction.Split(' ')[1];
                if (instruction[0] == '>')
                    characterController.Enter(affectedCharacter, dir);
                if (instruction[0] == '<')
                    characterController.Exit(affectedCharacter, dir);

                if (instruction[0] == '!')
                    characterController.ChangeSprite(affectedCharacter, int.Parse(instruction[1].ToString()));
            }
            characterController.SetSpeaker(speaker);

            //Showing dialog
            dialogUI.displayDialog(dialog, speaker);
            print("running");

            yield return null;

            while (!dialogUI.dialogCompleted)
            {
                if (Controls.dialogAdvanceDown())
                    dialogUI.resolveDialog();
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            //Replace this with things in the control set
            while (true)
            {
                if (Controls.dialogAdvanceDown())
                    break;
                yield return null;
            }
            print("next");
        }

        print("We finished");
        characterController.Close();
        dialogUI.Close();

        currentlyActive = false;

        yield return null;
    }

    private static List<string> CreateDialogComponents(string text)
    {
        List<string> dialogComponents = new List<string>(text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
        return dialogComponents;
    }
}
