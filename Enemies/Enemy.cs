using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody enemyRB;
    private GameObject player;
    private Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Parameters.level == 1)
        {
            enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
            if(transform.position.y < -2 || transform.position.y > 10)//Limites del mapa para que mueran
            {
                //actualizarParametros();
            }
            if (transform.position.x < -25 || transform.position.x > 25)//Limites del mapa para que mueran
            {
                //actualizarParametros();
            }
        }

        if(Parameters.level == 2)
        {
            enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
            if (transform.position.y < 34)///Limites del mapa para que mueran
            {
                //actualizarParametros();
            }
        }

        if (Parameters.level == 3)
        {
            enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
            if (transform.position.y < 70) //Limites del mapa para que mueran
            {
                Destroy(gameObject);
            }
                //actualizarParametros();
        }

    }

    //private void actualizarParametros()
    //{
    //    if (Parameters.score % 10 == 0 && Parameters.score != 0)
    //    {
    //        Parameters.addedBombThisFrame = true;
    //    }
    //}



}
