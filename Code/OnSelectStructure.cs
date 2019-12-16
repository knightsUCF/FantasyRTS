using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class OnSelectStructure : MonoBehaviour
{


    // R E F E R E N C E S ////////////////////////////////////////////////////////////

    // Outline outline;
    

    // G L O B A L S //////////////////////////////////////////////////////////////////

    public bool outlineToggle = true;


    // M E T H O D S //////////////////////////////////////////////////////////////////

    
    void ShowBuildingOptions()
    {

    }
    
    void OnMouseDown()
    {
        ShowBuildingOptions();
    }

}


/* Simple way of detecting a mouse on click event
 * 
 * 
 * Add collider on game object, put this also on the same game object with the mesh
 * 
 * 
 */



/*

public class OnClick : MonoBehaviour
{

    public Text text;
    public Text text2;


    Outline outline;
    PlanetData planetData;
    Audio audio;



    public bool outlineToggle = true;




    private void Start()
    {
        outline = GetComponent<Outline>();
        planetData = GetComponent<PlanetData>();
        audio = FindObjectOfType<Audio>();
    }




    void OnMouseDown()
    {
        // clear all outlines in scene
        Clear.ClearOutlines();
        outline.enabled = outlineToggle;
        DisplayPlanetData();
        outlineToggle = !outlineToggle;
    }



    void DisplayPlanetData()
    {
        if (outline.enabled)
        {
            // play a sound
            audio.PlaySelect();
            // Debug.Log("This is: " + planetData.Name); // lower case name gives the name of the game object
            text.text = planetData.Name;
            text2.text = planetData.Minerals;
        }

        if (!outline.enabled)
        {
            text.text = "";
            text2.text = "";
        }
    }



}

    */
