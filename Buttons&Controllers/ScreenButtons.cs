using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenButtons : MonoBehaviour
{

    // Start is called before the first frame update
    private bool isPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void dispararBullet()
    {

    }

    public void dispararBomba()
    {

    }

    public void ataqueAereo()
    {

    }

    public void saltar()
    {

    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausar el tiempo
        }
        else
        {
            Time.timeScale = 1f; // Reanudar el tiempo
        }
    }








}
