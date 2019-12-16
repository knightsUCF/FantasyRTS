using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class InputManager : MonoBehaviour
{


    void Close()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    void Update()
    {
        Close();
    }
}
