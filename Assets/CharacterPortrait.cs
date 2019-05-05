using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Expressions
{
    Neutral,
    Excited,
    Surprised,
    Helpful,
    Pleased,
    Smug,
    Bored,
    Serious,
    Embarassed,
    Disappointed,
    CautiousWorried,
    UpsetWorried,
    Agitated,
    VeryAgitated,
    MostAgitated
}

public class CharacterPortrait : MonoBehaviour {
    
    public List<Sprite> mouthExpressions;
    public List<Sprite> eyeExpressions;
    public List<Sprite> decalExpressions;

    public Image portraitBody;
    public Image portraitMouth;
    public Image portraitEyes;
    public Image portraitDecal;

    public void DiplayExpression(Expressions expression)
    {
        switch (expression)
        {
            case Expressions.Neutral:
                portraitMouth.sprite = mouthExpressions[0];
                portraitEyes.sprite = eyeExpressions[0];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Excited:
                portraitMouth.sprite = mouthExpressions[3];
                portraitEyes.sprite = eyeExpressions[2];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Surprised:
                portraitMouth.sprite = mouthExpressions[0];
                portraitEyes.sprite = eyeExpressions[3];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Helpful:
                portraitMouth.sprite = mouthExpressions[3];
                portraitEyes.sprite = eyeExpressions[0];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Pleased:
                portraitMouth.sprite = mouthExpressions[3];
                portraitEyes.sprite = eyeExpressions[1];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Smug:
                portraitMouth.sprite = mouthExpressions[4];
                portraitEyes.sprite = eyeExpressions[1];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Bored:
                portraitMouth.sprite = mouthExpressions[2];
                portraitEyes.sprite = eyeExpressions[1];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Serious:
                portraitMouth.sprite = mouthExpressions[1];
                portraitEyes.sprite = eyeExpressions[0];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Embarassed:
                portraitMouth.sprite = mouthExpressions[2];
                portraitEyes.sprite = eyeExpressions[2];
                portraitDecal.color = Color.clear;
                break;
            case Expressions.Disappointed:
                portraitMouth.sprite = mouthExpressions[0];
                portraitEyes.sprite = eyeExpressions[0];
                portraitDecal.color = Color.white;
                portraitDecal.sprite = decalExpressions[0];
                break;
            case Expressions.CautiousWorried:
                portraitMouth.sprite = mouthExpressions[5];
                portraitEyes.sprite = eyeExpressions[0];
                portraitDecal.color = Color.white;
                portraitDecal.sprite = decalExpressions[0];
                break;
            case Expressions.UpsetWorried:
                portraitMouth.sprite = mouthExpressions[5];
                portraitEyes.sprite = eyeExpressions[2];
                portraitDecal.color = Color.white;
                portraitDecal.sprite = decalExpressions[0];
                break;
            case Expressions.Agitated:
                portraitMouth.sprite = mouthExpressions[1];
                portraitEyes.sprite = eyeExpressions[1];
                portraitDecal.color = Color.white;
                portraitDecal.sprite = decalExpressions[1];
                break;
            case Expressions.VeryAgitated:
                portraitMouth.sprite = mouthExpressions[1];
                portraitEyes.sprite = eyeExpressions[0];
                portraitDecal.color = Color.white;
                portraitDecal.sprite = decalExpressions[1];
                break;
            case Expressions.MostAgitated:
                portraitMouth.sprite = mouthExpressions[1];
                portraitEyes.sprite = eyeExpressions[2];
                portraitDecal.color = Color.white;
                portraitDecal.sprite = decalExpressions[1];
                break;
        }
    }
}
