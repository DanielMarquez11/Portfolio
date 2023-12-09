using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Settings : MonoBehaviour
{
    public GameObject NextPageRightcanvas;
    public GameObject[] Old;
    public GameObject previousPageLeftcanvas;
    public GameObject mainTab;
    


    public void NextPageRight()
    {
        NextPageRightcanvas.SetActive(true);
        previousPageLeftcanvas.SetActive(false);
    }

    public void PreviousPageLeft()
    {
        previousPageLeftcanvas.SetActive(true);
        NextPageRightcanvas.SetActive(false);
    }

    public void Back(string name)
    {
       mainTab.SetActive(true);
       GameObject.Find(name).SetActive(false);
    }

    public void Graphics()
    {
        
    }
}