using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroAttack : MonoBehaviour
{
    public GameObject llamaPrefab;
    public Transform player; // Referencia al transform del personaje principal
    public float spawnHeight = 200f; // Altura desde la cual se generarán las llamas
    public float spawnRadius = 200f;
    public float despawnDelay = 7f;

    private AudioSource airAttackAudioSource;
    public AudioClip sonidoAirAttack;
    private AudioSource audioSource;

    void Start()
    {
        // Obtener una referencia al AudioSource en el SoundManager
        audioSource = GetComponent<AudioSource>();

        // Crear un nuevo AudioSource para el sonido de ataque aéreo
        airAttackAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Obtener una referencia al AudioSource en el SoundManager
        audioSource = GetComponent<AudioSource>();

        if (Input.GetKeyDown(KeyCode.F)) // Cambia esta tecla según tus necesidades
        {
            lanzarAtaqueAereo();
        }
    }



    public void lanzarAtaqueAereo()
    {
        for (int i = 0; i < 25; i++)
        {
            airAttack();
        }
    }

    private void airAttack()
    {
        airAttackAudioSource.clip = sonidoAirAttack;
        airAttackAudioSource.volume = 0.5f;
        airAttackAudioSource.Play();

        // Obtener la posición del jugador
        Vector3 playerPos = player.position;

        // Generar llamas dentro de un área cuadrada alrededor del jugador
        Vector3 spawnPos = playerPos + new Vector3(Random.Range(-spawnRadius, spawnRadius), spawnHeight, Random.Range(-spawnRadius, spawnRadius));

        // Instanciar la llama en la posición calculada
        GameObject newLlama = Instantiate(llamaPrefab, spawnPos, Quaternion.identity);

        // Iniciar la cuenta regresiva para despawnDelay segundos antes de desaparecer las llamas
        StartCoroutine(DestroyAfterDelay(newLlama, despawnDelay));

        // Aplicar fuerza hacia abajo para simular la caída de las llamas
        Rigidbody rb = newLlama.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.down * 20f, ForceMode.Impulse);
        }
    }
    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Convertir el objeto en kinemático para que deje de moverse
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Destruir el objeto después del retraso
        Destroy(obj);
    }
}
