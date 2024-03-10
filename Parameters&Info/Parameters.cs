using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Parameters : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI bombsText;
    public TextMeshProUGUI levelText;

    static public int score;
    static public int live;
    static public int level;
    static public int bombs;
    static public bool pararMusica = false;
    static public bool GAMEOVER = false;
    static public int difficulty = 0;
    static public bool addedBombThisFrame = false;
    static public string _nombreJugador = "Jugador";
    static public bool tregua;
    static public int enemyWaves = 1;
    private  bool actualizado = false;
    private int j = 0;
    private bool banderaBombas = false;
    static public bool retroceso = false;
    static public bool joyOn = false;

    void Start()
    {
        Reiniciar();  
    }

    void Update()
    {
        // Verificar si se ha sumado una bomba en este fotograma
        if (addedBombThisFrame)
        {
            addedBombThisFrame = false; // Marcar que se ha sumado una bomba en este fotograma
            bombs++;
        }
        actualizarTextos();
        actualizarLevel();
        actualizarBombas();
    }

    public void UpdateScore(int scoreUp)
    {
        score += scoreUp;
        scoreText.text = "Score: " + score;
    }

    public void UpdateVidas(int vidasUp)
    {
        live += vidasUp;
        liveText.text = "Lives: " + live;
    }

    public void UpdateBombas(int bombasUp)
    {
        bombs += bombasUp;
        bombsText.text = "Bombas: " + bombs;
    }

    public void GameOver()
    {
        GAMEOVER = true;
    }

    public void SetDifficulty(int amount)
    {
        difficulty = amount;
    }

    public void Reiniciar()
    {
        GAMEOVER = false;
        score = 0;
        live = 5;
        level = 1;
        bombs = 3;
        enemyWaves = 3;
        scoreText.text = "Score: " + score;
        liveText.text = "Live: " + live;
        levelText.text = "Level: " + level;
        bombsText.text = "Bombs: " + bombs;
    }

    private void actualizarLevel()
    {

        if (score > 200 && score <300)
        {
            level = 2;
            actualizarEnemies();
        }
        else if (score > 600)
        {
            level = 3;
            if(j == 0)
            {
                actualizado = false;
                j++;
            }
            ;
            actualizarEnemies();
        }
    }

    private void actualizarEnemies()
    {
        if(level == 2 && !actualizado)
        { 
            enemyWaves = 1;
            actualizado = true;
            tregua = true;
            Debug.Log("Tregua: " + Parameters.tregua);
        }

        if(level == 3 && !actualizado)
        {
            enemyWaves = 1;
            actualizado = true;
            tregua = true;
            Debug.Log("Tregua: " + Parameters.tregua);
        }
    }


    private void actualizarTextos()
    {
        scoreText.text = "Score: " + score;
        liveText.text = "Live: " + live;
        levelText.text = "Level: " + level;
        bombsText.text = "Bombs: " + bombs;
    }

    IEnumerator treguaTemp()
    {
        yield return new WaitForSeconds(15f);
        tregua = false;
    }

    private void actualizarBombas()
    {
        if (score % 10 == 0  && !banderaBombas)
        {
            Parameters.addedBombThisFrame = true;
            banderaBombas = true;
        }
        if (score % 10 != 0)
        {
            banderaBombas = false;
        }
    }


}

