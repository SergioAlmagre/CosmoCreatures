using System.Collections;
using UnityEngine;

public class MoverIsla1 : MonoBehaviour
{
    private float velocidadMinima = 0.001f; // Velocidad mínima de movimiento de la isla
    private float velocidadMaxima = 0.5f; // Velocidad máxima de movimiento de la isla
    private float incrementoVelocidad = 0.001f; // Incremento de velocidad por segundo
    private float velocidadMovimiento; // Velocidad actual de movimiento de la isla
    private int nivelObjetivo = 2; // Nivel en el que se activará el movimiento de la isla
    private bool movimientoActivo = false; // Indica si el movimiento está activo
    private Vector3 puntoInicial; // Punto de inicio del movimiento
    private Vector3 puntoFinal;

    void Start()
    {
        // Guardar la posición inicial de la isla
        puntoInicial = transform.position;
        Debug.Log("Punto inicial: " + puntoInicial);

        // Inicializar la velocidad de movimiento a la velocidad mínima
        velocidadMovimiento = velocidadMinima;
    }

    void Update()
    {
        establecerPuntoFinal();

        // Verificar si el nivel actual es igual al nivel objetivo
        if (Parameters.level == nivelObjetivo)
        {
            // Activar el movimiento de la isla
            movimientoActivo = true;
        }

        // Verificar si el movimiento está activo
        if (movimientoActivo)
        {
            // Aumentar gradualmente la velocidad de movimiento
            if (velocidadMovimiento < velocidadMaxima)
            {
                velocidadMovimiento += incrementoVelocidad * Time.deltaTime;
            }

            // Calcular la nueva posición de la isla usando Lerp para moverla suavemente
            float distanciaRecorrida = Mathf.PingPong(Time.time * velocidadMovimiento, 1f);
            transform.position = Vector3.Lerp(puntoInicial, puntoFinal, distanciaRecorrida);

            //if (Vector3.Distance(transform.position, puntoFinal) < 2f)
            //{
            //    // Desactivar el movimiento de la islaaa
            //    //movimientoActivo = false;
            //    Debug.Log("Movimiento desactivado");
            //    Parameters.tregua = false;
            //    Debug.Log("Tregua: " + Parameters.tregua);
            //}
        }
    }

    private void establecerPuntoFinal()
    {
        if (Parameters.level == 2)
        {
            puntoFinal = new Vector3(16.3f, 51.8f, 84.4f);
        }
        else if (Parameters.level == 3)
        {
            puntoFinal = new Vector3(3.6f, 95.8f, 71.3f);
        }
    }
}
