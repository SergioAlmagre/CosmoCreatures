using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : MonoBehaviourPunCallbacks
{

    private Rigidbody playerRb;
    private Animator playerAnimator;

    public ParticleSystem partExplosion;
    public ParticleSystem partCorrer;
    private float gravedad = -92.81f;
    private int dobleSalto = 0;

    public GameObject powerUpIndicator;
    private GameObject canvasBlood;
    private GameObject gameoverImage;
    private GameObject gameInfo;
    public GameObject bombs;
    public GameObject shots;

    private bool enSuelo = true;
    private bool hasPowerUp = false;
    public float powerUpImpulse = 15f;
    public float rotationSpeed = 200;
    public int fuerzaSalto = 100;
    public float speed = 100f;
    public float proyectilSpeed = 10f; // Velocidad inicial del proyectil

    private Quaternion rotacionInicial;
    FollowPlayer followPlayer;
    Vector3 previousPosition;

    public AudioClip sonidoVida;
    public AudioClip sonidoDisparo;
    public AudioClip sonidoGameOver;
    public AudioClip sonidoBomba1;
    public AudioClip sonidoBomba2;
    public AudioClip sonidoBomba3;
    public AudioClip sonidoDamage;
    public AudioClip sonidoAirAttack;
    public AudioClip sonidoCorrer;
    public AudioClip sonidoSaltar;
    public AudioClip sonidoMovimiento;

    private AudioSource audioSource;

    private AudioSource sonidDisparoSource;
    private AudioSource bomba1AudioSource;
    private AudioSource bomba2AudioSource;
    private AudioSource bomba3AudioSource;
    private AudioSource gameoverAudioSource;
    private AudioSource movimientoAudioSource;
    private AudioSource sonidoCorrerAudioSource;

    //public JoystickController joystick; // Referencia al joystick



    // Start is called before the first frame update
    void Start()
    {
        inicializarParametros();
    }


    void Update()
    {


        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space) && dobleSalto < 2 && !Parameters.GAMEOVER)
            {
                saltar();
            } else
            {
                playerAnimator.SetBool("Saltar", false);
            }


            dispararYMover();

            if (Input.GetKeyDown(KeyCode.B))
            {
                dispararBomba();
            }

            comprobarAlturaJugador();
        }
    }



    public void dispararYMover()
    {
        if (Input.GetKey(KeyCode.J))
        {

            shot();

            Parameters.retroceso = true;
            // Obtener la entrada del eje horizontal (por ejemplo, las teclas A y D o las flechas izquierda y derecha)
            float rotationInput = Input.GetAxis("Horizontal");

            // Calcular la rotación del personaje en función de la entrada y la velocidad de rotación
            float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;

            // Rotar el personaje alrededor de su eje vertical (eje Y)
            transform.Rotate(0f, rotationAmount, 0f);

        }
        else
        {
            if (!Parameters.GAMEOVER)
            {

                playerAnimator.SetBool("Disparar", false);
                // Obtener la entrada del teclado para el movimiento horizontal y vertical
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                // Manejar la lógica de correr
                if (horizontalInput == 0 && verticalInput == 0 && !Parameters.joyOn)
                {
                    sonidoCorrerAudioSource.Stop(); // Detener el sonido de correr si no se está moviendo
                    movimientoAudioSource.Stop(); // Detener el sonido de movimiento si no se está moviendo
                    playerAnimator.SetBool("Correr", false); // Detener la animación de correr
                    //playerAnimator.SetBool("recargar", true);
                }
                else
                {
                    if (Input.GetAxis("Vertical") < 0)
                    {
                        playerAnimator.SetBool("Correr", false);
                        playerAnimator.SetBool("Atras", true);
                        
                    }
                    else
                    {
                        playerAnimator.SetBool("Atras", false);
                    }
                    //playerAnimator.SetBool("recargar", false);
                    playerAnimator.SetBool("Correr", true); // Iniciar la animación de correr

                    // Reproducir el sonido de correr
                    if (!sonidoCorrerAudioSource.isPlaying) // Verificar si el sonido no se está reproduciendo para evitar superposiciones
                    {
                        sonidoCorrerAudioSource.clip = sonidoCorrer;
                        movimientoAudioSource.clip = sonidoMovimiento;

                        // Ajustar el volumen del movimientoAudioSource
                        movimientoAudioSource.volume = 0.1f;
                        sonidoCorrerAudioSource.volume = 2f;

                        sonidoCorrerAudioSource.Play();
                        movimientoAudioSource.Play();
                    }

                    // Calcular la rotación del jugador en función de la entrada horizontal
                    transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

                    // Calcular la dirección de movimiento relativa a la rotación actual del jugador
                    Vector3 movementDirection = transform.forward * verticalInput;

                    // Mover al jugador en el mundo en función de la dirección de movimiento
                    transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
                }
            }
        }
    }

    public void StartRafagaDisparo()
    {
       for(int i = 0; i < 45; i++)
        {
            shot();
        }
    }



    public void saltar()
    {
        audioSource.PlayOneShot(sonidoSaltar, 0.5f);
        playerRb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        enSuelo = false;
        playerAnimator.SetBool("Correr", false);
        playerAnimator.SetBool("Saltar", true);
        dobleSalto++;
    }


    private void inicializarParametros()
    {
        // Crear un nuevo AudioSource para cada sonido
        bomba1AudioSource = gameObject.AddComponent<AudioSource>();
        bomba2AudioSource = gameObject.AddComponent<AudioSource>();
        bomba3AudioSource = gameObject.AddComponent<AudioSource>();
        gameoverAudioSource = gameObject.AddComponent<AudioSource>();
        movimientoAudioSource = gameObject.AddComponent<AudioSource>();
        sonidoCorrerAudioSource = gameObject.AddComponent<AudioSource>();

        // Obtener una referencia al AudioSource en el SoundManager
        audioSource = GetComponent<AudioSource>();

        // Gravedad = 0f;
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();

        // Guardar la rotación inicial del personaje
        rotacionInicial = transform.rotation;
        playerAnimator.SetBool("Correr", false);
        Physics.gravity = new Vector3(0, gravedad, 0);

        // Buscar el GameObject con el tag específico y obtener su componente Canvas
        canvasBlood = GameObject.FindWithTag("blood");
        gameoverImage = GameObject.FindWithTag("gameover");
        gameInfo = GameObject.FindWithTag("gameinfo");
        canvasBlood.active = false;
        gameoverImage.active = false;
        gameInfo.active = true;
        dobleSalto = 0;
    }




    public void dispararBomba()
    {
        
        if (Parameters.bombs > 0)
        {
            bomba1AudioSource.clip = sonidoBomba1;
            bomba2AudioSource.clip = sonidoBomba2;
            bomba3AudioSource.clip = sonidoBomba3;

            bomba1AudioSource.volume = 1f;
            bomba2AudioSource.volume = 0.5f;
            bomba3AudioSource.volume = 1f;

            // Reproducir cada sonido de bomba
            bomba1AudioSource.Play();
            bomba2AudioSource.Play();
            bomba3AudioSource.Play();
            // Calcular la posición del proyectil ligeramente delante del personaje
            Vector3 spawnPosition = transform.position + transform.forward;

            // Instanciar el proyectil con una velocidad inicial hacia adelante
            GameObject newBomb = Instantiate(bombs, spawnPosition, bombs.transform.rotation);

            // Ignorar colisiones entre el jugador y el proyectil
            Physics.IgnoreCollision(newBomb.GetComponent<Collider>(), GetComponent<Collider>());
            Parameters.bombs--;
        }
        
    }

       


    private  void shot()
    {
        audioSource.PlayOneShot(sonidoDisparo, 1f);

        playerAnimator.SetBool("Disparar",true);
        // Calcular la dirección hacia la que mira el personaje
        Vector3 shootDirection = transform.forward * 2f;

        // Calcular la rotación hacia la dirección de disparo
        Quaternion shootRotation = Quaternion.LookRotation(shootDirection);

        // Calcular la posición del spawn de la bala ligeramente delante del personaje
        Vector3 spawnPosition = transform.position + shootDirection;
        // Ajustar la posición de salida en el eje Y (por ejemplo, subir 0.5 unidades)
        spawnPosition.y += 1f;

        GameObject newShot = Instantiate(shots, spawnPosition, shootRotation);
        newShot.transform.Rotate(Vector3.right, 90f); // Rotar 90 grados alrededor del eje X

        // Ignorar colisiones entre el jugador y el proyectil
        Physics.IgnoreCollision(newShot.GetComponent<Collider>(), GetComponent<Collider>());

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(PowerupCountdownRoutine());
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
        }
    }




    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            enSuelo = true;
            dobleSalto = 0;

            if (!Parameters.GAMEOVER)
            {
                //partCorrer.Play();
            }
        }


        if (collision.gameObject.CompareTag("battery"))
        {
            Parameters.live += 25;  
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(sonidoVida, 4.0f);
        }


        if (collision.gameObject.name == "Island2")
        {
            Parameters.tregua = false;
        }


        if (collision.gameObject.CompareTag("arbol"))
        {
            //transform.position = previousPosition;
        }


        if (collision.gameObject.name == "Island3")
        {
            Parameters.tregua = false;
        }


        if (collision.gameObject.CompareTag("Enemy"))
        {
            canvasBlood.active = true;
            Parameters.live--;
            audioSource.PlayOneShot(sonidoDamage, 0.8f);
            playerAnimator.SetBool("blood", false);
            StartCoroutine(bloodAnimation());


            if (Parameters.live <= 0 && !Parameters.GAMEOVER)
            {
                playerAnimator.SetBool("Muerto", true);
                finalizarPartida();
            }

        }


    }

    private void comprobarAlturaJugador()
    {
        if (playerRb.transform.position.y < -5 && !Parameters.GAMEOVER)//Limites del mapa para que mueran
        {
            finalizarPartida();
        }
    }
           

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        Debug.Log("Ya no tienes el power");
        powerUpIndicator.SetActive(false);
    }

    IEnumerator muerte()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(gameover());
    }

    IEnumerator bloodAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetBool("blood", true);
        canvasBlood.active = false;
    }

    IEnumerator gameover()
    {
        canvasBlood.active = false;

        if (!gameoverAudioSource.isPlaying) // Verificar si el sonido no se está reproduciendo para evitar superposiciones
        {
            gameoverAudioSource.Play();
        }

        gameoverImage.active = true;
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("WelcomeScene");
    }



    private void finalizarPartida()
    {
        gameInfo.active = false;
        StartCoroutine(muerte());
        canvasBlood.active = true;
        Parameters.live = 0;
        Debug.Log("Fin de la partida");
        Parameters.GAMEOVER = true;
        Parameters.pararMusica = true;
        audioSource.PlayOneShot(sonidoGameOver, 3.0f);

    }



}
