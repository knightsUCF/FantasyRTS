using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class _Resources : MonoBehaviour
{

    // P U B L I C   R E S O U R C E  V I E W ///////////////////////////////////////////////////////
    
    public int gold = 0;


    // R E F E R E N C E S //////////////////////////////////////////////////////////////////////////

    public Text goldText;
    Settings settings;


    
    
    // P U B L I C  A P I /////////////////////////////////////////////////////////////////////////////////


    public void UpdateGold(int amount)
    {
        gold = amount;
        goldText.text = "GOLD: " + gold.ToString();
    }


    public int GetGold()
    {
        return gold;
    }

    
    // M A I N ///////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        settings = FindObjectOfType<Settings>();
        gold = settings.startingGold;
        goldText.text = "GOLD: " + gold.ToString();
    }




    
}
