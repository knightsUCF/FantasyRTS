using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AI : MonoBehaviour
{


    // S E T T I N G S ///////////////////////////////////////////////////////////

    // public Vector3 destination = Vector3.zero;


    // R E F E R E N C E S ///////////////////////////////////////////////////////

    public GameObject playerToPursue;
    

    // M E T H O D S /////////////////////////////////////////////////////////////


    // do an invoke repeating to keep pursuing the player's destination, since the player's destination will keep changing, we will keep updating their position every second

    void UpdatePlayerPosition()
    {
        if (GetComponent<KnightEnemy>().engagingTarget)
        {
            Vector3 destination = playerToPursue.transform.position;
            GetComponent<KnightEnemy>().SetDestination(destination);
        }
    }


    // M A I N ///////////////////////////////////////////////////////////////////

    void Start()
    {
        // GetComponent<KnightEnemy>().SetDestination(destination);
        InvokeRepeating("UpdatePlayerPosition", 1.0f, 1.0f);
    }


    void Update()
    {

    }

}
