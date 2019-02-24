using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionHud : MonoBehaviour {
    public Text instructionText;

    public static InstructionHud instance;

    public void Awake()
    {
        if (InstructionHud.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    public void DisplayInstructions(string instruction)
    {
        this.gameObject.SetActive(true);
        instructionText.text = instruction;
    }

    public void HideInstructions()
    {
        this.gameObject.SetActive(false);
    }
}
