using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Build : MonoBehaviour
{



    // S E T T I N G S ////////////////////////////////////////////////////////////////////////////

    public float gridSize = 1.0f;



    // R E F E R E N C E S ////////////////////////////////////////////////////////////////////////


    public GameObject castle;
    public GameObject farm;
    public GameObject barracks;

    _Resources resources;
    Settings settings;
    



    // G L O B A L S  /////////////////////////////////////////////////////////////////////////////

    GameObject GO;
    bool startedBuilding = false;
    Ray ray;
    RaycastHit hitInfo;
    Vector3 lastGridPos = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 newGridPos = new Vector3(0.0f, 0.0f, 0.0f);




    // M E T H O D S //////////////////////////////////////////////////////////////////////////////

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        Vector3 closestVector;
        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x / gridSize);
        int zCount = Mathf.RoundToInt(position.z / gridSize);
        int yCount = 0;
        closestVector = new Vector3((float)xCount * gridSize, (float)yCount, (float)zCount * gridSize);
        closestVector += transform.position;
        return closestVector;
    }


    public void Place(GameObject structure)
    {
        startedBuilding = true;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 startPos = Input.mousePosition;
        var pos = GetNearestPointOnGrid(startPos);
        GO = Instantiate(structure, pos, Quaternion.identity, this.transform);
    }
    

    void Drag()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            var gridPos = GetNearestPointOnGrid(hitInfo.point);
            GO.transform.position = gridPos;
            newGridPos = gridPos;
            if (lastGridPos != newGridPos)
            {
                // soundInterface.PlayDragStructureSound();
                lastGridPos = newGridPos;
            }
        }

        if (Input.GetMouseButtonDown(0)) // and building requirements
        {
            Finish();
        }
    }

    void Cancel()
    {
        if (Input.GetMouseButtonDown(1)) Destroy(GO);
    }


    void Finish()
    {
        GameObject finalGO;
        Vector3 finalizedPosition; // not to be confused with finalPosition
        finalizedPosition = GO.transform.position;
        Quaternion finalizedRotation = GO.transform.rotation;
        finalGO = (GameObject)Instantiate(GO, finalizedPosition, finalizedRotation, this.transform);
        Destroy(GO);
        startedBuilding = false;
    }


    void RunBuildingRoutines()
    {
        if (startedBuilding)
        {
            Drag();
            Cancel();
        }
    }



    // P U B L I C   A P I //////////////////////////////////////////////////////////////////////////////

    public void Castle()
    {
        resources.UpdateGold(resources.GetGold() - settings.castleCost);
        Place(castle);
    }

    public void Barracks()
    {
        resources.UpdateGold(resources.GetGold() - settings.barracksCost);
        Place(barracks);
    }

    public void Farm()
    {
        resources.UpdateGold(resources.GetGold() - settings.farmCost);
        Place(farm);
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////




    // M A I N  ///////////////////////////////////////////////////////////////////////////////////


    void Start()
    {
        resources = FindObjectOfType<_Resources>();
        settings = FindObjectOfType<Settings>();
    }


    void Update()
    {
        RunBuildingRoutines();
    }












}

/*

public class Build : MonoBehaviour
{


    // S E T T I N G S ////////////////////////////////////////////////////////////////////////////

    public float gridSize = 10.0f;



    // R E F E R E N C E S ////////////////////////////////////////////////////////////////////////

    public GameObject structureTypeA;
    public GameObject structureTypeB;



    // G L O B A L S  /////////////////////////////////////////////////////////////////////////////

    GameObject currentStructure;
    GameObject GO;
    GameObject finalGO;
    GameObject selectedStructure;
    bool startBuilding = false;
    Ray ray;
    RaycastHit hitInfo;
    Vector3 lastGridPos = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 newGridPos = new Vector3(0.0f, 0.0f, 0.0f);




    // M E T H O D S ////////////////////////////////////////////////////////////////////////////////

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        Vector3 closestVector;
        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x / gridSize);
        int zCount = Mathf.RoundToInt(position.z / gridSize);
        int yCount = 0;
        closestVector = new Vector3((float)xCount * gridSize, (float)yCount, (float)zCount * gridSize);
        closestVector += transform.position;
        return closestVector;
    }


    public void BuildStructure(GameObject structure)
    {
        startBuilding = true;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 startPos = Input.mousePosition;
        // Cursor.visible = false;
        var pos = GetNearestPointOnGrid(startPos);
        GO = Instantiate(gameObject, pos, Quaternion.identity, this.transform);
    }




    void Drag()
    {
        // move structure to drag point

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            var gridPos = GetNearestPointOnGrid(hitInfo.point);
            GO.transform.position = gridPos;
            newGridPos = gridPos;
            if (lastGridPos != newGridPos)
            {
                // soundInterface.PlayDragStructureSound();
                lastGridPos = newGridPos;
            }
        }


        // finalize building on mouse click (and check building requirements if needed)

        if (Input.GetMouseButtonDown(0))
        {
            _Finalize();
        }


        // if we can't build play the appropriate sound effect

        // if (Input.GetMouseButtonDown(0) && !buildingRequirements.canBuild) soundInterface.PlayCantBuildHereSound();


        // cancel building

        // if (Input.GetMouseButtonDown(1)) DestroyBuilding();

    }



    void Rotate()
    {

    }


    void _Finalize()
    {
        // soundInterface.PlayFinalizeStructureSound();
        // Cursor.visible = true;
        Vector3 finalizedPosition; // not to be confused with finalPosition
        finalizedPosition = GO.transform.position;
        Quaternion finalizedRotation = GO.transform.rotation;
        Destroy(GO);
        finalGO = (GameObject)Instantiate(gameObject, finalizedPosition, finalizedRotation, this.transform);
        startBuilding = false;
    }


    void RunBuildingRoutines()
    {
        if (startBuilding) // && GO != null)
        {
            Drag();
            // Rotate();
        }
    }




    // M A I N  /////////////////////////////////////////////////////////////////////////////////////////


    void Start()
    {
        BuildStructure(structureTypeA);
        startBuilding = true;
    }

    void Update()
    {
        RunBuildingRoutines();
    }
}




   // E N D //////////////////////////////////////////////////////////////////////////////////////////////

    */
