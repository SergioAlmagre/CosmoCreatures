using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 200f;
    public float tiempoDestruccion = 6f; // Tiempo antes de destruir el proyectil
    private Rigidbody rb;

    public AudioClip impactSound1; // Lo pongo así porque con arrays no me funcionó
    public AudioClip impactSound2; // 
    public AudioClip impactSound3; // 
    public AudioClip impactSound4; // 
    public AudioClip impactSound5;


    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();


        // Aplicar velocidad inicial
        rb.velocity = transform.forward * bulletSpeed;

        // Destruir el proyectil después de un tiempo determinado
        Destroy(gameObject, tiempoDestruccion);


    }

    // Update is called once per frame
    void Update()
    {

        //// Mover el proyectil hacia adelante
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {


        // Verificar si la colisión fue con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRB.AddForce(awayFromPlayer * 40f, ForceMode.Impulse);



            int RandomSound = Random.Range(1, 5); // se que es un chapuzón, pero es no ha habido manera de hacerlo con arrays
            if (RandomSound == 1)
            {
                AudioSource.PlayClipAtPoint(impactSound1, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound1, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound1, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound1, transform.position, 100f);
            }
               
              


            if (RandomSound == 2)
            {
                AudioSource.PlayClipAtPoint(impactSound2, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound2, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound2, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound2, transform.position, 100f);
            }
                
             
       
   
            if (RandomSound == 3)
            {
                AudioSource.PlayClipAtPoint(impactSound3, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound3, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound3, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound3, transform.position, 100f);
            }
                
               

            if (RandomSound == 4)
            {
                AudioSource.PlayClipAtPoint(impactSound4, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound4, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound4, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound4, transform.position, 100f);
            }
               

            if (RandomSound == 5) //Grito
            {
                AudioSource.PlayClipAtPoint(impactSound5, transform.position, 100f);
                AudioSource.PlayClipAtPoint(impactSound5, transform.position, 100f);
            }
                
             
       
            




            // Destruir el proyectil solo si impacta con un enemigo
            Destroy(gameObject);
            Parameters.score++;
        }


    }
}
