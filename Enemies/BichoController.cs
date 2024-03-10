using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BichoController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisi�n es con una bala
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("ExpansiveBomb") || collision.gameObject.CompareTag("fire"))
        {
            Parameters.score++;
            StartCoroutine(animacionMuerte());
        }
    }


    IEnumerator animacionMuerte()
    {
        // Activar la animaci�n de muerte del enemigo
        playerAnimator.SetBool("bichoMuerto", true);
        //playerRb.isKinematic = true;
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }


}
