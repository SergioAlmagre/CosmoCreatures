using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutearMusica : MonoBehaviour
{
    public AudioClip musicaAplicacion;
    private AudioSource audioSource;
    private bool musicaPausada = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Agregamos un AudioSource al objeto
        audioSource.clip = musicaAplicacion;
        pausarMusica();
        audioSource.volume = 0.5f;
    }

    void Update()
    {
        

    }

    public void pausarMusica()
    {
        if (musicaPausada)
        {
            audioSource.Pause(); // Reanudar la música
        }
        else
        {
            audioSource.Play(); // Reproducir la música
        }

        musicaPausada = !musicaPausada; // Cambiar el estado de la música pausada/reanudada
    }
}
