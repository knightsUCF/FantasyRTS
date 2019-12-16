using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class EnemyCharacter : MonoBehaviour
{

    // R E F E R E N C E S //////////////////////////////////////////////////////////////

    public HP hp;
    Settings settings;
    public GameObject attacker;

    

    // M E T H O D S ////////////////////////////////////////////////////////////////////


    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "PlayerWeapon")
        {
            hp.UpdateHP(hp.GetHP() - settings.swordDamage);

            if (hp.GetHP() <= 0)
            {
                GameObject go = c.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                go.GetComponent<Knight>().EnemyDestroyed();
            }
        }

    }


    // M A I N ////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        settings = FindObjectOfType<Settings>();
    }

}




/*
 * 
 *     void OnCollisionEnter(Collision c)
    {
        if (c.tag == "PlayerWeapon")
       
            Debug.Log("detecting player weapon");
        }

    }
void OnTriggerEnter(Collider c)
{
if (c.tag == "Player")
{
    Debug.Log("detecting player");

    if (c.gameObject.GetComponent<Knight>().state == c.gameObject.GetComponent<Knight>().State.Attacking)
    {
        Debug.Log("Detecting attacking state");
    }
}
}

*/

