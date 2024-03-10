using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansiveBomb : MonoBehaviour
{
    public float bombMovement = 10f;
    public float tiempoDestruccion = 3f; // Tiempo antes de destruir el proyectil

    public AudioClip impactSound1; // Lo pongo así porque con arrays no me funcionó
    public AudioClip impactSound2; // 
    public AudioClip impactSound3; // 
    public AudioClip impactSound4; // 
    public AudioClip impactSound5; // 

    // Start is called before the first frame update
    void Start()
    {
        // Destruir el proyectil después de un tiempo determinado
        Destroy(gameObject, tiempoDestruccion);

    }

    // Update is called once per frame
    void Update()
    {
        // Mover el proyectil hacia adelante
        //transform.Translate(Vector3.forward * bombMovement * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {

        // Verificar si la colisión fue con un enemigow
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();

            // Desactivar las constraints de rotación del Rigidbody del personaje
            enemyRB.constraints = RigidbodyConstraints.None;

            // Desactivar la gravedad del Rigidbody del personaje
            enemyRB.useGravity = false;

            int RandomSound = Random.Range(1, 6); // se que es un chapuzón, pero es no ha habido manera de hacerlo con arrays
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


            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRB.AddForce(awayFromPlayer * 70f, ForceMode.Impulse);
            Parameters.score++;

           
        }

      
    }

}
