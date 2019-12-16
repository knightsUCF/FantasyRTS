using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class PlayerHP : MonoBehaviour
{

    // R E F E R E N C E S //////////////////////////////////////////////////////////////////////////////////////////////////////


    public GameObject parentObjectToDestroy;
    Settings settings;



    // G L O B A L S ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int hp;



    // M E T H O D S ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdateHP(int amount)
    {
        hp = amount;
        GetComponent<TextMesh>().text = hp.ToString();

        if (hp <= 0)
        {
            // if (GetComponent<EnemyCharacter>().attacker != null) GetComponent<EnemyCharacter>().attacker.EnemyDestroyed();

            Destroy(parentObjectToDestroy);
        }
    }


    public int GetHP()
    {
        return hp;
    }



    // M A I N /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        GetComponent<TextMesh>().text = hp.ToString();
        settings = FindObjectOfType<Settings>();
    }


}
