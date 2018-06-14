using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour {

    public TextAsset GameStartDialog;

    //Instance Managing;
    public static ProgressManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public TextAsset GetStartDialog()
    {
        return GameStartDialog;
    }
}
