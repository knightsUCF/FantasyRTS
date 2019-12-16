using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class HUD : MonoBehaviour
{

    Build build;

    /*
    [System.Serializable]
    public enum SelectedStructure
    {
        None,
        Barracks,
        Farm
    }

    public SelectedStructure selectedStructure;
    */



    void Start()
    {
        build = FindObjectOfType<Build>();    
    }


    public void OnClickBuildBarracks()
    {
        // selectedStructure = SelectedStructure.Barracks;
        build.Barracks();

    }


    public void OnClickBuildFarm()
    {
        build.Farm();
    }

    public void OnClickBuildCastle()
    {
        build.Castle();
    }
}
