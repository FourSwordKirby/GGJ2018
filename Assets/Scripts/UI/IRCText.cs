using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IRCText : MonoBehaviour {

    public Color ircBack;
    public Color ircHighlight;
    public Color ircTextImportant;

    public Text chatbox;

    public int lineLimit;

    public Animator p1Anim;
    public Animator p2Anim;

    private List<string> messages = new List<string>()
    {
        "[18:30] == roger_[49a9c13d@gateway / web / freenode / ip.73.169.193.61] has joined #roger",
        "[18:30] == mode /#roger [+ns] by herbert.freenode.net",
        "[18:30] == mode /#roger [-o roger_] by services.",
        "[18:30] == mode /#roger [+ct-s] by services.",
        "[18:30] == ChanServ[ChanServ@services.] has joined #roger",
        "[18:30] == mode /#roger [+o ChanServ] by services.",
        "[18:31] == takumif[49a9c13d@gateway / web / freenode / ip.73.169.193.61] has joined #rogerlololollololol",
        "[18:31] == ChanServ[ChanServ@services.] has left #roger []",
        "[18:31] < roger_ > herro",
        "[18:31] < takumif > k",
        "[18:31] < roger_ > do you see this",
        "[18:32] == takumif[49a9c13d@gateway / web / freenode / ip.73.169.193.61] has quit[Client Quit]"
    };



	// Use this for initialization
	void Start () {
        chatbox.text = "";
	}


    int i = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int emotion = p1Anim.GetInteger("Emotion")+1;
            if (emotion > 3)
                emotion = 0;
            p1Anim.SetInteger("Emotion", emotion);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int emotion = p2Anim.GetInteger("Emotion") + 1;
            if (emotion > 3)
                emotion = 0;
            p2Anim.SetInteger("Emotion", emotion);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (chatbox.text != "")
                chatbox.text += "\n";
            chatbox.text += cleanMessage(messages[i]);
            Canvas.ForceUpdateCanvases();
            int currentLineCount = chatbox.cachedTextGenerator.lineCount;

            while (currentLineCount > lineLimit)
            {
                int j = 0;
                int startIndex = chatbox.cachedTextGenerator.lines[j].startCharIdx;
                int endIndex = (i == chatbox.cachedTextGenerator.lines.Count - 1) ? chatbox.text.Length
                    : chatbox.cachedTextGenerator.lines[j + 1].startCharIdx;
                int length = endIndex - startIndex;
                length = Mathf.Min(chatbox.text.IndexOf('\n')+1, length);
                Debug.Log(chatbox.text.Substring(startIndex, length));
                string newText = chatbox.text.Remove(startIndex, length);
                print(newText);
                chatbox.text = newText;

                Canvas.ForceUpdateCanvases();
                currentLineCount = chatbox.cachedTextGenerator.lineCount;
            }
            i++;
        }
    }

    private string cleanMessage(string message)
    {
        message = message.Replace("==", "<color=#" + ColorUtility.ToHtmlStringRGB(ircTextImportant) + ">==</color>");
        return message;
    }
}
