using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class JoystickController : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _moveSpeed;
    private Animator playerAnimator;
    private Rigidbody _rigidbody;
    private Vector3 _moveVector;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Obtiene la entrada del joystick.
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        // Calcula la rotación del jugador en función de la entrada horizontal
        transform.Rotate(Vector3.up * horizontalInput * _rotateSpeed * Time.deltaTime);

        // Calcula la dirección de movimiento relativa a la rotación actual del jugador
        Vector3 movementDirection = transform.forward * verticalInput;

        // Mover al jugador en el mundo en función de la dirección de movimiento
        transform.Translate(movementDirection * _moveSpeed * Time.deltaTime, Space.World);


        // Simula el movimiento del joystick con teclas de teclado
        if (horizontalInput > 0.5f) // Mover hacia la derecha
        {
            Parameters.joyOn = true;

        }
        else if (horizontalInput < -0.5f) // Mover hacia la izquierda
        {
            Parameters.joyOn = true;
        }
        else
        {
            Parameters.joyOn = false;
        }

        

        if (verticalInput > 0.1f) // Mover hacia adelante
        {
            Parameters.joyOn = true;
            playerAnimator.SetBool("recargar", false);
            playerAnimator.SetBool("Correr", true); // Iniciar la animación de correr
        }
        else if (verticalInput < -0.1f) // Mover hacia atrás
        {
            Parameters.joyOn = true;
            playerAnimator.SetBool("Correr", false);
            playerAnimator.SetBool("Atras", true);
        }
        else
        {
            Parameters.joyOn = false;
        }
    }







}
