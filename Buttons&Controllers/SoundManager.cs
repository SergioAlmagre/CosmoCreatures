using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip sonidoVida;
    public AudioClip sonidoDisparo;
    public AudioClip sonidoGameOver;
    public AudioClip sonidoBomba;
    public AudioClip sonidoDamage;
    public AudioClip sonidoAirAttack;
    private AudioSource audioSource;

    void Start()
    {
        // Obtener una referencia al AudioSource en el SoundManager
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonidoDisparar()
    {
        if (sonidoDisparo != null)
        {
            Debug.Log("Reproduciendo sonido de disparo");
            audioSource.PlayOneShot(sonidoDisparo,1f);
        }
    }


    public void ReproducirSonidoGameOver()
    {
        if (sonidoGameOver != null)
        {
            audioSource.PlayOneShot(sonidoGameOver);
        }
    }

    public void ReproducirSonidoVida()
    {
        if (sonidoVida != null)
        {
            audioSource.PlayOneShot(sonidoVida);
        }   
    }


    public void ReproducirSonidoBomba()
    {
        if (sonidoBomba != null)
        {
            audioSource.PlayOneShot(sonidoBomba);
        }
    }

    public void ReproducirSonidoDamage()
    {
        if (sonidoGameOver != null)
        {
            audioSource.PlayOneShot(sonidoDamage);
        }
    }

    public void ReproducirSonidoAirAttack()
    {
        if (sonidoGameOver != null)
        {
            audioSource.PlayOneShot(sonidoDamage);
        }
    }
}
