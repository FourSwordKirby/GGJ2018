using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingBar : MonoBehaviour {

    public AnimationCurve curve;
    public float duration = 10.0f;
    public Text loadingText;
    public Text loadingInfo;

    public List<string> loadingComments;
    public int loadingSize;
    float counter = 0.0f;

    // Update is called once per frame
    void Update () {
        counter += Time.deltaTime;
        float progress = curve.Evaluate(counter / duration);

        if (progress < 1)
        {
            string loadingComment = loadingComments[(int)(loadingComments.Count * progress)] + ": ";
            string loadingBar = "|";
            for (int i = 0; i < 25; i++)
            {
                if (i < 27 * progress)
                    loadingBar += "█";
                else
                    loadingBar += ".";
            }
            loadingBar += ".";
            loadingBar += "|";

            string loadingWheel = " [";
            int iteration = (int)(counter / 0.2f) % 4;
            if (iteration == 0)
                loadingWheel += "-";
            if (iteration == 1)
                loadingWheel += "/";
            if (iteration == 2)
                loadingWheel += "|";
            if (iteration == 3)
                loadingWheel += "\\";
            loadingWheel += "] ";

            string loadingProgress = " " + ((int)(progress * loadingSize)).ToString("00") + "/" + loadingSize.ToString("00") + " ";
            string loadingETA = "ETA: " + (duration - counter).ToString("0.000");
            loadingText.text = loadingComment;
            loadingInfo.text = loadingBar + loadingWheel + loadingProgress + loadingETA;
        }
        else
        {
            string loadingComment = "Finished: ";
            string loadingBar = "|";
            for (int i = 0; i < 25; i++)
            {
                loadingBar += "█";
            }
            loadingBar += "|";

            string loadingWheel = " [-] ";

            string loadingProgress = " " + loadingSize + "/" + loadingSize + " ";
            string loadingETA = "ETA: 0.000";
            loadingText.text = loadingComment;
            loadingInfo.text = loadingBar + loadingWheel + loadingProgress + loadingETA;
        }
	}
}
