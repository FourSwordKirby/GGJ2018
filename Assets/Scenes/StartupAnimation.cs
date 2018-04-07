using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupAnimation : MonoBehaviour {

    public Text startupTextbox;
    public Text asciiLogoTextbox;
    public Text bottomTextbox;

    public AudioSource bootSfx;

    public GameObject fakeLogo;
    public GameObject loadingBar;

    public float currentTime;

    private string startupString =
@"Award Modular BIOS v6.00PG, An Energy Star Ally
Copyright(c) 1984-99, Award Software, Inc.

BIW1M/BIW2M BIOS V1.3

Main Processor : PENTIUM II 910MHz
Memory Testing : ";
    
    // Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;

        if(currentTime < 6)
        {
            startupTextbox.color = Color.Lerp(Color.clear, Color.white, currentTime / 5.0f);
            asciiLogoTextbox.color = Color.Lerp(Color.clear, Color.white, currentTime / 5.0f);
            bottomTextbox.color = Color.Lerp(Color.clear, Color.white, currentTime / 5.0f);
        }

        if (currentTime < 10)
        {
            string loadedMemory = ((int)(131072 * (currentTime / 10.0f))).ToString();
            startupTextbox.text = startupString + loadedMemory + "K OK";
        }
        else if (currentTime < 11)
        {
            if (currentTime - Time.deltaTime <= 10)
                startupString += "131072K OK";
            startupTextbox.text = startupString;
        }
        else if (currentTime < 12)
        {
            if (currentTime - Time.deltaTime <= 11)
                startupString += " + 1024K Shared Memory";

            startupTextbox.text = startupString;
        }

        else if (currentTime < 14)
        {
            if (currentTime - Time.deltaTime <= 12)
                startupString +=
@"


Award Plug and Play BIOS Extension v1.0A
Copyright(C) 1999, Award Software, Inc.";

            startupTextbox.text = startupString;
        }
        else if (currentTime < 15)
        {
            if (currentTime - Time.deltaTime <= 14)
                startupString +=
@"

Detecting Primary Master ... MAXTOR etc etc";

            startupTextbox.text = startupString;
        }
        else if (currentTime < 15.3)
        {
            if (currentTime - Time.deltaTime <= 15)
                startupString +=
@"
Detecting Primary Master ... MAXTOR etc etc";

            startupTextbox.text = startupString;
        }
        else if (currentTime < 15.6)
        {
            if (currentTime - Time.deltaTime <= 15.3)
                startupString +=
@"
Detecting Primary Master ... MAXTOR etc etc";

            startupTextbox.text = startupString;
        }
        else if (currentTime < 18)
        {
            startupTextbox.text = startupString;
        }
        else if (currentTime < 20)
        {
            startupTextbox.text = startupString;

            startupTextbox.color = Color.clear;
            asciiLogoTextbox.color = Color.clear;
            bottomTextbox.color = Color.clear;
        
            bootSfx.volume = Mathf.Lerp(1.0f, 0.0f, (currentTime - 18) / 2);
        }
        else if (currentTime < 29)
        {
            print("Here");
            //Animation after the bootup
            fakeLogo.SetActive(true);
            loadingBar.SetActive(true);
        }
        else if(currentTime < 30)
        {
            //Animation after the bootup
            fakeLogo.SetActive(false);
            loadingBar.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }

        //Blinking Cursor Effect
        if (currentTime < 20 && (int)(currentTime * 6) % 2 == 0)
            startupTextbox.text += "_";
    }
}
