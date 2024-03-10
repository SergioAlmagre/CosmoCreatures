using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WelcomePage : MonoBehaviour
{

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void ReadStringInput(string input)
    {
        Parameters._nombreJugador = input;
        Debug.Log(Parameters._nombreJugador.ToString());
    }



    public void btnExit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void btnAbout()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("About");
        Debug.Log("About");
    }

    public void btnJugar()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        Debug.Log("Jugar");
    }
    


}
