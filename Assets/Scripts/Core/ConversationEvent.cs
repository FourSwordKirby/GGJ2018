using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationEvent : ShmupGameEvent
{
    public TextAsset conversation;
    public bool pauseGameplay;


    bool conversationOver = false;

    public override void RunEvent()
    {
        StartCoroutine(StartConversation(conversation, true));
    }

    public IEnumerator StartConversation(TextAsset dialog, bool withPortraits)
    {
        if (pauseGameplay)
            ShmupGameManager.instance.PauseGameplay();

        List<string> dialogEntries = DialogEngine.CreateDialogComponents(dialog.text);
        ConversationController.instance.StartConversation(dialogEntries);
        yield return new WaitForEndOfFrame();

        while (!ConversationController.instance.IsConversationFinished())
            yield return null;

        if (pauseGameplay)
            ShmupGameManager.instance.ResumeGameplay();

        conversationOver = true;
        yield return null;
    }

    public override void EndEvent()
    {
        conversationOver = true;
        throw new System.NotImplementedException();
    }

    public override bool EventCompleted()
    {
        return conversationOver;
    }
}
