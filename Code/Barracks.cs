using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Add collider on game object, put this also on the same game object with the mesh



public class Barracks : MonoBehaviour
{


    // R E F E R E N C E S ///////////////////////////////////////////////////

    public GameObject barracksGUI;
    public GameObject knight;

    _Resources resources;
    Settings settings;



    // G L O B A L S /////////////////////////////////////////////////////////

    bool barracksMenuToggle = true;



    // M E T H O D S ////////////////////////////////////////////////////////


    void OnMouseDown()
    {
        // TODO: will need to turn off all other selections with a global command: Clear.AllSelections();
        barracksGUI.SetActive(barracksMenuToggle);
        barracksMenuToggle = !barracksMenuToggle;
    }


    public void GenerateKnight()
    {
        resources.UpdateGold(resources.GetGold() - settings.knightCost);
        
        // TODO: determine if we can generate a unit per unit requirements
        // TODO: will need to generate the next knight up some where to not collider with previous knight, need some mechanism to detect if there are knights there, if so simply move along x axis 5 units or so
        // TODO: check how many units are allowed to generate
        // TODO: generate units per timer
        // TODO: have some sort of check if a structure is close by, let's not generate a unit there

        Vector3 startingWarriorPos = transform.position;
        startingWarriorPos.x += 10;

        GameObject GO = Instantiate(knight, startingWarriorPos, Quaternion.identity, this.transform);
    }


    // M A I N ////////////////////////////////////////////////////////////////

    void Start()
    {
        resources = FindObjectOfType<_Resources>();
        settings = FindObjectOfType<Settings>();
        // barracksGUI = GameObject.FindGameObjectsOfTypeAll("BarracksGUI");
    }
}
