using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollABbutton : MonoBehaviour
{
    public Main Main;
    public void RerollABs()
    {
        Main.ABmanager.RerollABs();
    }
}
