using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//A crappy thrown together script for forming barricades
//Future editors should use execute in 
//[ExecuteInEditMode]
public class BarricadeCreator : MonoBehaviour {

    public GameObject IndicatorArea;

    public GameObject Barricade;
    public float objWidth;
    public float objHeight;
    public float spacing;

    private void Start()
    {
        IndicatorArea.SetActive(false);
        float width = this.transform.localScale.x;
        float height = this.transform.localScale.z;

        int x = (int)(width / (objWidth + spacing));
        int z = (int)(height / (objHeight + spacing));

        float width_origin = (int)this.transform.position.x - (x / 2) * (objWidth + spacing) + (objWidth + spacing)/2;
        float height_origin = (int)this.transform.position.z - (z / 2) * (objHeight + spacing) + (objHeight + spacing) / 2;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                GameObject barricade_instance = PrefabUtility.InstantiatePrefab(Barricade as GameObject) as GameObject;
                float xpos = (width_origin + i * (objWidth + spacing));
                float zpos = (height_origin + j * (objHeight + spacing));
                barricade_instance.transform.position = new Vector3(xpos, 0, zpos);
                //barricade_instance.transform.parent = this.transform;
            }
        }
    }
}
