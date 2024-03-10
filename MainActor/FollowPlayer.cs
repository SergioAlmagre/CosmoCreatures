using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Referencia al transform del jugador
    public float distance = 5f; // Distancia entre la cámara y el jugador
    public float height = 0; // Altura de la cámara respecto al jugador
    public float offsetX = -3f; // Desplazamiento en el eje X
    public float offsetY = -5f; // Desplazamiento en el eje Y
    public float damping = 0f; // Factor de amortiguación para suavizar el movimiento de la cámara

    void LateUpdate()
    {
        // Añadir desplazamiento en el eje X y el eje Y
        Vector3 desiredPosition = playerTransform.position + playerTransform.right * offsetX + Vector3.up * offsetY;

        // Calcular la posición deseada para la cámara detrás del jugador
        desiredPosition -= playerTransform.forward * distance + Vector3.up * height;

        if (Parameters.retroceso)
        {
            // Generar movimiento turbulento usando el seno de un valor periódico (por ejemplo, el tiempo)
            float turbulence = Mathf.Sin(Time.time * 100f) * 100f;
            desiredPosition += transform.right * turbulence; // Aplicar turbulencia en el eje X
            desiredPosition += transform.up * turbulence;    // Aplicar turbulencia en el eje Y
            Parameters.retroceso = false;
        }
        // Lerp (interpolación lineal) suaviza el movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, damping * Time.deltaTime);

        // Hacer que la cámara mire hacia el jugador
        transform.LookAt(playerTransform.position);
    }

}

